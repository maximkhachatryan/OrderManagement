using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Models.Order
{
    public class PlaceOrderRequestDTO
    {
        public string ProductDescription { get; set; }
        public Guid VenderAddressId { get; set; }
        public DateTime PickUpDate { get; set; }
        public bool IsDeliveryPaymentByClient { get; set; }
        public bool ShouldProductPriceBePaid { get; set; }
        public decimal ProductPrice { get; set; }
        public string OtherNotes { get; set; }
    }
}
