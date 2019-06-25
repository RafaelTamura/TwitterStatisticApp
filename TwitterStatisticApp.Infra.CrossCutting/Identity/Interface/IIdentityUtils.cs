using IdentityModel.Client;

namespace TwitterStatisticApp.Infra.CrossCutting.Identity.Interface
{
    public interface IIdentityUtils
    {
        TokenResponse ObterIdentity(string login, string senha);
    }
}