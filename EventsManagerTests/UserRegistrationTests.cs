using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using EventsManager.Models;
using EventsManager.Services;

namespace EventsManagerTests
{
    public class UserRegistrationTests
    {

        [Fact]
        public void CadastrarPerfilComumComSucesso()
        {
            // Implementar caso de teste para Cadastro com o perfil Comum Realizado com Sucesso
        }

        [Fact]
        public void CadastrarPerfilComEmailJaExistente()
        {
            // Implementar caso de teste para tentativa de Cadastrar um e-mail já existente
        }

        [Fact]
        public void CadastrarPerfilComSenhaForaDoPadrao()
        {
            // Implementar caso de teste para tentativa de realizar o cadastro com uma senha fora do padrão
        }

    }
}   
