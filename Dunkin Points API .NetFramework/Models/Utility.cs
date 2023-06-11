using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Dunkin_Points_API.NetFramework.Models
{
    public static class Utility
    {
        //*******************************Encryption functions******************************************

        public static Encoding iso = Encoding.UTF8;
        public static string blowfishkey = "51632913932367811";
        public static string blowfishIV = "dtqnexqu";
        public static string webkey = "eySLkm@2qd";
        public static string EncryptBlowFish(string texttoencrypt)
        {
            BlowfishEngine blowfishEngine = new BlowfishEngine();
            CbcBlockCipher cbcBlockCipher = new CbcBlockCipher(blowfishEngine);

            KeyParameter key = new KeyParameter(Encoding.ASCII.GetBytes(blowfishkey));
            ParametersWithIV IV = new ParametersWithIV(key, Encoding.ASCII.GetBytes(blowfishIV));
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(cbcBlockCipher);
            cipher.Init(true, IV);
            byte[] indata = iso.GetBytes(texttoencrypt);
            int size = cipher.GetOutputSize(indata.Length);
            byte[] result = new byte[size];
            int olen = cipher.ProcessBytes(indata, 0, indata.Length, result, 0);
            olen += cipher.DoFinal(result, olen);
            if (olen <= size)
            {
                byte[] tmp = new byte[olen];
                System.Buffer.BlockCopy(result, 0, tmp, 0, olen);
                result = tmp;
            }
            string encrypted = Convert.ToBase64String(result);
            return encrypted;
        }


        public static string DycryptBlowFish(string texttodecrypt)
        {
            byte[] plaintext = Convert.FromBase64String(texttodecrypt);
            string decrypted = DycryptBlowFish(plaintext);
            return decrypted;
        }

        public static string DycryptBlowFish(byte[] plaintext)
        {
            BlowfishEngine blowfishEngine = new BlowfishEngine();
            CbcBlockCipher cbcBlockCipher = new CbcBlockCipher(blowfishEngine);
            KeyParameter key = new KeyParameter(iso.GetBytes(blowfishkey));
            //gcwicrge
            ParametersWithIV IV = new ParametersWithIV(key, iso.GetBytes(blowfishIV));
            PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(cbcBlockCipher);
            cipher.Init(false, IV);
            int size = cipher.GetOutputSize(plaintext.Length);
            byte[] result = new byte[size];
            int olen = cipher.ProcessBytes(plaintext, 0, plaintext.Length, result, 0);
            olen += cipher.DoFinal(result, olen);

            if (olen <= size)
            {
                byte[] tmp = new byte[olen];
                System.Buffer.BlockCopy(result, 0, tmp, 0, olen);
                result = tmp;
            }
            string decrypted = iso.GetString(result);
            return decrypted;
        }


        public static string BasicAuth()
        {
            var username = "load";
            var password = "load";
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(username + ":" + password));
            return (encoded);
        }



        //*******************************Auth headers******************************************
        public static string getAuthHeader(string verb, string contenttype, string functionname)
        {
            string ticks = Math.Round((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds, 0).ToString();
            string encrticks = EncryptBlowFish(ticks);
            string datetimestr = encrticks;
            string checksumformat = Convert.ToBase64String(iso.GetBytes(createSignature(verb, contenttype, functionname)));
            string mystr = datetimestr + ":" + checksumformat;
            var ToBase64String  = Convert.ToBase64String(Encoding.ASCII.GetBytes(mystr));
            return ToBase64String; //Please note this function retuens        
        }

        private static string createSignature(string verb, string contenttype, string functionname)
        {
            string httpmethod = verb; // HttpContext.Current.Request.HttpMethod.ToLower();
            //string contenttype = HttpContext.Current.Request.ContentType.ToLower();
            string concat_string = webkey + httpmethod + contenttype + functionname;
            string hashedstring = Regex.Replace(EncodePassword(concat_string), "-", "");
            return hashedstring;
        }

        private static string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        //string authHeader = getAuthHeader("post", "application/json", "checkbalance");
        //string authHeader = getAuthHeader("post", "application/json", "addbalance");
        //string authHeader = getAuthHeader("post", "application/json", "deductbalance");

        ////The above function gives username/password concatinated with colon (:). This should be further converted into base64string and added as basic authentication
        ////Depending on how the call is made and if any library is used, you might have to pass username/password seperately or pass the base64 concatenated string.

        //objRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(auth)));









        //============================================DATA======================================================

        //       CheckBalanceParams obj4 = new CheckBalanceParams();
        //       obj4.lang = 1;
        //obj4.ecomID = 2;
        //obj4.loyaltyNumber = "%3005635?";
        //obj4.isStaging = false;
        //obj4.countryCode = 1;
        //obj4.storePosID = "1";

        //var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj4);
        //       string enc = clsCommon.EncryptBlowFish(json);

        //       AddBalanceParams obj = new AddBalanceParams();
        //       obj.lang = 1;
        //obj.ecomID = 2;
        //obj.isStaging = false;
        //obj.loyaltyNumber = "%3005635?";
        //obj.countryCode = 1;
        //obj.storePosID = "1";
        //obj.orderValue = 2;
        //obj.instoreOrderID = "1";

        //json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj);
        //       enc = clsCommon.EncryptBlowFish(json);

        //DeductBalanceParams obj1 = new DeductBalanceParams();
        //       obj1.lang = 1;
        //obj1.ecomID = 2;
        //obj1.isStaging = false;
        //obj1.loyaltyNumber = "%3005635?";
        //obj1.countryCode = 1;
        //obj1.storePosID = "1";
        //obj1.orderValue = 1;
        //obj1.instoreOrderID = "1";

        //json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj1);
        //       enc = clsCommon.EncryptBlowFish(json);







    }
}