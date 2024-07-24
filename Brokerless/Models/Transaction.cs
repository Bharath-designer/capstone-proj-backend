﻿using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class Transaction
    {
        [Key]
        public string TransactionId { get; set; }
        public Currency Currency { get; set; }
        public double Amount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int SubscriptionTemplateId { get; set; } // Foreign Key
        public int UserId { get; set; } // Foreign Key
        public SubscriptionTemplate SubscriptionTemplate { get; set; }
        public User User { get; set; }
    }
}
