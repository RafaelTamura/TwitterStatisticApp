using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterStatisticApp.Identity.Domain.Interfaces;
using TwitterStatisticApp.Identity.Infra.Data.Mock;
using TwitterStatisticApp.Identity.Infra.Data.Repository;
using TwitterStatisticApp.Identity.Infra.Data.Validation;

namespace TwitterStatisticApp.Identity
{
    public class Startup
    {
        #region Métodos Privados
        private IConfiguration Configuration;
        private IUsuarioRepository _repositoryUsuario;
        #endregion

        #region Construtor
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        #endregion

        #region Métodos Públicos
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Identity Server Configuration
            services.AddIdentityServer(x => x.IssuerUri = string.Format("{0}{1}", Configuration["IdentidadeServerPrefix"], Configuration["IdentidadeServer"]))
                    .AddDeveloperSigningCredential()
                    .AddInMemoryClients(MockIdentity.GetClients())
                    .AddInMemoryApiResources(MockIdentity.GetApiResources())
                    .AddInMemoryIdentityResources(MockIdentity.GetIdentityResources())
                    .AddCustomValidator()
                    .AddProfileService<CustomProfileService>();
            #endregion

            #region Dependency Injection
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            #endregion

            #region Resolve Services of Dependency Injection
            _repositoryUsuario = services.BuildServiceProvider().GetService<IUsuarioRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Identity Server Activation
            app.UseIdentityServer();
            #endregion

            #region Identity Server Data Initialization
            InicializacaoDados();
            #endregion
        } 
        #endregion

        #region Métodos Privados
        /// <summary>
        /// Inicialização dos dados.
        /// </summary>
        private void InicializacaoDados()
        {
            CriarUsuarioServidor();
        }

        /// <summary>
        /// Criar usuário padrão para servidor.
        /// Será utilizado para criar dados do Identity
        /// </summary>
        private void CriarUsuarioServidor()
        {
            var usuario = MockIdentity.GetUsuario();
            if (!_repositoryUsuario.ExisteUsuario(usuario.NomeUsuario))
            {
                _repositoryUsuario.Add(usuario);
            }
        }
        #endregion
    }
}
