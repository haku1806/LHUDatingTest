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
    public class AdminController : ControllerBase
    {
        private AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            this._adminService = adminService;
        }

        [HttpGet]
        public IActionResult Get(string key)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                responseAPI.Data = this._adminService.Get(key);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("{username}")]
        [HttpGet]
        public IActionResult GetById(string username)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                responseAPI.Data = this._adminService.GetById(username);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [HttpPost]
        public IActionResult Post(AdminDto admin)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                this._adminService.Insert(admin);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("{username}")]
        [HttpPut]
        public IActionResult Put(string username, AdminDto admin)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                this._adminService.Update(username, admin);
                return Ok(responseAPI);
            }
            catch (Exception ex)
            {
                responseAPI.Message = ex.Message;
                return BadRequest(responseAPI);
            }
        }

        [Route("{username}")]
        [HttpDelete]
        public IActionResult Delete(string username)
        {
            ResponseAPI responseAPI = new ResponseAPI();
            try
            {
                this._adminService.DeleteById(username);
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
