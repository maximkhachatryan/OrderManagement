using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Areas.Admin.Models
{
    public class AdminVendorListItemModel
    {
        public Guid Id { get; set; }

        [DisplayName("Կոդ")]
        public string Code { get; set; }

        [DisplayName("Անուն")]
        public string Name { get; set; }

        [DisplayName("Բրենդ")]
        public string BrandName { get; set; }

        [DisplayName("Բալանս")]
        public decimal VendorWalletAmount { get; set; }
    }
}
