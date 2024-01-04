using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EventsManager.Models;
using EventsManager.Services;

namespace EventsManagerTests
{
    public class UserLoginTests
    {


        [Fact]
        public void RealizarLoginComSucesso()
        {
            // Implementar caso de teste para Login Realizado com Sucesso
        }

        [Fact]
        public void RealizarLoginComEmailOuSenhaIncorretos()
        {
            // Implementar caso de teste para tentativa de login com E-mail ou Senha Incorretos
        }

        [Fact]
        public void RealizarLoginSemCadastro()
        {
            // Implementar caso de teste de quando alguém que não possui cadastro tentar realizar login. 
        }

        [Fact]
        public void RealizarLoginESalvarDadosDeLogin()
        {
            // Implementar caso de teste para quando quando alguém realizar login, perguntar se deseja manter dados salvos. 
            //  caso a pessoa indque que sim, exibir mensagem: "E-mail não encontrado. Deseja realizar cadastro?"
        }
    }
}