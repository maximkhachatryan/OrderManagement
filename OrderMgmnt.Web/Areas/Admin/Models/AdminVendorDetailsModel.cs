using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VendorAddress;

namespace OrderMgmnt.Web.Areas.Admin.Models
{
    public class AdminVendorDetailsModel
    {
        public Guid Id { get; set; }

        [DisplayName("Կոդ")]
        public string Code { get; set; }

        [DisplayName("Անուն")]
        public string Name { get; set; }

        [DisplayName("Բրենդ")]
        public string BrandName { get; set; }

        [DisplayName("Հեռախոսահամար 1")]
        public string PhoneNumber1 { get; set; }

        [DisplayName("Հեռախոսահամար 2")]
        public string PhoneNumber2 { get; set; }

        [DisplayName("Instagram-ի հասցե")]
        public string InstagramLink { get; set; }

        [DisplayName("Վեբ կայքի հասցե")]
        public string WebsiteLink { get; set; }

        [DisplayName("Բալանս")]
        public decimal VendorWalletAmount { get; set; }

        [DisplayName("Հասցեներ")]
        public IEnumerable<AdminVendorAddressDetailsModel> Addresses { get; set; }


        public class AdminVendorAddressDetailsModel
        {
            public Guid Id { get; set; }

            [DisplayName("Վարչական շրջան")]
            public AdministrativeDistrict District { get; set; }

            [DisplayName("Հասցե")]
            public string AddressInfo { get; set; }
        }
    }
}
