using System;
using SQLite;

namespace App.Gestao.Financeira.Domain
{
    public class Transacao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public decimal Valor { get; set; }
        public int Categoria { get; set; }
        public string Descricao { get; set; }
        public int Tipo { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Programado { get; set; }
    }
}