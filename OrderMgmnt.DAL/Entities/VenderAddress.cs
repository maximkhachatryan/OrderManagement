using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public bool IsRemoved { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual Vender Vender { get; set; }

        public enum AdministrativeDistrict
        {
            [Display(Name = "Կենտրոն")]
            Kentron = 1,
            [Display(Name = "Արաբկիր")]
            Arabkir = 2,
            [Display(Name = "Քանաքեռ-Զեյթուն")]
            QanaqerZeytun = 3,
            [Display(Name = "Ավան")]
            Avan = 4,
            [Display(Name = "Նոր Նորք")]
            NorNork = 5,
            [Display(Name = "Նորք Մարաշ")]
            NorkMarash = 6,
            [Display(Name = "Էրեբունի")]
            Erebuni = 7,
            [Display(Name = "Շենգավիթ")]
            Shengavit = 8,
            [Display(Name = "Նուբարաշեն")]
            Nubarashen = 9,
            [Display(Name = "Մալաթիա-Սեբաստիա")]
            MalatiaSebastia = 10,
            [Display(Name = "Աջափնյակ")]
            Ajapnyak = 11,
            [Display(Name = "Դավթաշեն")]
            Davtashen = 12
        }

    }
}
