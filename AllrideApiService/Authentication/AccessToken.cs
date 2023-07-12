using System.Security.Claims;

namespace AllrideApiService.Authentication
{
    public class AccessToken
    {
        ///*
        // * Login endpointi çağrıldığında, kullanıcı girişi başarılı olduğu durumda döneceğim  token da  ihtiyacım olan geriye dönülecek verileri dönen objedir
        // */
        //public string Token { get; set; }
        //public string RefreshToken { get; set; }
        /////  Claims elemanı  nesne tipinden bir koleksiyon görevi görmekte ve tokenda tutulacak  tüm harici verileri üzerinden generate sürecine dahil etme görevini üstlenmektedir.        
        //public Claim[] Claims { get; set; }
        ////Token oluşturulurken kullanılacak secret key değerini taşımaktadır.
        //public string SecurityKey { get; set; }
        ////Oluşturulacak token değerinin aktiflik süresini tutacak olan elemandır.
        //public DateTime ExpireDate { get; set; }
        public string SecurityKey { get; set; }
  
    }
}
