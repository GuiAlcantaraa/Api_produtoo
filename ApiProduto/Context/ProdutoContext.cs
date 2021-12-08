using ApiProduto.models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.Context
{
    public class ProdutoContext
    {

        public MongoDatabase Database;
        public String DataBaseName = "test";
        string conexaoMongoDB = "mongodb+srv://Api_Produto:tv88925431@cluster0.th13k.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";

        public IMongoCollection<Produto> _produtos;
        public IMongoCollection<Categoria> _categoria;
        public IMongoCollection<Usuario> _usuarios;
        public IMongoCollection<Venda> _VendaProduto;



        public ProdutoContext()
        {
            var cliente = new MongoClient(conexaoMongoDB);
            var server = cliente.GetDatabase(DataBaseName);
            _produtos = server.GetCollection<Produto>("Produtos");
            _categoria = server.GetCollection<Categoria>("Categoria");
            _usuarios = server.GetCollection<Usuario>("Usuario");
            _VendaProduto = server.GetCollection<Venda>("Venda");

            
        }

    }
}

