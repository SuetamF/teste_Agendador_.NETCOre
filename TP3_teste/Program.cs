using Modelo;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace Agendador
{
    class Program
    {

        static void Main(string[] args)
        {
            RepoPessoa.CriarArquivo();

            RepoPessoa.Pessoas = new List<Pessoa>();

            RepoPessoa.LerArquivo();

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
                    var pessoaEscolhida = resultados[selecao];
                    Console.WriteLine($" A data do proximo Aniversario de {pessoaEscolhida.Nome} {pessoaEscolhida.Sobrenome} é:{pessoaEscolhida.DatadeAniver}");
                    var diasAteAniversario = pessoaEscolhida.CalculoDeTempo();
                    if (diasAteAniversario == 0)
                    {
                        Console.WriteLine(pessoaEscolhida.Parabens());
                    }
                    else
                    {
                        Console.WriteLine($"Faltam {diasAteAniversario} dia(s) para o proximo aniversario de {pessoaEscolhida.Nome}");
                    }
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
                    if (data2.Month < DateTime.Now.Month && data2.Day < DateTime.Now.Day ) 
                    {
                        DateTime dataNiver = data2.AddYears(1);
                        data2 = dataNiver;
                    }
                    String aniversario = (data2.ToString("dd/MM/yyyy"));
                    String nascimento = (data.ToString("dd/MM/yyyy"));
                    Pessoa pessoa = new Pessoa() { Nome = nome, Sobrenome = sobrenome, DatadeNascimento = data, DatadeAniver = data2 };
                    RepoPessoa.InserirPessoa(pessoa);

                    var diretorio = @"C:\MeuDir";
                    var nomeArquivo = "Pessoas.csv";
                    var caminhoArquivo = Path.Combine(diretorio, nomeArquivo);
                    var csv = new StringBuilder();
                    csv.AppendLine($"{nome};{sobrenome};{nascimento};{aniversario}");
                    File.AppendAllText(caminhoArquivo, csv.ToString());

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
