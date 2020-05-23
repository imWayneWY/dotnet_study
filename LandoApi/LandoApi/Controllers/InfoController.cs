using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LandoApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly HotelInfo _hotelInfol;

        public InfoController(IOptions<HotelInfo> hotelInfoWrapper)
        {
            _hotelInfol = hotelInfoWrapper.Value;
        }

        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(200)]
        public ActionResult<HotelInfo> GetInfo()
        {
            _hotelInfol.Href = Url.Link(nameof(GetInfo), null);
            return _hotelInfol;
        }
    }
}
