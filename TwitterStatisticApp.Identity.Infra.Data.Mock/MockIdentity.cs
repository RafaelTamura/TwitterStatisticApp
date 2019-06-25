using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using TwitterStatisticApp.Identity.Domain.Entities;

namespace TwitterStatisticApp.Identity.Infra.Data.Mock
{
    public static class MockIdentity
    {
        public static Usuario GetUsuario()
        {
            return new Usuario(Guid.NewGuid(),
                               "adminServerIdentity",
                               "ef{0uFt%kA4QTlp",
                               null);
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("apiAutenticacao", "API Autenticação")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                // resource owner password and client credentials grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("685EBF9C26D58BFB6A59C7792CCAA".Sha256())
                    },
                    AllowedScopes = { "apiAutenticacao" },
                    AccessTokenLifetime = 3600, //1 hora
                    ClientClaimsPrefix = "",
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
        }
    }
}
