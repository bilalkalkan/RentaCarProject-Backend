using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        readonly ICreditCardDal _creditCardDal;
        readonly ICustomerService _customerService;
        readonly IHttpContextAccessor _httpContextAccessor;

        public CreditCardManager(ICreditCardDal creditCardDal, IHttpContextAccessor httpContextAccessor, ICustomerService customerService)
        {
            _creditCardDal = creditCardDal;
            _httpContextAccessor = httpContextAccessor;
            _customerService = customerService;
        }


        [Authentication]
        public IDataResult<List<CreditCard>> GetCards()
        {
            int authUserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId();
            var customer = _customerService.GetByUserId(authUserId);
            if (customer.Success == false)
            {
                return new ErrorDataResult<List<CreditCard>>();
            }

            var result = _creditCardDal.GetAll(c => c.CustomerId == customer.Data.UserId);
            if (result == null)
            {
                return new ErrorDataResult<List<CreditCard>>(Messages.CreditCardNotFound);
            }
            return new SuccessDataResult<List<CreditCard>>(result);
        }

        [Authentication]
        public IResult Save(CreditCard card)
        {
            var authUserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId();
            var customer = _customerService.GetByUserId(authUserId);
            var result = BusinessRules.Run(IsCardExist(card.CardNumber));
            if (result != null)
            {
                return result;
            }
            card.CustomerId = customer.Data.UserId;
            _creditCardDal.Add(card);
            return new SuccessResult(Messages.addedCard);
        }

        private IResult IsCardExist(string cardNumber)
        {
            var result = _creditCardDal.Get(c => c.CardNumber == cardNumber);
            if (result != null)
            {
                return new ErrorResult(Messages.CardExist);
            }
            return new SuccessResult();
        }
    }
}
