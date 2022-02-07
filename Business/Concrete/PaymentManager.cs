using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Concrete
{
   public class PaymentManager:IPaymentService
    {
        public IResult Pay(CreditCard card)
        {
            return new SuccessResult(Messages.paymentoccurred);
        }
    }
}
