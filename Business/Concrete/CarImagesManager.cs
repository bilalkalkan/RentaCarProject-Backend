using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofact.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImagesManager : ICarImagesService
    {

        private ICarImagesDal _carImagesDal;

        public CarImagesManager(ICarImagesDal carImage)
        {
            _carImagesDal = carImage;
        }

        [ValidationAspect(typeof(ImageValidator))]
        public IResult Add(IFormFile file, CarImages carImages)
        {
            IResult result = BusinessRules.Run(CheckIfCountofImagesoftheCar(carImages.CarId));

            if (result != null)
            {
                return result;
            }
            var ImageResult = FileHelper.Upload(file);
            if (!ImageResult.Success)
            {
                return new ErrorResult(ImageResult.Message);
            }

            carImages.ImagePath = ImageResult.Message;

            _carImagesDal.Add(carImages);
            return new SuccessResult(Messages.ImageAdded);
        }

        public IResult Delete(CarImages carImage)
        {
            var Image = _carImagesDal.Get(p => p.Id == carImage.Id);
            if (Image == null)
            {
                return new ErrorResult(Messages.ImageNotFound);
            }
            var ImageResult = FileHelper.Delete(carImage.ImagePath);
            _carImagesDal.Delete(carImage);
            return new SuccessResult(Messages.ImageDeleted);
        }

        public IDataResult<List<CarImages>> GetAll()
        {

            return new SuccessDataResult<List<CarImages>>(_carImagesDal.GetAll(), Messages.ImagesListed);
        }

        public IDataResult<CarImages> GetById(int id)
        {
            return new SuccessDataResult<CarImages>(_carImagesDal.Get(p => p.Id == id));
        }

        public IDataResult<List<CarImages>> GetImagesByCarId(int carId)
        {
            IResult result = BusinessRules.Run(CheckCarImageNull(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarImages>>(result.Message);
            }
            return new SuccessDataResult<List<CarImages>>(CheckCarImageNull(carId).Data);
        }

        [ValidationAspect(typeof(ImageValidator))]
        public IResult Update(IFormFile file, CarImages carImage)
        {
            var isImage = _carImagesDal.Get(p => p.Id == carImage.Id);
            if (isImage == null)
            {
                return new ErrorResult(Messages.ImageNotFound);
            }
            var updateFile = FileHelper.Update(file, isImage.ImagePath);
            if (updateFile.Success)
            {
                return new ErrorResult(updateFile.Message);
            }
            _carImagesDal.Update(carImage);
            return new SuccessResult(Messages.ImageUpdated);
        }

        private IResult CheckIfCountofImagesoftheCar(int carId)
        {
            var result = _carImagesDal.GetAll(c => c.CarId == carId).Count;
            if (result > 5)
            {
                return new ErrorResult(Messages.ImageCountOfCarError);
            }
            return new SuccessResult();
        }

        private IDataResult<List<CarImages>> CheckCarImageNull(int id)
        {
            try
            {
                string path = @"\images\logo.jpg";
                var result = _carImagesDal.GetAll(c => c.CarId == id).Any();
                if (!result)
                {
                    List<CarImages> carImage = new List<CarImages>();
                    carImage.Add(new CarImages() { CarId = id, ImagePath = path, Date = DateTime.Now });
                }
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<CarImages>>(ex.Message);
            }

            return new SuccessDataResult<List<CarImages>>(_carImagesDal.GetAll(p => p.CarId == id).ToList());
        }


    }
}
