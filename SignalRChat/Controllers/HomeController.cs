
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRChat.DTOs;
using SignalRChat.Entities;
using SignalRChat.Hubs;
using SignalRChat.Models;
using SignalRChat.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        readonly IHubContext<ChatHub> _hubContext;
        private HubService _hubService;
        public HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> hubContext, HubService hubService)
        {
            _hubContext = hubContext;
            
            _hubService = hubService;
            _logger = logger;
            
        }
        async public Task<IActionResult> Index()
        {
            
            
            return View();
        }

      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult GetRooms(int id)
        {
           

            List<UserRoomResponseDTO> rooms = new List<UserRoomResponseDTO>
            {
                new UserRoomResponseDTO { Id=1,Name="Spor",IsActive=true},
                new UserRoomResponseDTO { Id=2,Name="Haber"},
                  new UserRoomResponseDTO { Id=3,Name="Oyun",IsActive=true},
                    new UserRoomResponseDTO { Id=4,Name="Video"},
                    new UserRoomResponseDTO { Id=5,Name="Burçlar"},
                    new UserRoomResponseDTO { Id=6,Name="Magazin",IsActive=true},
            };

           // _hubContext.Clients.All.SendAsync("ReceiveMessage", "hi");
            



            return Json(rooms);
        }
    }
}
