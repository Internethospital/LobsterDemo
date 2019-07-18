using Core.Common.CoreFrame;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lobster.Service.Demo.Controllers
{
    [ApiVersion("1.0")]
    [EnableCors("AllowSameDomain")]//启用跨域
    [Route("demo/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class HealthController : Controller
    {

        [HttpGet]
        public IActionResult Get() => Ok("ok");
    }
}
