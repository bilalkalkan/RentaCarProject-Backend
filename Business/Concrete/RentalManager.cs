using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofact.Validation;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {

        private readonly IRentalDal _rentalDal;
        private readonly ICarService _carService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerService _customerService;

        public RentalManager(IRentalDal rentalDal, ICarService carService, IHttpContextAccessor httpContextAccessor, ICustomerService customerService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _httpContextAccessor = httpContextAccessor;
            _customerService = customerService;
        }


        [Authentication]
       
        public IResult Add(Rental rental)
        {
            var authUserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId();
            var customer = _customerService.GetByUserId(authUserId);

            if (customer.Success == false)
            {
                return new ErrorResult();
            }
            var result = BusinessRules.Run(IsCarReturned(rental.CarId), IsCarExists(rental.CarId),IsFindexScoreEnough(rental.CarId,customer.Data.UserId));
            if (result != null)
            {
                return result;
            }
            var carToRent = _carService.GetById(rental.CarId);
            Rental rentalToAdd = new Rental();
            rentalToAdd.RentDate = DateTime.Now;
            rentalToAdd.CarId = rental.CarId;
            rentalToAdd.CustomerId = customer.Data.UserId;
            rentalToAdd.RentDays = rental.RentDays;
            rentalToAdd.TotalPrice = (decimal)rental.RentDays * carToRent.Data.DailyPrice;
            _rentalDal.Add(rentalToAdd);
            return new SuccessResult(Messages.CarRented);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult Update(Rental rental)
        {
            throw new NotImplementedException();
        }


        private IResult IsCarReturned(int id)
        {
            var result = _rentalDal.Get(c => c.CarId == id && c.ReturnDate == null);
            if (result != null)
            {
                return new ErrorResult(Messages.CarBusy);
            }
            return new SuccessResult();
        }
        private IResult IsCarExists(int id)
        {
            var result = _carService.GetById(id);
            if (result.Data == null)
            {
                return new ErrorResult(Messages.CarNotExists);
            }
            return new SuccessResult();
        }

        private IResult IsFindexScoreEnough(int carId,int customerId)
        {
            var customer = _customerService.GetByUserId(customerId).Data;
            var car = _carService.GetById(carId).Data;
            if (customer.FindexScore>=car.FindexScore)
            {
                return new SuccessResult();
            }

            return new ErrorResult("Findesk puanınız düşük");
        }
    }
}
