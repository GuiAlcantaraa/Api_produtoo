using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.models
{
    public class Produto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string DescricaoP { get; set; }
        public float PrecoVenda  { get; set; }
        public float PrecoCusto { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public int Estoque { get; set; }
        public string Imagem { get; set; }
        public float AlturaCm { get; set; }
        public float LarguraCm { get; set; }
        public float ComprimentoCm { get; set; }
        public string CategoriaProduto { get; set; }
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public int EstoqueAtual { get; set; }
        public int EstoqueMinimo { get; set; }
       

    }
}
