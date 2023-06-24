using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Ստեղծված է")]
        Created = 0,
        [Display(Name = "Լրացված է")]
        ClientFilled = 1,
        [Display(Name = "Հաստատված է")]
        Accepted = 2,
        [Display(Name = "Մերժված է")]
        Rejected = 3,
        [Display(Name = "Վերցված է")]
        PickedUp = 4,
        [Display(Name = "Առաքումը սկսված է")]
        DeliveryStarted = 5,
        [Display(Name = "Հաջողությամբ առաքված է")]
        Delivered = 6,
        [Display(Name = "Մերժված է հաճախորդի կողմից")]
        RejectedByClient = 7,
        [Display(Name = "Վերադարձված է գործընկերոջը")]
        SentBackToVender = 8
    }
}
