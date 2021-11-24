using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.models
{
    public class ProdutoEstoque
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Codigo { get; set; }
        public int qtde { get; set; }
        
    }
}
