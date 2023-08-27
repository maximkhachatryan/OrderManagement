using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VendorAddress;

namespace OrderMgmnt.Web.Areas.Admin.Models
{
    public class AdminVendorAddressCreateEditModel
    {
        public Guid Id { get; set; }

        [DisplayName("Վարչական շրջան")]
        public AdministrativeDistrict District { get; set; }

        [DisplayName("Փողոց, շենք, տուն, բնակարան")]
        public string AddressInfo { get; set; }
    }
}
