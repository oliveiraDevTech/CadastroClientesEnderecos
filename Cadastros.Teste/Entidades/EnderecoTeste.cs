﻿using Bogus;
using Cadastro.Dominio.Entidades.Enderecos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Xunit;

namespace Cadastro.Teste.Entidades
{
    public class EnderecoTeste
    {
        public EnderecoModelo EnderecoModelo { get; private set; }
        public Endereco Endereco { get; private set; }

        //[Required(ErrorMessage = "Campo obrigatório")]
        //[MaxLength(30, ErrorMessage = "O campo nome permite no máximo 50 caracteres")]
        //public string Logradouro { get; set; } //50

        //[Required(ErrorMessage = "Campo obrigatório")]
        //[MaxLength(30, ErrorMessage = "O campo nome permite no máximo 40 caracteres")]
        //public string Bairro { get; set; } //40

        //[Required(ErrorMessage = "Campo obrigatório")]
        //[MaxLength(30, ErrorMessage = "O campo nome permite no máximo 40 caracteres")]
        //public string Cidade { get; set; } //40

        //[Required(ErrorMessage = "Campo obrigatório")]
        //[MaxLength(30, ErrorMessage = "O campo nome permite no máximo 40 caracteres")]
        //public string Estado { get; set; } //40

        public EnderecoTeste()
        {
            var faker = new Faker();
            Endereco = new Endereco();

            EnderecoModelo = new EnderecoModelo()
            {
                Logradouro = faker.Address.StreetAddress(),
                Bairro = faker.Address.Country(),
                Cidade = faker.Address.City(),
                Estado = faker.Address.State()
            };
        }

        [Fact]
        public void InstanciarUmEnderecoPeloModelo()
        {
            Endereco.CopiarDoModelo(EnderecoModelo);
            Assert.True(Endereco.Valid);
        }

        [Fact]
        public void InstanciarUmEnderecoPeloModeloVazio()
        {
            var clienteModelo = new EnderecoModelo();

            Endereco.CopiarDoModelo(clienteModelo);
            Assert.True(Endereco.Invalid);
        }

        [Theory]
        [InlineData(null)]
        public void InstanciarUmEnderecoPeloModeloNulo(EnderecoModelo EnderecoModeloNulo)
        {
            Endereco.CopiarDoModelo(EnderecoModeloNulo);
            Assert.True(Endereco.Invalid);
        }

        [Theory]
        [InlineData("Adicionando um logradouro de tamanho maior que o permitido")]
        [InlineData(null)]
        [InlineData("")]
        public void NaoPodeTerUmLogradouroInvalido(string logradouro)
        {
            EnderecoModelo.Logradouro = logradouro;

            Endereco.CopiarDoModelo(EnderecoModelo);
            if (Endereco.Invalid)
                Assert.True(Endereco.Notifications.Where(x => x.Property == "Logradouro").Any());
        }

        [Theory]
        [InlineData("Adicionando um Bairro de tamanho maior que o permitido")]
        [InlineData(null)]
        [InlineData("")]
        public void NaoPodeTerUmBairroInvalido(string bairro)
        {
            EnderecoModelo.Bairro = bairro;

            Endereco.CopiarDoModelo(EnderecoModelo);
            if (Endereco.Invalid)
                Assert.True(Endereco.Notifications.Where(x => x.Property == "Bairro").Any());
        }

        [Theory]
        [InlineData("Adicionando uma Cidade de tamanho maior que o permitido")]
        [InlineData(null)]
        [InlineData("")]
        public void NaoPodeTerUmaCidadeInvalida(string cidade)
        {
            EnderecoModelo.Cidade = cidade;

            Endereco.CopiarDoModelo(EnderecoModelo);
            if (Endereco.Invalid)
                Assert.True(Endereco.Notifications.Where(x => x.Property == "Cidade").Any());
        }

        [Theory]
        [InlineData("Adicionando um Estado de tamanho maior que o permitido")]
        [InlineData(null)]
        [InlineData("")]
        public void NaoPodeTerUmEstadoInvalido(string estado)
        {
            EnderecoModelo.Estado = estado;

            Endereco.CopiarDoModelo(EnderecoModelo);
            if (Endereco.Invalid)
                Assert.True(Endereco.Notifications.Where(x => x.Property == "Estado").Any());
        }
    }
}