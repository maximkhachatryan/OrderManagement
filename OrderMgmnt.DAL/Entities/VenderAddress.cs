using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMgmnt.DAL.Entities
{
    public class VenderAddress
    {
        public Guid Id { get; set; }
        public AdministrativeDistrict District { get; set; }
        public string AddressInfo { get; set; }


    public enum AdministrativeDistrict
        {
            Kentron = 1,
            Arabkir = 2,
            QanaqerZeytun = 3,
            Avan = 4,
            NorNork = 5,
            NorkMarash = 6,
            Erebuni = 7,
            Shengavit = 8,
            Nubarashen = 9,
            MalatiaSebastia = 10,
            Ajapnyak = 11,
            Davtashen = 12
        }

    }
}
