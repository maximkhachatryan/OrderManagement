using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgmnt.DAL.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientAddress { get; set; }
        public DateTime PreferredDate { get; set; }
        public byte State { get; set; }
        public int VenderId { get; set; }


        public Vender Vender { get; set; }
    }
}
