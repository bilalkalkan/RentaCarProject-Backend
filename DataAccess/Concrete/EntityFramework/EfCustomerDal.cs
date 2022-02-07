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
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentaCarContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomersDetails()
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var result = from c in context.Customers
                    join u in context.Users on c.UserId equals u.Id
                    select new CustomerDetailDto
                    {
                        CustomerId = c.UserId,
                        CompanyName = c.CompanyName,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    };
                return result.ToList();
            }
        }
    }
}
