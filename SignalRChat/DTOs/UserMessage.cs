﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.DTOs
{
    public class UserMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
