﻿using AtonTalent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtonTalentAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
