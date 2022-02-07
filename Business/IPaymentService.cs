using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business
{
   public interface IPaymentService
   {
       IResult Pay(CreditCard card);
   }
}
