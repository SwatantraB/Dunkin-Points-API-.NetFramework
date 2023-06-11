using Dunkin_Points_API.NetFramework.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using static Dunkin_Points_API.NetFramework.Models.PointsModels;

namespace Dunkin_Points_API.NetFramework.Controllers
{
    public class PointsController : ApiController
    {
        [Route("Api/CheckBalance")]
        public async Task<object> CheckBalance(CheckBalanceModels checkbalancemodels)
        {
            var json = JsonConvert.SerializeObject(checkbalancemodels);
            string encryptedtext = Utility.EncryptBlowFish(json);
            var response = await Services.PointsAPI("/PointsAPI/profile.svc/checkbalance", encryptedtext, "Post", "application/json", "checkbalance");

            var jsonData = JsonConvert.DeserializeObject(response.ToString());
            jsonData = JsonConvert.DeserializeObject(jsonData.ToString());

            return  new { encryptedtext, jsonData };
        }

        [Route("Api/AddBalance")]
        public async Task<object> AddBalance(AddBalanceModels addbalancemodels)
        {
            var json = JsonConvert.SerializeObject(addbalancemodels);
            string encryptedtext = Utility.EncryptBlowFish(json);
            var response = await Services.PointsAPI("/PointsAPI/profile.svc/addbalance", encryptedtext, "Post", "application/json", "checkbalance");

            var jsonData = JsonConvert.DeserializeObject(response.ToString());
            jsonData = JsonConvert.DeserializeObject(jsonData.ToString());

            return new { encryptedtext, jsonData };
        }

        [Route("Api/DeductBalance")]
        public async Task<object> DeductBalance(DeductBalanceModels deductbalancemodels)
        {
            var json = JsonConvert.SerializeObject(deductbalancemodels);
            string encryptedtext = Utility.EncryptBlowFish(json);
            var response = await Services.PointsAPI("/PointsAPI/profile.svc/deductbalance", encryptedtext, "Post", "application/json", "deductbalance");

            var jsonData = JsonConvert.DeserializeObject(response.ToString());
            jsonData = JsonConvert.DeserializeObject(jsonData.ToString());

            return new { encryptedtext, jsonData };
        }

        [Route("Api/AllPointsApiTest")]
        [HttpGet]
        public List<object> AllPointsApiTest()
        {
            List<object> collection = new List<object>();
            List<string> data = new List<string>();

            CheckBalanceModels obj4 = new CheckBalanceModels();
            obj4.lang = 1;
            obj4.ecomID = 2;
            obj4.loyaltyNumber = "%1379239?";
            obj4.isStaging = false;
            obj4.countryCode = 1;
            obj4.storePosID = "1";
            obj4.mobileNumber = "0555769022";

            //var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj4);
            var json = JsonConvert.SerializeObject(obj4);
            string encryptedtext = Utility.EncryptBlowFish(json);
            data.Add(encryptedtext);
            var responce = Services.PointsAPI("/PointsAPI/profile.svc/checkbalance", encryptedtext, "Post", "application/json", "checkbalance");
            collection.Add(responce.Result);

            AddBalanceModels obj = new AddBalanceModels();
            obj.lang = 1;
            obj.ecomID = 2;
            obj.isStaging = false;
            obj.loyaltyNumber = "%1379239?";
            obj.countryCode = 1;
            obj.storePosID = "1";
            obj.orderValue = 2252555;
            obj.instoreOrderID = "1";


            //json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj);
            json = JsonConvert.SerializeObject(obj);
            encryptedtext = Utility.EncryptBlowFish(json);
            data.Add(encryptedtext);
            responce = Services.PointsAPI("https://productiondd.buzzparade.com/PointsAPI/profile.svc/addbalance", encryptedtext, "Post", "application/json", "addbalance");
            collection.Add(responce.Result);

            DeductBalanceModels obj1 = new DeductBalanceModels();
            obj1.lang = 1;
            obj1.ecomID = 2;
            obj1.isStaging = false;
            obj1.loyaltyNumber = "%1379239?";
            obj1.countryCode = 1;
            obj1.storePosID = "1";
            obj1.orderValue = 1;
            obj1.instoreOrderID = "1";

            //json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj1);
            json = JsonConvert.SerializeObject(obj1);
            encryptedtext = Utility.EncryptBlowFish(json);
            data.Add(encryptedtext);
            responce = Services.PointsAPI("https://productiondd.buzzparade.com/PointsAPI/profile.svc/deductbalance", encryptedtext, "Post", "application/json", "deductbalance");
            collection.Add(responce.Result);

            return collection;
        }


        [Route("Api/DycryptBlowFish")]
        [HttpGet]
        public List<string> DycryptBlowFish()
        {
            List<string> collection = new List<string>();
            List<string> Responce = new List<string>();
            //deductbalance
            collection.Add("eENHfd3g1OecdqLQNnBvsVK3McosXo0q1PQuC4+1TKROVIDVMKKBZ8SCW/5gnd1IQPFOFndmnVFbqGPevi9uWZ8IIhNMk+N8IX1+8E68WlUqps+kkIqbopWspsut5JYxYauSqBl9hikERzopj8eXiUH+yAM0hEH02jRRDl1Ve5YMDXrv+Yr0vaUQVl7PdrpOl2DoKkJux5LmKHsgMY0X6TYbw/R5hJ+o");
            //checkbalance
            collection.Add("eENHfd3g1OecdqLQNnBvsWNXv/to+qH9x3guKlLH1wXkEvILf+4N0PhvhREO7OZUsAlxx46YmiqRk+XhXCoSpzxd816IXh1V71pSrSThNpDWPKfOO7Cti1nZHXfJ3gyuHE6qfNlz8l0=");
            //addbalance
            collection.Add("DseHEJwiy1PFoCLa3V4NdqKgz+NyMAx8QSvJBvjyx8sRz9CXTt3/10lv7ZlvA9hOCJhbS+JSoWzX35TvUJMZfrb/CF2vv4L6Vfm24vuHM9GXI6JqPfREKpHAztiwDwxrEddnUpCfPq/sVvvLyT1EblcWV7Yv+sirFY+/3Dj0xT7nO1Bi3sRohUG74O0XCClaMnw4AP9l8TD6TspVozZuHbfftXwcsiye");
            foreach (var item in collection)
            {
                var data = Utility.DycryptBlowFish(item);
                Responce.Add(data);
            }
            return Responce;
        }

        [Route("Api/EncryptBlowFish")]
        [HttpGet]
        public async void EncryptBlowFish()
        {
            var dda = "{\"ecomID\":2,\"isStaging\":false,\"loyaltyNumber\":\"%1379239?\",\"countryCode\":1,\"storePosID\":\"10163\",\"orderValue\":13.0000000000,\"instoreOrderID\":\"10163-10163-350537\"}";
            Utility.EncryptBlowFish(dda);
        }
    }
}
