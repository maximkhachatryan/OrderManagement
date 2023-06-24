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
        public DateTime DesiredPickUpDate { get; set; }
        public bool IsDeliveryPaymentByClient { get; set; }
        public bool ShouldProductPriceBePaid { get; set; }
        public decimal? ProductPrice { get; set; }
        public string OtherNotes { get; set; }

        public DateTime CreateDate { get; set; }

        /// <summary>
        /// This field will be set when the client fills the delivery details
        /// </summary>
        public DateTime? ClientFillDate { get; set; }

        /// <summary>
        /// This field will be set when the operator accepts the order
        /// </summary>
        public DateTime? AcceptDate { get; set; }

        /// <summary>
        /// This field will be set when the operator rejects the order
        /// </summary>
        public DateTime? RejectDate { get; set; }

        /// <summary>
        /// This field will be set when the product will be brought to the office
        /// </summary>
        public DateTime? ActualPickUpDate { get; set; }

        /// <summary>
        /// This field will be set when the courier takes the product and leaves the office
        /// </summary>
        public DateTime? DeliveryStartDate { get; set; }

        /// <summary>
        /// This field will be set when the courier delivers the product and returns back to the office
        /// </summary>
        public DateTime? DeliveryEndDate { get; set; }

        /// <summary>
        /// This field will be set when the courier brings the product back to the office because of client rejection
        /// </summary>
        public DateTime? ClientRejectDate { get; set; }

        /// <summary>
        /// This field will be set when the client rejected product is returned back to the vender
        /// </summary>
        public DateTime? SentBackToVenderDate { get; set; }


        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientAddress { get; set; }
        public DateTime? ClientChangeDeliveryDate { get; set; }
        public string ClientNotes { get; set; }


        public VenderAddress VenderAddress { get; set; }
    }
}
