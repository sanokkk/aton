using AtonTalent.Domain.ComplexRequests;
using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;
using AtonTalent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtonTalentAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken,CreateRequest request)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = new Response<User>();
            try
            {
                response.Content = await _userService.CreateUserAsync(request.currentUser, request.userCreateDto, cancellationToken);
            }
            catch (Exception ex)
            {
                response.Success = false;
                _logger.Log(LogLevel.Error, ex.Message);
            }
            if (response.Success)
                return Ok(response.Content);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(UpdateRequest request, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = new Response<User>();
            try
            {
                response.Content = await _userService.UpdateAsync(request.currentUser, request.updateModel, id, cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.Success = false;
            }
            if (response.Success)
                return Ok(response.Content);
            return BadRequest();
        }
    }
}
