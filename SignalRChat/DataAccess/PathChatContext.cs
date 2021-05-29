using Microsoft.EntityFrameworkCore;
using SignalRChat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.DataAccess
{
    public class PathChatContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False
             optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=PathChatDB;Trusted_Connection=true");


        }
        public DbSet<User>  Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UserRoom>  UserRooms { get; set; }
    }
}
