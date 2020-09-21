using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace Modelo
{
    public class RepoPessoa
    {

        public static List<Pessoa> Pessoas { get; set; }

        //Busca
        public static List<Pessoa> BuscarPessoa(string busca)
        {
            List<Pessoa> resultados = new List<Pessoa>();
            foreach (var p in Pessoas)
            {
                var nomeCompleto = $"{p.Nome} {p.Sobrenome}";
                if (nomeCompleto.Contains(busca))
                {
                    resultados.Add(p);
                }
            }

            return resultados;
        }

        //Adicionar
        public static void InserirPessoa(Pessoa pessoa)
        {
            Pessoas.Add(pessoa);
        }

        public static void CriarArquivo()
        {

            var diretorio = @"C:\MeuDir";
            var nomeArquivo = "Pessoas.csv";
            var caminhoArquivo = Path.Combine(diretorio, nomeArquivo);
 
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }
            else
            {
                Console.WriteLine("AVISO: O diretório já existe.");
            }

            var csv = new StringBuilder();

            if (!File.Exists(caminhoArquivo))
            {
                csv.AppendLine("Nome;Sobrenome;Data de Nascimento;Data de Aniversario");
                File.WriteAllText(caminhoArquivo, csv.ToString());
            }

            csv.Clear();

        }

        public static void ArquivoParaLista()
        {

            var diretorio = @"C:\MeuDir";
            var nomeArquivo = "Pessoas.csv";
            var caminhoArquivo = Path.Combine(diretorio, nomeArquivo);

            var linhas = File.ReadAllLines(caminhoArquivo);
            ArraySegment<string> linhasSegmento = new ArraySegment<string>(linhas);
            var dados = linhasSegmento.Slice(1);
            foreach (var linha in dados)
            {
                Pessoa p = new Pessoa();
                Char[] tokens = new Char[] { ';', ',', '\n' };
                string[] dadosPessoa = linha.Split(tokens);

                int nomeIndex = 0;
                int sobrenomeIndex = 1;
                int nascimentoIndex = 2;
                int aniversarioIndex = 3;

                p.Nome = dadosPessoa[nomeIndex];
                p.Sobrenome = dadosPessoa[sobrenomeIndex];
                p.DatadeNascimento = DateTime.ParseExact(dadosPessoa[nascimentoIndex], "dd/MM/yyyy", new CultureInfo("pt-BR"));
                p.DatadeAniver = DateTime.ParseExact(dadosPessoa[aniversarioIndex], "dd/MM/yyyy", new CultureInfo("pt-BR"));

                Pessoas.Add(p);
            }
        }
        //Rescrever arquivo lendo lista
        public static void ListaParaArquivo()
        {
            var diretorio = @"C:\MeuDir";
            var nomeArquivo = "Pessoas.csv";
            var caminhoArquivo = Path.Combine(diretorio, nomeArquivo);
            var csv = new StringBuilder();
            csv.AppendLine("Nome;Sobrenome;Data de Nascimento;Data de Aniversario");

            foreach (var p in Pessoas)
            {
                var nome = p.Nome;
                var sobrenome = p.Sobrenome;
                String nascimento = (p.DatadeNascimento.ToString("dd/MM/yyyy"));
                String aniversario = (p.DatadeAniver.ToString("dd/MM/yyyy"));
                csv.AppendLine($"{nome};{sobrenome};{nascimento};{aniversario}");
            }
            File.WriteAllText(caminhoArquivo, csv.ToString());
            csv.Clear();
        }
        
    }

}
