﻿namespace aspnet_store.Models.Entities
{
    public class Servico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public string Descricao { get; set; }
        public int PrazoEntrega { get; set; }
    }
}
