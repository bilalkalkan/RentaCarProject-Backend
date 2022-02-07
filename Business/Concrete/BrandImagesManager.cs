using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class BrandImagesManager : IBrandImagesService
    {
        private IBrandImagesDal _brandImagesDal;

        public BrandImagesManager(IBrandImagesDal brandImagesDal)
        {
            _brandImagesDal = brandImagesDal;
        }

        public IResult Add(IFormFile file, BrandImages brandImages)
        {
            IResult result = BusinessRules.Run(CheckIfCountofImagesoftheBrand(brandImages.BrandId));

            if (result != null)
            {
                return result;
            }
            var ImageResult = FileHelper.Upload(file);
            if (!ImageResult.Success)
            {
                return new ErrorResult(ImageResult.Message);
            }

            brandImages.ImagePath = ImageResult.Message;

            _brandImagesDal.Add(brandImages);
            return new SuccessResult(Messages.ImageAdded);
        }

        public IDataResult<List<BrandImages>> GetAll()
        {
            return new SuccessDataResult<List<BrandImages>>(_brandImagesDal.GetAll());
        }


        private IResult CheckIfCountofImagesoftheBrand(int brandId)
        {
            var result = _brandImagesDal.GetAll(p=>p.BrandId==brandId).Count;
            if (result>1)
            {
                return new ErrorResult(Messages.ImageCountOfBrandError);
            }

            return new SuccessResult();
        }
    }
}
