﻿using Ecommerce.userManage.Application.Service;
using Ecommerce.userManage.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.userManage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }


        [HttpPost]
        public IActionResult addUser(UserModel userModel)
        {
            try
            {
                _userService.addUser(userModel);
                return Ok("user added successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{Id?}")]
        public IActionResult getUserById(int Id)
        {
            try
            {
                var userData = _userService.getUserById(Id);
                return Ok(userData);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult updateUser(UserModel userModel)
        {
            try
            {
                _userService.updateUser(userModel);
                return Ok("user updated successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("email/{email?}")]
        public IActionResult getUserByEmail(string email)
        {
            try
            {
                var userData = _userService.getUserByEmail(email);
                return Ok(userData);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}