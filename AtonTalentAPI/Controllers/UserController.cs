using AtonTalent.Domain.ComplexRequests;
using AtonTalent.Domain.Dtos;
using AtonTalent.Domain.Models;
using AtonTalent.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtonTalentAPI.Controllers;

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
    [Route("/Login")]
    public async Task<IActionResult> LoginAsync([FromBody]LoginDto currentUser, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userService.GetSelfUser(currentUser, cancellationToken);
        if (user is null)
            return BadRequest();

        string credentials = new string(currentUser.Login + ":" + currentUser.Password);
        var bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(credentials);
        var baseString = Convert.ToBase64String(bytes);

        var cookieOptions = new CookieOptions() { Expires = DateTime.UtcNow.AddMinutes(30)};

        Response.Cookies.Append("Token", baseString, cookieOptions);
        return Ok();
    }

    [Authorize]
    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete("Token");
        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken, [FromBody]UserCreateDto model)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();

        var response = new Response<User>();
        try
        {
            response.Content = await _userService.CreateUserAsync(currentUser, model, cancellationToken);
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

    [Authorize]
    [HttpPut]
    [Route("/Update/{id}")]
    public async Task<IActionResult> UpdateAsync([FromBody]UpdateUserDto model, [FromRoute]Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();
        var response = new Response<User>();
        try
        {
            response.Content = await _userService.UpdateAsync(currentUser, model, id, cancellationToken);
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

    [Authorize]
    [HttpPut]
    [Route("/ChangePassword/{id}")]
    public async Task<IActionResult> ChangePasswordAsync([FromHeader]string newPassword, [FromQuery]Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();
        var response = new Response<User>();
        try
        {
            response.Content = await _userService.ChangePasswordAsync(currentUser, newPassword, id, cancellationToken);
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

    [Authorize]
    [HttpPut]
    [Route("/ChangeLogin/{id}")]
    public async Task<IActionResult> ChangeLoginAsync([FromHeader]string newLogin, [FromRoute]Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();
        var response = new Response<User>();
        try
        {
            response.Content = await _userService.ChangeLoginAsync(currentUser, newLogin, id, cancellationToken);
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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetActiveUsersAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var currentUser = GetFromAuth();

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

    [Authorize]
    [HttpGet("{login}")]
    public async Task<IActionResult> GetByLoginAsync([FromRoute]string login, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();
        var response = new Response<UserByLogin>();
        try
        {
            response.Content = await _userService.GetByLoginAsync(currentUser, login, cancellationToken);
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

    [Authorize]
    [HttpGet]
    [Route("/GetByLoginPass")]
    public async Task<IActionResult> GetByLoginPassAsync([FromBody]GetByLoginPassRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();
        var response = new Response<User>();
        try
        {
            response.Content = await _userService.GetByLoginPass(currentUser, request.UserToGet, cancellationToken);
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

    [Authorize]
    [HttpGet]
    [Route("/GetOverAge/{age}")]
    public async Task<IActionResult> GetOverAgeAsync([FromRoute]int age, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();
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

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync([FromBody]DeleteUserRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();
        var response = new Response<User>();
        try
        {
            response.Content = await _userService.DeleteUserAsync(currentUser, request.Login, request.DeleteType, cancellationToken);
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

    [Authorize]
    [HttpPost]
    [Route("/RecoverUser/{id}")]
    public async Task<IActionResult> RecoverUserAsync([FromRoute]Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var currentUser = GetFromAuth();
        var response = new Response<User>();
        try
        {
            response.Content = await _userService.RecoverUserAsync(currentUser, id, cancellationToken);
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

    [Authorize]
    private LoginDto GetFromAuth()
    {
        var currentUser = new LoginDto()
        {
            Login = User.Claims.First(f => f.Type == "Login").Value,
            Password = User.Claims.First(f => f.Type == "Password").Value
        };
        return currentUser;
    }

}
