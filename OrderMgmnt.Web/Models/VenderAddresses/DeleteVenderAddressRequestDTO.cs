using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VenderAddress;

namespace OrderMgmnt.Web.Models.VenderAddresses
{
    public class DeleteVenderAddressRequestDTO
    {
        public Guid Id { get; set; }
    }
}
