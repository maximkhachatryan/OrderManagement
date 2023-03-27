using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Models.PlaceOrder
{
    public class OrderModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliverAddress { get; set; }
        public DateTime PreferedDate { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
