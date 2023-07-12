using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;
namespace AllrideApiService.Configuration
{
    public static class AsimetrikSifreleme
    {

        public static string EncryptText1(string user)
        {
            RSACryptoServiceProvider RSApublicKey = ImportPublicKey1(ReadFromPublicKey());  //publicKey

            var bytesPlainTextData = Encoding.Unicode.GetBytes(user);

            var bytesCypherText = RSApublicKey.Encrypt(bytesPlainTextData, false);

            var cypherText = Convert.ToBase64String(bytesCypherText);

            return cypherText;
        }

        public static string Decryption1(string strText)
        {
            RSACryptoServiceProvider RSAprivateKey = ImportPrivateKey1(ReadFromPrivateKey()); //privateKey
            var bytesPlainTextData = Convert.FromBase64String(strText);   // Gelen şifreli datayı byte dönüştürdük
            bytesPlainTextData = RSAprivateKey.Decrypt(bytesPlainTextData, false); // Rsa da şifre çözümü içn dönüştürdüğümüz byte ı verdik
            var resultBytes = Convert.ToBase64String(bytesPlainTextData); // Çözülen metni program içersinde kullanabilmek için tekrar stringe çevirdik
            return resultBytes;
        }
        public static RSACryptoServiceProvider ImportPrivateKey1(string pem)
        {
            PemReader pr = new(new StringReader(pem));
            var obj = pr.ReadObject();
            var KeyPair = (AsymmetricCipherKeyPair)obj;
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaParams);
            return csp;
        }
        /// <summary>
        /// Import OpenSSH PEM public key string into MS RSACryptoServiceProvider
        /// </summary>
        /// <param name="pem"></param>
        /// <returns></returns>
        public static RSACryptoServiceProvider ImportPublicKey1(string pem)
        {
            PemReader pr = new(new StringReader(pem));
            var obj = pr.ReadObject();
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)obj);

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaParams);
            return csp;
        }
        //public static RSACryptoServiceProvider ImportPublicKey(string pem)
        //{
        //    PemReader pr = new(new StringReader(pem));
        //    var obj = pr.ReadObject();
        //    var publicKey = (AsymmetricKeyParameter)obj;
        //    RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)obj);

        //    RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
        //    csp.ImportParameters(rsaParams);
        //    return csp;
        //}

        public static string ReadFromPrivateKey()
        {
            String privateKeyTxt = "";
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\User\\Desktop\\Keys\\rsa-private.txt");
                //Read the first line of text
                privateKeyTxt = sr.ReadLine();
                //Continue to read until you reach end of file
                while (privateKeyTxt != null)
                {
                    //write the line to console window
                    Console.WriteLine(privateKeyTxt);
                    //Read the next line
                    privateKeyTxt = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return privateKeyTxt;

        }
        public static string ReadFromPublicKey()
        {
            String publicKeyTxt = "";
            String newPublicKey = "";
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\User\\Desktop\\Keys\\rsa-public.txt");
                //Read the first line of text
                publicKeyTxt = sr.ReadLine();
                //Continue to read until you reach end of file
                while (publicKeyTxt != null)
                {
                    newPublicKey += publicKeyTxt;
                    //Read the next line
                    publicKeyTxt = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return newPublicKey;
        }

    }
}
/*
 * 
            // Convert the text to an array of bytes   
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = byteConverter.GetBytes(text);

            // Create a byte array to store the encrypted data in it   
            byte[] encryptedData;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Set the rsa pulic key   
                rsa.FromXmlString(publicKey);

                // Encrypt the data and store it in the encyptedData Array   
                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }
            // Save the encypted data array into a file   
            //var response = File.WriteAllBytes(fileName, encryptedData);
            return byteConverter.GetString(encryptedData);
        }
 * 
 * 
 */
/*
 * 
 * 
 * CASE 
 *  public static string EncryptText( string text)
        {
            // Convert the text to an array of bytes   
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            var publicKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var testData = byteConverter.GetBytes(text);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    // client encrypting data with public key issued by server                    
                    rsa.FromXmlString(publicKey.ToString());

                    var encryptedData = rsa.Encrypt(testData, true);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string Decryption(string strText)
        {
            var privateKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";

            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var base64Encrypted = strText;

                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKey);

                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }*/