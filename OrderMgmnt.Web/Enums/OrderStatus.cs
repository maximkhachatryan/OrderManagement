using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Enums
{
    public enum OrderStatus
    {
        Created = 0,
        ClientFilled = 1,
        Accepted = 2,
        Rejected = 3,
        PickedUp = 4,
        DeliveryStarted = 5,
        Delivered = 6,
        RejectedByClient = 7,
        SentBackToVender = 8
    }
}
