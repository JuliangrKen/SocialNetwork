﻿namespace SocialNetwork.BLL.Models
{
    public class MessageSendingData
    {
        public int SenderId { get; set; }
        public string? RecipientEmail { get; set; }
        public string? Content { get; set; }
    }
}