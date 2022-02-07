using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
   public interface IBrandImagesService
   {
       IDataResult<List<BrandImages>> GetAll();
        IResult Add(IFormFile file, BrandImages brandImages);
    }
}
