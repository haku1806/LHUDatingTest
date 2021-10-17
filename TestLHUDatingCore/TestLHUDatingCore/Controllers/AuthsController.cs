using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestLHUDatingCore.Dto;
using TestLHUDatingCore.Service;

namespace TestLHUDatingCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private AdminService _adminService;

        public AuthsController(AdminService adminService)
        {
            this._adminService = adminService;
        }

        [HttpPost]
        public IActionResult GetAccessToken(AdminDto admin)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                responseAPI.Data = this._adminService.GetAccessToken(admin);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }
    }
}
