using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetById(int id);
        IDataResult<List<CarDetailDto>> GetCarDetailList();
        IDataResult<List<CarDetailDto>> GetCarDetailListByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarDetailListByColorId(int colorId);
        IDataResult<CarDetailDto> GetCarDetailsById(int id);
        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetCarByBrandIdAndByColorId(int brandId, int colorId);
        IDataResult<List<Car>> GetCarsByColorId(int id);
        IDataResult<int> GetCarFindex(int carId);
        IResult Add(Car car);
        IResult Delete(Car car);
        IResult Update(Car car);

        IResult AddTransactionTest(Car car);

        //RESTFUL --> HTTP -->
    }
}
