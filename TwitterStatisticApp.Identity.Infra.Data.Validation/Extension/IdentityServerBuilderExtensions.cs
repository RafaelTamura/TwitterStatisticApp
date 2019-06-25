using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace TwitterStatisticApp.Identity.Infra.Data.Validation
{
    public static class IdentityServerBuilderExtensions
    {
        #region Métodos Estáticos
        /// <summary>
        /// Configure Custom Validator
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddCustomValidator(this IIdentityServerBuilder builder)
        {
            builder.AddResourceOwnerValidator<ValidationOwnerPassword>();

            return builder;
        } 
        #endregion
    }
}
