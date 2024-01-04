using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EventsManager.Models;
using EventsManager.Services;

namespace EventsManagerTests
{
    public class EventRegistrationTests
    {


        [Fact]
        public void CadastrarEventoComSucesso()
        {
            // Implementar caso de teste para o Cadastro de um evento com sucesso
        }

        [Fact]
        public void TentarCadastrarEventoComNomeDeEventoJaExistente()
        {
            // Implementar caso de teste para o Cadastro de um evento com um nome de evento já existente
        }

        [Fact]
        public void TentarCadastrarEventoSemPreencherCamposObrigatorios()
        {
            // Implementar caso de teste para o Cadastro de um evento sem o preenchimento dos campos obrigatórios
        }

        [Fact]
        public void TentarCadastrarEventoSemInserirImagem()
        {
            // Implementar caso de teste para o Cadastro de um evento sem o endereço da imagem de capa
        }
    
    }
}