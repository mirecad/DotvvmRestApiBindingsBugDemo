using System;
using DotvvmRestApiBindingsBugDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace DotvvmRestApiBindingsBugDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<DateTimeWrapper> Get()
        {
            return new DateTimeWrapper() { Value = DateTime.Now };
        }
    }
}
