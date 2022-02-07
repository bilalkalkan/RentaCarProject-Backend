using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal : EfEntityRepositoryBase<Brand, RentaCarContext>, IBrandDal
    {
        public List<BrandDetailDto> GetBrandDetails()
        {
            using (RentaCarContext context=new RentaCarContext())
            {
                var result = from brand in context.Brands
                    join brandImages in context.BrandImages on brand.BrandId equals brandImages.Id
                    select new BrandDetailDto()
                    {
                        BrandId = brand.BrandId,
                        BrandName = brand.BrandName,
                        BrandImages = brandImages.ImagePath
                    };

                return result.ToList();


            }
        }
    }
}
