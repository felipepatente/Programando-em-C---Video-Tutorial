using System;

namespace ObjetoTransferencia
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string nome { get; set; }
        public DateTime dataNasimento { get; set; }
        public Boolean sexo { get; set; }
        public decimal limiteCompra { get; set; }
    }
}
