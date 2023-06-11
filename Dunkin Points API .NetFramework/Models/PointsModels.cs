using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dunkin_Points_API.NetFramework.Models
{
    public class PointsModels
    {
        public class CheckBalanceModels
        { 
            public int lang { get; set; }
            public string storePosID { get; set; }
            public int countryCode { get; set; }
            public int ecomID { get; set; }
            public string mobileNumber { get; set; }
            public string loyaltyNumber { get; set; }
            public Boolean isStaging { get; set; }
        }

        public class AddBalanceModels
        {
            public int lang { get; set; }
            public string storePosID { get; set; }
            public int countryCode { get; set; }
            public int ecomID { get; set; }
            public string loyaltyNumber { get; set; }
            public string instoreOrderID { get; set; }
            public decimal orderValue { get; set; }
            public Boolean isStaging { get; set; }
        }

        public class DeductBalanceModels
        {
            public int lang { get; set; }
            public string storePosID { get; set; }
            public int countryCode { get; set; }
            public int ecomID { get; set; }
            public string loyaltyNumber { get; set; }
            public string instoreOrderID { get; set; }
            public decimal orderValue { get; set; }
            public decimal walletcredits { get; set; }
            public Boolean isStaging { get; set; }
        }
    }
}