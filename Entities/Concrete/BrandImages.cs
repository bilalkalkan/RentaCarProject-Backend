using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
   public class BrandImages:IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        public BrandImages()
        {
            Date = DateTime.Now;
        }
    }
}
