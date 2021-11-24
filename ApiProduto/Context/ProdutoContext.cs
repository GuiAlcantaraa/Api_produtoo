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
     
        public ProdutoContext()
        {
            var cliente = new MongoClient(conexaoMongoDB);
            var server = cliente.GetDatabase(DataBaseName);
            _produtos = server.GetCollection<Produto>("Produtos");
            
        }
    }
    public class CategoriaContext
    {

        public MongoDatabase Database;
        public String DataBaseName = "test";
        string conexaoMongoDB = "mongodb+srv://Api_Produto:tv88925431@cluster0.th13k.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";


        public IMongoCollection<Categoria> _categorias;
        public CategoriaContext()
        {
            var cliente = new MongoClient(conexaoMongoDB);
            var server = cliente.GetDatabase(DataBaseName);
            _categorias = server.GetCollection<Categoria>("categorias");
        }
        
    }
    public class UsuarioContext
    {

        public MongoDatabase Database;
        public String DataBaseName = "test";
        string conexaoMongoDB = "mongodb+srv://Api_Produto:tv88925431@cluster0.th13k.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";


        public IMongoCollection<Usuario> _usuarios;
        public UsuarioContext()
        {
            var cliente = new MongoClient(conexaoMongoDB);
            var server = cliente.GetDatabase(DataBaseName);
            _usuarios = server.GetCollection<Usuario>("Usuarios");
        }

    }
}

