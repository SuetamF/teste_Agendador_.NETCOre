using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DatadeNascimento { get; set; }
        public DateTime DatadeAniver { get; set; }


        public int CalculoDeTempo()
        {
            DateTime aniversario = new DateTime(DateTime.Now.Year, DatadeNascimento.Month, DatadeNascimento.Day);
            TimeSpan intervalo = aniversario - DateTime.Now;
            if (intervalo.Days < 0)
            {
                DateTime ProximoAniver = aniversario.AddYears(1);
                intervalo = ProximoAniver - DateTime.Now;
            }
            else if (intervalo.Days == 0 && intervalo.Hours <= 0 && intervalo.Minutes <= 0 && intervalo.Seconds <= 0 && intervalo.Milliseconds <= 0)
            {
                return 0;
            }
            var resultado = intervalo.Days + 1;
            return resultado;
        }

        public string Parabens()
        {
            return $"Parabéns, {this.Nome} {this.Sobrenome} " +
                   $"pelos seus {DateTime.Now.Year - DatadeNascimento.Year} anos. ";
        }
    }
}
