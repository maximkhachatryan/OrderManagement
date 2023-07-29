using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VendorAddress;

namespace OrderMgmnt.Web.Models.VendorAddresses
{
    public class CreateVendorAddressRequestDTO
    {
        public AdministrativeDistrict District { get; set; }
        public string AddressInfo { get; set; }
    }
}
