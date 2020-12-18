using System;

namespace BNPL.Entity
{
    public class OrderHistory
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateTime { get; set; }
        public Status Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
