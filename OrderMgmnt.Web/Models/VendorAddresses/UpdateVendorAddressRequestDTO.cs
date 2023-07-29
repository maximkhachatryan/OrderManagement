using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VendorAddress;

namespace OrderMgmnt.Web.Models.VendorAddresses
{
    public class UpdateVendorAddressRequestDTO
    {
        public Guid Id { get; set; }
        public AdministrativeDistrict District { get; set; }
        public string AddressInfo { get; set; }
    }
}
