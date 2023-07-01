using OrderMgmnt.Web.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderMgmnt.Web.Areas.Admin.Models
{
    public class AdminOrderDetailsModel
    {
        public Guid OrderId { get; set; }

        //[DisplayName("Պատվերի համար")]
        //public int OrderNo { get; set; }

        [DisplayName("Կարգավիճակ")]
        public OrderStatus Status { get; set; }

        [DisplayName("Հայտը բացվել է")]
        public DateTime OrderCreated { get; set; }

        [DisplayName("Գործընկեր")]
        public string Vender { get; set; }

        [DisplayName("Ապրանքի նկարագիր")]
        public string ProductDescription { get; set; }

        [DisplayName("Վերցման հասցե")]
        public string PickupAddress { get; set; }

        [DisplayName("Նախընտրելի վերցման օր")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DesiredPickupDate { get; set; }

        [DisplayName("Ապրանքի արժեք")]
        public decimal? ProductPrice { get; set; }

        [DisplayName("Առաքման արժեք")]
        public decimal DeliveryPrice { get; set; }

        [DisplayName("Նշումներ")]
        public string VenderNotes { get; set; }


        [DisplayName("Ստացողի հաստատել է")]
        public DateTime? ReceiverAcceptDate { get; set; }

        [DisplayName("Ստացողի անուն/ազգանուն")]
        public string ReceiverName { get; set; }

        [DisplayName("Ստացողի հասցե")]
        public string ReceiverAddress { get; set; }

        [DisplayName("Ստացողի հեռախոսահամար")]
        public string ReceiverPhoneNumber { get; set; }

        [DisplayName("Ստացողի կողմից առաքման օրը փոփոխվել է")]
        public DateTime? ReceiverChangeDeliveryDate { get; set; }

        [DisplayName("Ստացողի նշումներ")]
        public string ReceiverNotes { get; set; }


        [DisplayName("Հաստատվել է")]
        public DateTime? AcceptDate { get; set; }

        [DisplayName("Մերժվել է")]
        public DateTime? RejectDate { get; set; }

        [DisplayName("Վերցվել է")]
        public DateTime? ActualPickUpDate { get; set; }

        [DisplayName("Առաքումը սկսվել է")]
        public DateTime? DeliveryStartDate { get; set; }

        [DisplayName("Առաքումը բարեհաջող ավարտվել է")]
        public DateTime? SuccessfulDeliveryDate { get; set; }

        [DisplayName("Ստացողը մերժել է")]
        public DateTime? ReceiverRejectionDate { get; set; }

        [DisplayName("Մերժված ապրանքը վերադարձվել է գործընկերոջը")]
        public DateTime? ReturnBackToVenderDate { get; set; }
    }
}
