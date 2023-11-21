using Elmah.Io.AspNetCore;
using TesteArquitetura.Core.Notificacoes.Interfaces;
using TesteArquitetura.WebApi.Extensions;
using TesteArquitetura.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TesteArquitetura.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[AutoValidateAntiforgeryToken]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        public AuthController(INotificador notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IUser user,
                              ILogger<AuthController> logger) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        //[EnableCors("Development")]
        [AllowAnonymous]
        [HttpPost("nova-conta")]
        public async Task<IActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var user = new IdentityUser
                {
                    UserName = registerUser.Email,
                    Email = registerUser.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return CustomResponse(await GerarJwt(user.Email));
                }
                foreach (var error in result.Errors)
                {
                    NotificarErro(error.Description);
                }

                return CustomResponse(registerUser);
            }
            catch (Exception ex)
            {
                ElmahIoApi.Log(ex, HttpContext);
                return CustomResponse(registerUser);
            }
        }

        [AllowAnonymous]
        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuario " + loginUser.Email + " logado com sucesso");
                    return CustomResponse(await GerarJwt(loginUser.Email));
                }
                if (result.IsLockedOut)
                {
                    NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
                    return CustomResponse(loginUser);
                }

                NotificarErro("Usuário ou Senha incorretos");
                return CustomResponse(loginUser);
            }
            catch (Exception ex)
            {
                ElmahIoApi.Log(ex, HttpContext);
                return CustomResponse(loginUser);
            }
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            try
            {
                var td = new SecurityTokenDescriptor();
                td.Issuer = _appSettings.Emissor;
                td.Audience = _appSettings.ValidoEm;
                td.Subject = identityClaims;
                td.Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras);
                td.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

                var token = tokenHandler.CreateToken(td);

                var encodedToken = tokenHandler.WriteToken(token);

                var response = new LoginResponseViewModel
                {
                    AccessToken = encodedToken,
                    ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                    UserToken = new UserTokenViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Nome = user.UserName,
                        Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                    }
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;


        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
