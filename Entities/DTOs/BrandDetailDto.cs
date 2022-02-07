﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.DTOs
{
   public class BrandDetailDto:IDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandImages { get; set; }
    }
}
