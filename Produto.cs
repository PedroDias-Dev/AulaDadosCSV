using System.IO;

namespace Aula_27_28_29_30
{
    public class Produto
    {
        public int Codigo {get; set;}
        public string Nome {get; set;}
        public float Preco {get; set;}

        private const string PATH = "Database/produto.csv";
        //private const string DIRECTORY = "Aula_27_28_29_30/Database";

        //file.Directory.CreateDirectory(PATH); // If the directory already exists, this method does nothing.

        public Produto(){
            string pasta = PATH.Split("/")[0];

            if (!Directory.Exists(pasta)) {
                Directory.CreateDirectory(pasta);
            }
            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }
        }

        public void Cadastrar(Produto prod ){
            var linha = new string[] { PrepararLinha(prod) };

            File.AppendAllLines(PATH, linha);
        }

        public string PrepararLinha(Produto p){
            return $"código={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }

        public Produto(int _codigo, string _nome, float _preco){
            this.Nome = _nome;
            this.Codigo = _codigo;
            this.Preco = _preco;
        }
    }
}