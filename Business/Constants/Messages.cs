using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarNotFound = "Bu özelliklere ait araba bulunmamaktadır";
        public static string CarAdded = "Araba eklendi";
        public static string CarnotAdded = "Araba eklenmedi";
        public static string CarDeleted = "Araba silindi";
        public static string CarnotDeleted = "Araba silinemedi";
        public static string CarsListed = "Arabalar listelendi";
        public static string CarUpdated = "Araba güncellendi";
        public static string CarnotUpdated = "Araba güncellenemedi";



        public static string BrandAdded = "Marka eklendi";
        public static string BrandnotAdded = "Marka eklenemedi";
        public static string BrandDeleted = "Marka silindi";
        public static string BrandnotDeleted = "Marka silinemedi";
        public static string BrandUpdated = "Marka Güncellendi";
        public static string BrandnotUpdated = "Marka Güncellenemedi";
        public static string BrandsListed = "Markalar listelendi";



        public static string ColorAdded = "Renk eklendi";
        public static string ColornotAdded = "Renk eklenemedi";
        public static string ColorDeleted = "Renk silindi";
        public static string ColornotDeleted = "Renk silinemedi";
        public static string ColorUpdated = "Renk Güncellendi";
        public static string ColornotUpdated = "Renk Güncellenemedi";
        public static string ColorsListed = "Renkler listelendi";

        public static string MaintenanceTime = "Sistem bakımda";

        public static string UserAdded = "Kullanıcı eklendi";
        public static string UsernotAdded = "Kullanıcı eklenemedi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string UsernotDeleted = "Kullanıcı silinemedi";
        public static string UsersListed = "Kullanıcılar listelendi";
        public static string UserUpdated = "Kullanıcı güncellendi";
        public static string UsernotUpdated = "Kullanıcı güncellenemedi";


        public static string CustomerAdded = "Müşteri eklendi";
        public static string CustomernotAdded = "Müşteri eklenemedi";
        public static string CustomerDeleted = "Müşteri silindi";
        public static string CustomernotDeleted = "Müşteri silinemedi";
        public static string CustomersListed = "Müşteriler listlendi";
        public static string CustomerUpdated = "Müşteri güncellendi";
        public static string CustomernotUpdated = "Müşteriler güncellenemedi";


        public static string CarRented = "Araba kiralandı";
        public static string CarnotRented = "Arabanın kiralanabilmesi için arabanın teslim edilmesi gerekmektedir";

        public static string RentalDeleted = "Kiralama silindi";
        public static string ImageCountOfCarError = "Arabanın en fazla 5 görseli olabilir";
        public static string ImagesListed = "Görseller listelendi";

        public static string ImageAdded = "Görsel eklendi";

        public static string ImageDeleted = "Görsel silindi";
        public static string ImageNotFound = "Görsel bulunamadı";
        public static string ImageUpdated = "Görsel güncellendi";

        public static string AuthorizationDenied = "Yetkiniz yok.";

        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
        public static string NoRegisteredCars = "Bu Markaya ait araba yok";
        public static string NoRegistered = "Bu renge kayıtlı araba yok";

        public static string GetCar = "Araba getirildi";

        public static string Paymented { get; internal set; }
        public static string PaymentDeleted { get; set; }
        public static string PaymentListed { get; set; }
        public static string GetPaymented { get; set; }
        public static string PaymentUpdated { get; internal set; }
        public static string ImageCountOfBrandError { get; internal set; }
        public static string addedCard { get; internal set; }
        public static string deletedCard { get; set; }
        public static string paymentoccurred { get; internal set; }
        public static string cardAdded { get; internal set; }
        public static string CardExist { get; set; }
        public static string CreditCardNotFound { get; set; }
        public static string creditCardDeleted { get; internal set; }
        public static string CarNotExists { get; internal set; }
        public static string CarBusy { get; internal set; }
    }
}
