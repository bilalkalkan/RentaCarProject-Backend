using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface ICreditCardService
   {

       IResult Save(CreditCard card);
       IDataResult<List<CreditCard>> GetCards();
    }
}
