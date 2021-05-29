using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Entities
{
    public class UserRoom: IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public bool IsActive { get; set; }
    }
}
