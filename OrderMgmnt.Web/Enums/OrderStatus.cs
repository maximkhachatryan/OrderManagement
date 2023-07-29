using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Ստեղծված")]
        Created = 0,
        [Display(Name = "Լրացված")]
        ClientFilled = 1,
        [Display(Name = "Հաստատված")]
        Accepted = 2,
        [Display(Name = "Մերժված")]
        Rejected = 3,
        [Display(Name = "Վերցված")]
        PickedUp = 4,
        [Display(Name = "Առաքումը սկսված")]
        DeliveryStarted = 5,
        [Display(Name = "Հաջողությամբ առաքված")]
        Delivered = 6,
        [Display(Name = "Հաճախորդի կողմից մերժված")]
        RejectedByClient = 7,
        [Display(Name = "Գործընկերոջը վերադարձված")]
        SentBackToVendor = 8
    }
}
