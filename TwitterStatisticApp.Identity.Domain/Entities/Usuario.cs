using System;
using System.Collections.Generic;
using TwitterStatisticApp.Identity.Domain.Entities.ObjectValues;

namespace TwitterStatisticApp.Identity.Domain.Entities
{
    public class Usuario
    {
        public Usuario(Guid id, string nomeUsuario, string senha, IEnumerable<UsuarioClaim> claims)
        {
            Id = id;
            NomeUsuario = nomeUsuario;
            Senha = senha;
            Claims = claims;
        }

        public Guid Id { get; private set; }
        public string NomeUsuario { get; private set; }
        public string Senha { get; private set; }
        public IEnumerable<UsuarioClaim> Claims { get; private set; }

        public void SetSenha(string senha)
        {
            Senha = senha;
        }
    }
}
