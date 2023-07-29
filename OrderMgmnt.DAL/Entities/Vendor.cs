using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgmnt.DAL.Entities
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string InstagramLink { get; set; }
        public string WebsiteLink { get; set; }

        public decimal VendorWalletAmount { get; set; }

        
        public virtual ICollection<VendorAddress> Addresses { get; set; }

    }
}
