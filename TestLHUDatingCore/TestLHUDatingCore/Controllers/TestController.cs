using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLHUDatingCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
       [HttpGet]
       public IEnumerable<string> Get()
        {
            return new String[] { "Mot", "Hai", "Ba" };
        }
    }
}
