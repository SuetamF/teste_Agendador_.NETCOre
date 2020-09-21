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

            RepoPessoa.ArquivoParaLista();

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
                    Console.Clear();
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
                    Console.WriteLine("Digite o Indice de uma pessoa da lista acima para selecionar:");
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
                    Console.Clear();
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
                    csv.Clear();

                    break;
                case 3:
                    //Edit
                    Console.Clear();
                    int j = 0;
                    foreach (var p in RepoPessoa.Pessoas)
                    {
                        Console.WriteLine($"{j} - {p.Nome} {p.Sobrenome} {p.DatadeNascimento}");
                        j++;
                    }
                    Console.WriteLine("Digite o Indice de uma pessoa da lista acima para editada-la:");
                    var selecaoE = Int32.Parse(Console.ReadLine());
                    var pessoaEscolhidaE = RepoPessoa.Pessoas[selecaoE];
                    Console.WriteLine("Informe o novo nome da pessoa:");
                    var nomeE = Console.ReadLine();
                    Console.WriteLine("Informe o novo sobrenome da pessoa:");
                    var sobrenomeE = Console.ReadLine();
                    Console.WriteLine("Informe a nova data de nascimento da pessoa:");
                    var dataE = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", new CultureInfo("pt-BR"));
                    var data2E = new DateTime(DateTime.Now.Year, dataE.Month, dataE.Day);
                    if (data2E.Month < DateTime.Now.Month && data2E.Day < DateTime.Now.Day)
                    {
                        DateTime dataNiver = data2E.AddYears(1);
                        data2E = dataNiver;
                    }
                    pessoaEscolhidaE.Nome = nomeE;
                    pessoaEscolhidaE.Sobrenome = sobrenomeE;
                    pessoaEscolhidaE.DatadeNascimento = dataE;
                    pessoaEscolhidaE.DatadeAniver = data2E;
                    RepoPessoa.ListaParaArquivo();
                    break;
                case 4:
                    //Delete
                    Console.Clear();
                    int y = 0;
                    foreach (var p in RepoPessoa.Pessoas)
                    {
                        Console.WriteLine($"{y} - {p.Nome} {p.Sobrenome}");
                        y++;
                    }
                    Console.WriteLine("Digite o Indice de uma pessoa da lista acima para a deletar:");
                    var selecaoD = Int32.Parse(Console.ReadLine());
                    var pessoaEscolhidaD = RepoPessoa.Pessoas[selecaoD];
                    RepoPessoa.Pessoas.Remove(pessoaEscolhidaD);
                    RepoPessoa.ListaParaArquivo();
                    break;
                case 5:
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
            Console.Clear();
            Console.WriteLine("Gerenciador de Aniversário");
            Console.WriteLine("Selecione uma opção abaixo:");
            Console.WriteLine("1 -> Pesquisar pessoas");
            Console.WriteLine("2 -> Adicionar nova pessoa");
            Console.WriteLine("3 -> Editar Pessoa");
            Console.WriteLine("4 -> Deletar Pessoa");
            Console.WriteLine("5 -> Sair");
        }
    }
}
