using RTools_NTS.Util;

namespace AllrideApiService.Configuration
{
   // Token ile ilgili validasyon yapılanmasında kullanılacak key değerini “appsettings.json” dosyasından okuyabilmek ve bunu “TokenOption” isimli bir sınıf üzerinden gerçekleştirebilmek için yapılan klasik konfigürasyon çalışmasıdır.
    public class TokenOption
    {   
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
