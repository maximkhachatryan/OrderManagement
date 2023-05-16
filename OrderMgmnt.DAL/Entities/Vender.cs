using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgmnt.DAL.Entities
{
    public class Vender
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string InstagramLink { get; set; }
        public string WebsiteLink { get; set; }

        public decimal VenderWalletAmount { get; set; }

        
        public virtual ICollection<VenderAddress> Addresses { get; set; }

    }
}
