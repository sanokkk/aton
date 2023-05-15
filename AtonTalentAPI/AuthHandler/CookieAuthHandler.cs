using AtonTalent.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace AtonTalentAPI.AuthHandler;

public class CookieAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ApplicationDbContext _db;
    public CookieAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ApplicationDbContext db) 
        : base(options, logger, encoder, clock)
    {
        _db = db;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Cookies.ContainsKey("Token"))
            return  AuthenticateResult.Fail("No header found");

        var headerValue = Request.Cookies["Token"].ToString();
        var bytes = Convert.FromBase64String(headerValue);

        var credentials = Encoding.UTF8.GetString(bytes);
        if (!string.IsNullOrEmpty(credentials))
        {
            var userLogin = credentials.Split(':')[0];
            var userPassword = credentials.Split(":")[1];

            var user = _db.Users
                .FirstOrDefault(u => u.Login == userLogin && u.Password == userPassword);

            if (user is null)
                return AuthenticateResult.Fail("Unauthorized");

            var claims = new []
            {
            new Claim(ClaimTypes.NameIdentifier, user.Login),
            new Claim("Login", userLogin),
            new Claim("Password", user.Password)
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        else
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        


    }
}
