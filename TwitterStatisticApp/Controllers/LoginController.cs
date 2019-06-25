using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwitterStatisticApp.Infra.CrossCutting.Identity.Interface;
using TwitterStatisticApp.ViewModels;

namespace TwitterStatisticApp.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private IIdentityUtils _identityUtils;

        public LoginController(IIdentityUtils identityUtils)
        {
            _identityUtils = identityUtils;
        }

        [HttpPost("Acessar")]
        public ActionResult<string> Acessar([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel == null)
            {
                return Ok(null);
            }

            var token = _identityUtils.ObterIdentity(loginViewModel.Usuario, loginViewModel.Senha);
            if (token != null)
            {
                return Ok(JsonConvert.SerializeObject(token.AccessToken));
            }

            return NotFound();
        }
    }
}