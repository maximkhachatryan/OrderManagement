using OrderMgmnt.DAL.Entities;
using OrderMgmnt.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Helpers
{
    public static class OrderStatusHelper
    {
        public static OrderStatus GetOrderStatus(this Order order)
        {
            if (order.SentBackToVendorDate != null)
                return OrderStatus.SentBackToVendor;

            if (order.ClientRejectDate != null)
                return OrderStatus.RejectedByClient;

            if (order.DeliveryEndDate != null)
                return OrderStatus.Delivered;

            if (order.DeliveryStartDate != null)
                return OrderStatus.DeliveryStarted;

            if (order.ActualPickUpDate != null)
                return OrderStatus.PickedUp;

            if (order.RejectDate != null)
                return OrderStatus.Rejected;

            if (order.AcceptDate != null)
                return OrderStatus.Accepted;

            if (order.ClientFillDate != null)
                return OrderStatus.ClientFilled;

            if (order.CreateDate != null)
                return OrderStatus.Created;

            throw new InvalidOperationException("Cannot recognize order status.");
        }
    }
}
