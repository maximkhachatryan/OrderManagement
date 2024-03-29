﻿using OrderMgmnt.Web.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VendorAddress;

namespace OrderMgmnt.Web.Areas.Admin.Models
{
    public class AdminOrderListItemModel
    {
        public Guid Id { get; set; }

        [DisplayName("Պատվերի կոդ")]
        public int OrderCode { get; set; }

        [DisplayName("Ապրանքի նկարագիր")]
        public string ProductDescription { get; set; }

        [DisplayName("Ուղարկող")]
        public string Sender { get; set; }

        [DisplayName("Ուղարկողի վարչական շրջան")]
        public AdministrativeDistrict SenderDistrict { get; set; }

        [DisplayName("Ստացող")]
        public string Receiver { get; set; }

        [DisplayName("Ստացողի վարչական շրջան")]
        public AdministrativeDistrict? ReceiverDistrict { get; set; }

        [DisplayName("Կարգավիճակ")]
        public OrderStatus Status { get; set; }

        [DisplayName("Ստեղծման ամսաթիվ")]
        public DateTime CreationDate { get; set; }
    }
}
