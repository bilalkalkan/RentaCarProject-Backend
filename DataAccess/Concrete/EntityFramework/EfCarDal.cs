using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentaCarContext>, ICarDal
    {
        public CarDetailDto GetCarDetailById(Expression<Func<CarDetailDto, bool>> filter)
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             join color in context.Colors on car.ColorId equals color.ColorId

                             select new CarDetailDto()
                             {
                                 CarId = car.Id,
                                 CarName = car.Description,
                                 BrandId = brand.BrandId,
                                 BrandName = brand.BrandName,
                                 ColorId = color.ColorId,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 CarImages = (from image in context.CarImages where image.CarId == car.Id select image.ImagePath)
                                     .ToList()
                             };
                return result.SingleOrDefault(filter);
            }
        }

       

        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var result = from car in context.Cars
                    join brand in context.Brands on car.BrandId equals brand.BrandId
                    join color in context.Colors on car.ColorId equals color.ColorId
                   
                    select new CarDetailDto()
                    {
                        CarId = car.Id,
                        CarName = car.Description,
                        BrandId = brand.BrandId,
                        BrandName = brand.BrandName,
                        ColorId = color.ColorId,
                        ColorName = color.ColorName,
                        DailyPrice = car.DailyPrice,
                        CarImages = (from image in context.CarImages where image.CarId==car.Id select image.ImagePath).ToList()


                    };

                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }

    }
}

