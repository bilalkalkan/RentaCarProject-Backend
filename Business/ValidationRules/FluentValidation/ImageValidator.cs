using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ImageValidator : AbstractValidator<CarImages>
    {
        public ImageValidator()
        {

            RuleFor(C => C.CarId).NotNull();


        }


    }
}
