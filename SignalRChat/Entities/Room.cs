﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Entities
{
    public class Room: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
