using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofact.Caching;
using Core.Aspects.Autofact.Performance;
using Core.Aspects.Autofact.Transaction;
using Core.Aspects.Autofact.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("car.delete,admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            if (car == null)
            {
                return new ErrorResult(Messages.CarnotDeleted);
            }
            else
            {
                _carDal.Delete(car);
                return new SuccessResult(Messages.CarDeleted);
            }
        }


        //[SecuredOperation("car.list,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheAspect]

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 2)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            else
            {
                return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
            }

        }

        [CacheAspect]
        [PerformanceAspect(5)]
        //[SecuredOperation("car.getbyid,admin")]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p => p.Id == id));
        }

        //[CacheAspect]
        // [SecuredOperation("car.getcarsdetails,admin")]
        public IDataResult<CarDetailDto> GetCarDetailsById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
            if (result==null)
            {
                return new ErrorDataResult<CarDetailDto>();
            }

            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetailById(c=>c.CarId==id));
        }

        [CacheAspect]
        //[SecuredOperation("car.getcarsbranid,admin")]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            var result = (_carDal.GetAll(p => p.BrandId == id)).Any();
            if (result == false)
            {
                return new ErrorDataResult<List<Car>>(Messages.NoRegisteredCars);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id), Messages.CarsListed);
        }

        [CacheAspect]
        //[SecuredOperation("car.getcarsbycolorid,admin")]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            var result = (_carDal.GetAll(c => c.ColorId == id)).Any();
            if (result == false)
            {
                return new ErrorDataResult<List<Car>>(Messages.NoRegistered);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id), Messages.CarsListed);
        }

        public IDataResult<int> GetCarFindex(int carId)
        {
            var result = _carDal.Get(c => c.Id == carId);
            return new SuccessDataResult<int>(result.FindexScore);
        }


        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run();

            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);

        }

        //[SecuredOperation("car.update,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {

            if (car == null)
            {
                return new ErrorResult(Messages.CarnotUpdated);
            }
            else
            {
                _carDal.Update(car);
                return new SuccessResult(Messages.CarUpdated);
            }

        }

        [TransactionScopeAspect]
        public IResult AddTransactionTest(Car car)
        {
            Add(car);
            if (car.DailyPrice < 0)
            {
                throw new Exception("");
            }
            Add(car);

            return null;
        }

        public IDataResult<List<CarDetailDto>> GetCarByBrandIdAndByColorId(int brandId, int colorId)
        {
            var result = (_carDal.GetAll(c => c.BrandId == brandId && c.ColorId == colorId)).Any();
            if (result == false)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.CarNotFound);

            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailList()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailListByBrandId(int brandId)
        {
            var result = (_carDal.GetCarDetails(c => c.BrandId == brandId)).Any();
            if (result == false)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.NoRegisteredCars);
            }

            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(b => b.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailListByColorId(int colorId)
        {
            var result = (_carDal.GetCarDetails(c => c.ColorId == colorId)).Any();
            if (result==false)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.NoRegistered);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId));
        }
    }
}

