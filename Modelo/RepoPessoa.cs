using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
