using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgmnt.DAL.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string ProductDescription { get; set; }
        public DateTime PickUpDate { get; set; }
        public bool IsDeliveryPaymentByClient { get; set; }
        public bool ShouldProductPriceBePaid { get; set; }
        public decimal ProductPrice { get; set; }
        public string OtherNotes { get; set; }

        public DateTime? CreateDate { get; set; }
        

        public DateTime? OrderFinishDate { get; set; }
        public DateTime? OrderCancelDate { get; set; }
        public Guid VenderId { get; set; }

        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientAddress { get; set; }
        public DateTime? ClientPreferredDate { get; set; }
        public DateTime? ClientFillDate { get; set; }

        public VenderAddress VenderAddress { get; set; }
    }
}
