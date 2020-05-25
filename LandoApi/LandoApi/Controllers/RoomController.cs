using LandoApi.Models;
using LandoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandoApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        // private readonly HotelApiDbContext _context;
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet(Name = nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{roomId}",Name =nameof(GetRoomById))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Room>> GetRoomById(Guid roomId)
        {
            var room = await _roomService.GetRoomAsync(roomId);
            if (room == null) return NotFound();

            return room;
        }
    }
}
