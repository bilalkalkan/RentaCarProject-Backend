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
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentaCarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var result = from rent in context.Rentals
                    join car in context.Cars
                        on rent.CarId equals car.Id
                    join brand in context.Brands
                        on car.BrandId equals brand.BrandId
                    join customer in context.Customers
                        on rent.CustomerId equals customer.UserId
                    join user in context.Users
                        on customer.UserId equals user.Id
                    select new RentalDetailDto
                    {
                        Id = rent.Id,
                        BrandName = brand.BrandName,
                        FullName = user.FirstName + " " + user.LastName,
                        RentDate = rent.RentDate,
                        ReturnDate = rent.ReturnDate

                    };
                return result.ToList();

            }
        }
    }
}
