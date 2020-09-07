using Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace TP3
{
    class Program
    {

        static void Main(string[] args)
        {
            RepoPessoa.Pessoas = new List<Pessoa>();
            while (true)
            {
                Menu();
                var opcao = Int32.Parse(Console.ReadLine());
                Selecionaropcao(opcao);
            }
        }

        private static void Selecionaropcao(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    //Pesquisar
                    Console.WriteLine("Pesquisar pessoas");
                    Console.WriteLine("Informe o nome da pessoa:");
                    var busca = Console.ReadLine();
                    var resultados = RepoPessoa.BuscarPessoa(busca);
                    int i = 0;
                    foreach (var p in resultados)
                    {
                        Console.WriteLine($"{i} - {p.Nome} {p.Sobrenome}");
                        i++;
                    }
                    Console.WriteLine("Selecione uma pessoa da lista acima:");
                    var selecao = Int32.Parse(Console.ReadLine());
                    var pesoaEscolhida = resultados[selecao];
                    Console.WriteLine($"Nome:{pesoaEscolhida.Nome} {pesoaEscolhida.Sobrenome} Data de Aniversario:{pesoaEscolhida.DatadeAniver}");
                    TimeSpan intervalo = pesoaEscolhida.DatadeAniver - DateTime.Now;
                    if (intervalo.Days < 0)
                    {
                        DateTime ProximoAniver = new DateTime(DateTime.Now.Year + 1, pesoaEscolhida.DatadeAniver.Month, pesoaEscolhida.DatadeAniver.Day);
                        intervalo = ProximoAniver - DateTime.Now;
                    }
                    Console.WriteLine($"{intervalo.Days} Dias ate o Aniversario dessa Pessoa");
                    break;
                case 2:
                    //Adicionar
                    Console.WriteLine("Adicionar nova pessoa");
                    Console.WriteLine("Informe o nome da pessoa:");
                    var nome = Console.ReadLine();
                    Console.WriteLine("Informe o sobrenome da pessoa:");
                    var sobrenome = Console.ReadLine();
                    Console.WriteLine("Informe a data de nascimento da pessoa:");
                    var data = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", new CultureInfo("pt-BR"));
                    var data2 = new DateTime(DateTime.Now.Year, data.Month, data.Day);
                    Pessoa pessoa = new Pessoa() { Nome = nome, Sobrenome = sobrenome, DatadeNascimento = data, DatadeAniver = data2 };
                    RepoPessoa.InserirPessoa(pessoa);
                    break;
                case 3:
                    //Sair
                    Console.WriteLine("Fim do Programa.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção Inválida.");
                    break;
            }
        }

        private static void Menu()
        {
            Console.WriteLine("Gerenciador de Aniversário");
            Console.WriteLine("Selecione uma opção abaixo:");
            Console.WriteLine("1 -> Pesquisar pessoas");
            Console.WriteLine("2 -> Adicionar nova pessoa");
            Console.WriteLine("3 -> Sair");
        }
    }
}
