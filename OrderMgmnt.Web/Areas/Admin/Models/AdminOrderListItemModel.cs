using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Areas.Admin.Models
{
    public class AdminOrderListItemModel
    {
        public Guid Id { get; set; }

        [DisplayName("Ապրանքի նկարագիր")]
        public string ProductDescription { get; set; }

        [DisplayName("Ուղարկող")]
        public string Sender { get; set; }

        [DisplayName("Ստացող")]
        public string Receiver { get; set; }

        [DisplayName("Ստեղծման ամսաթիվ")]
        public DateTime CreationDate { get; set; }
    }
}
