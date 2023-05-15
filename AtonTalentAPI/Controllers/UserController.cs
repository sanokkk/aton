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

        [HttpPut]
        [Route("/Update/{id}")]
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

        [HttpPut]
        [Route("/ChangePassword/{id}")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest request, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = new Response<User>();
            try
            {
                response.Content = await _userService.ChangePasswordAsync(request.CurrentUser, request.NewPassword, id, cancellationToken);
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

        [HttpPut]
        [Route("/ChangeLogin/{id}")]
        public async Task<IActionResult> ChangeLoginAsync(ChangeLoginRequest request, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = new Response<User>();
            try
            {
                response.Content = await _userService.ChangeLoginAsync(request.CurrentUser, request.NewPassword, id, cancellationToken);
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

        [HttpGet]
        public async Task<IActionResult> GetActiveUsersAsync([FromBody]LoginDto currentUser, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = new Response<User[]>();

            try
            {
                response.Content = await _userService.GetActiveUsers(currentUser, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.Success = false;
            }
            if (response.Success)
                return Ok(response.Content);
            return BadRequest();
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetByLoginAsync([FromBody]GetByLoginRequest request, CancellationToken cancellationToken)
        {
            var response = new Response<UserByLogin>();
            try
            {
                response.Content = await _userService.GetByLoginAsync(request.CurrentUser, request.Login, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.Success = false;
            }
            if (response.Success)
                return Ok(response.Content);
            return BadRequest();
        }

        [HttpGet]
        [Route("/GetByLoginPass")]
        public async Task<IActionResult> GetByLoginPassAsync([FromBody]GetByLoginPassRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var response = new Response<User>();
            try
            {
                response.Content = await _userService.GetByLoginPass(request.UserRequested, request.UserToGet, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.Success = false;
            }
            if (response.Success)
                return Ok(response.Content);
            return BadRequest();
        }

        [HttpGet]
        [Route("/GetOverAge/{age}")]
        public async Task<IActionResult> GetOverAgeAsync([FromBody]LoginDto currentUser, int age, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = new Response<User[]>();
            try
            {
                response.Content = await _userService.GetOverAge(currentUser, age, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.Success = false;
            }
            if (response.Success)
                return Ok(response.Content);
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromBody]DeleteUserRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = new Response<User>();
            try
            {
                response.Content = await _userService.DeleteUserAsync(request.CurrentUser, request.Login, request.DeleteType, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                response.Success = false;
            }
            if (response.Success)
                return Ok(response.Content);
            return BadRequest();
        }

        [HttpPost]
        [Route("/RecoverUser")]
        public async Task<IActionResult> RecoverUserAsync(RecoverUserRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var response = new Response<User>();
            try
            {
                response.Content = await _userService.RecoverUserAsync(request.CurrentUser, request.Id, cancellationToken);
            }
            catch (Exception ex)
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
