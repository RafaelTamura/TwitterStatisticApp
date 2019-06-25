using Microsoft.Extensions.Configuration;
using System.Linq;
using TwitterStatisticApp.Identity.Domain.Entities;
using TwitterStatisticApp.Identity.Domain.Interfaces;

namespace TwitterStatisticApp.Identity.Infra.Data.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration)
        { }

        public Usuario ObterPorNomeUsuarioESenha(string nomeUsuario, string senha)
        {
            if (string.IsNullOrEmpty(nomeUsuario) || string.IsNullOrEmpty(senha))
            {
                return null;
            }

            nomeUsuario = nomeUsuario.ToUpper();
            return this.Find(q => q.NomeUsuario.ToUpper() == nomeUsuario
                                && q.Senha.Equals(senha)).FirstOrDefault();
        }

        public Usuario ObterPorNomeUsuario(string nomeUsuario)
        {
            if (string.IsNullOrEmpty(nomeUsuario))
            {
                return null;
            }

            nomeUsuario = nomeUsuario.ToUpper();
            return this.Find(q => q.NomeUsuario != null && q.NomeUsuario.ToUpper() == nomeUsuario).FirstOrDefault();
        }

        public bool ExisteUsuario(string nomeUsuario)
        {
            nomeUsuario = nomeUsuario.ToUpper();
            return this.Find(q => q.NomeUsuario.ToUpper() == nomeUsuario).FirstOrDefault() != null;
        }
    }
}
