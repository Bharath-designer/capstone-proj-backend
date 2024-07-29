﻿using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int ConversationId { get; set; } // Foreign Key
        public Conversation Conversation { get; set; }

    }
}
