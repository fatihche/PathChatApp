using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.DTOs
{
    public class UserRoomResponseDTO
    {
        public UserRoomResponseDTO()
        {
            UserMessages = new List<UserMessage>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<UserMessage> UserMessages { get; set; }
    }
}
