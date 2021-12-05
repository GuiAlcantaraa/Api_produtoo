using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.models
{
    public class Venda
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DataVenda { get; set; }
        public string CpfVendedor { get; set; }
        public string CpfCliente { get; set; }
        public string NomeCliente { get; set; }
        public float ValorTotal { get; set; }
        public List<VendaItens> Itens {get; set;}
    }
}
