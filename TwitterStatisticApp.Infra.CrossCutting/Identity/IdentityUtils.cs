using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TwitterStatisticApp.Infra.CrossCutting.Identity.Interface;

namespace TwitterStatisticApp.Infra.CrossCutting.Identity
{
    public class IdentityUtils : IIdentityUtils
    {
        #region [ Variáveis Locais ]
        private IConfiguration _config;
        private string identidadeURL;
        #endregion

        #region [ Construtor ]
        public IdentityUtils(IConfiguration config)
        {
            _config = config;
            identidadeURL = string.Format("{0}{1}", _config["IdentidadeServerPrefix"], _config["IdentidadeServer"]);
        }
        #endregion

        #region [ Métodos Públicos ]
        /// <summary>
        /// Obter o token do usuário no Identity.
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Token e informações adicionais</returns>
        public TokenResponse ObterIdentity(string login, string senha)
        {
            string urlRequisicao = identidadeURL;
            string clientId = _config["IdentityServer:ClientId"];
            string clientSecret = _config["IdentityServer:ClientSecret"];
            string escopoApi = _config["IdentityServer:ScopeApi"];
            bool.TryParse(_config["IdentityServer:RequisicaoHTTPS"], out bool requisicaoHTTPS);

            Task<DiscoveryResponse> disco = null;

            if (requisicaoHTTPS)
            {
                disco = DiscoveryClient.GetAsync(urlRequisicao);
            }
            else
            {
                // discover endpoints from metadata without HTTPS
                var discoveryClient = new DiscoveryClient(urlRequisicao);
                discoveryClient.Policy = new DiscoveryPolicy { RequireHttps = false };
                discoveryClient.Policy.ValidateIssuerName = false;
                disco = discoveryClient.GetAsync();
            }

            var tokenClient = new TokenClient(disco.Result.TokenEndpoint, clientId, clientSecret);
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync(login, senha, escopoApi);

            // request token
            if (!tokenResponse.Result.IsError)
            {
                return tokenResponse.Result;
            }

            return null;
        }

        #endregion
    }
}
