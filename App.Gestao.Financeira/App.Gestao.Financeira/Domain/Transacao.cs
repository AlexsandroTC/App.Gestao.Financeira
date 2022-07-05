using System;
using SQLite;

namespace App.Gestao.Financeira.Domain
{
    public class Transacao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public int Tipo { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataUltimaEdicao { get; set; }
        public bool Programado { get; set; }
        public bool Estornado { get; set; }

        [Ignore]
        public string TypeTransaction
        {
            get
            {
                if (Tipo == 2)
                {
                    return "Saida";
                }
                else
                {
                    return "Entrada";
                }
            }
        }
        
        [Ignore]
        public string SufixeType
        {
            get
            {
                if (Tipo == 2)
                {
                    return "-";
                }
                else 
                {
                    return "";
                }
            }
        }
    }
}