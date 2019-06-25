using TwitterStatisticApp.Identity.Domain.Entities;

namespace TwitterStatisticApp.Identity.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        bool ExisteUsuario(string nomeUsuario);
        Usuario ObterPorNomeUsuario(string nomeUsuario);
        Usuario ObterPorNomeUsuarioESenha(string nomeUsuario, string senha);
    }
}