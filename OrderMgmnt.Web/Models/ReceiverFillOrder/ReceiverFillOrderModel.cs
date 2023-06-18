using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Models.ReceiverFillOrder
{
    public class ReceiverFillOrderModel : IValidatableObject
    {
        public Guid OrderId { get; set; }
        public string ProductDescription { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string Notes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (string.IsNullOrWhiteSpace(ReceiverName))
            {
                results.Add(new ValidationResult("Ստացողի Անուն/Ազգանունը լրացված չէ", new[] { nameof(ReceiverName) }));
            }

            if (string.IsNullOrWhiteSpace(ReceiverAddress))
            {
                results.Add(new ValidationResult("Ստացողի հասցեն լրացված չէ", new[] { nameof(ReceiverAddress) }));
            }

            if (string.IsNullOrWhiteSpace(ReceiverPhoneNumber))
            {
                results.Add(new ValidationResult("Ստացողի հեռախոսահամարը լրացված չէ", new[] { nameof(ReceiverPhoneNumber) }));
            }

            return results;
        }
    }
}
