using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImagesService
    {
        IDataResult<List<CarImages>> GetAll();
        IDataResult<CarImages> GetById(int id);
        IDataResult<List<CarImages>> GetImagesByCarId(int carId);
        IResult Add(IFormFile file, CarImages images);
        IResult Delete(CarImages carImage);
        IResult Update(IFormFile file, CarImages carImage);
    }
}
