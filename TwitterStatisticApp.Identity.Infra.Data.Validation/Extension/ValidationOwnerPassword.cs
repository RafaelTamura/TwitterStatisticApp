using IdentityModel;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TwitterStatisticApp.Identity.Domain.Interfaces;

namespace TwitterStatisticApp.Identity.Infra.Data.Validation
{
    public class ValidationOwnerPassword : IResourceOwnerPasswordValidator
    {
        #region Variáveis Locais
        protected IUsuarioRepository _usuarioRepository;
        #endregion

        #region Construtor
        public ValidationOwnerPassword(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        #endregion

        #region Métodos Públicos
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            List<Claim> listaClaims = new List<Claim>();

            if (!context.Equals(null))
            {
                string usuario = context.UserName.ToString();
                string senha = context.Password.ToString();
                Dictionary<string, object> retornoUsuario = new Dictionary<string, object>();

                var usuarioValido = _usuarioRepository.ObterPorNomeUsuarioESenha(usuario, senha);

                if (usuarioValido != null)
                {
                    usuarioValido.SetSenha(string.Empty);
                    retornoUsuario.Add("Usuario", usuarioValido);

                    #region Inclusão de roles registradas para o usuário
                    if (usuarioValido.Claims != null)
                    {
                        foreach (var claim in usuarioValido.Claims)
                        {
                            if (!string.IsNullOrEmpty(claim.Type)
                                && !string.IsNullOrEmpty(claim.Value))
                            {
                                listaClaims.Add(new Claim(claim.Type, claim.Value));
                            }
                        }
                    }
                    #endregion

                    context.Result = new GrantValidationResult(
                                            subject: usuarioValido.NomeUsuario,
                                            authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                                            claims: listaClaims,
                                            customResponse: retornoUsuario);
                }
            }

            await Task.FromResult(context.Result);
        } 
        #endregion
    }
}
