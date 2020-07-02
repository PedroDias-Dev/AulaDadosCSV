using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Aula_27_28_29_30
{
    public class Produto : IProduto
    {   
        // ATRIBUTOS BASICOS
        public int Codigo {get; set;}
        public string Nome {get; set;}
        public float Preco {get; set;}

        private const string PATH = "Database/produto.csv";
        //(erro)  private const string DIRECTORY = "Aula_27_28_29_30/Database";

        //(erro)  file.Directory.CreateDirectory(PATH); // If the directory already exists, this method does nothing.

        public Produto(){
            /// <summary>
            /// Metodo construtor:    
            /// Verifica a existencia da pasta ou do arquivo
            /// Caso nao exista, ele cria o diretorio e o arquivo
            /// </summary>
            /// <returns></returns>
            string pasta = PATH.Split("/")[0];

            if (!Directory.Exists(pasta)) {
                Directory.CreateDirectory(pasta);
            }
            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }
        }

        public void Cadastrar(Produto prod ){
            /// <summary>
            /// Cadastra novos produtos
            /// </summary>
            /// <returns></returns>
            var linha = new string[] { PrepararLinha(prod) };

            File.AppendAllLines(PATH, linha);
        }

        public string PrepararLinha(Produto p){
            /// <summary>
            /// Organiza a escrita no .csv
            /// </summary>
            /// <value></value>
            return $"código={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }

        public List<Produto> Ler(){
            /// <summary>
            /// Le os arquivos no .csv
            /// </summary>
            /// <typeparam name="Produto"></typeparam>
            /// <returns></returns>
            List<Produto> produtos = new List<Produto>(); // Cria uma nova lista
            

            string[] linhas = File.ReadAllLines(PATH); // Le as linhas

            foreach  (string linha in linhas){
                string[] dado = linha.Split(";");

                Produto p = new Produto();
                p.Codigo = Int32.Parse( Separar(dado[0]) );
                p.Nome = Separar( dado[1] );
                p.Preco = float.Parse( Separar(dado[2]) );

                produtos.Add(p);
            }
            produtos = produtos.OrderBy(y => y.Nome).ToList();

            return produtos;
        }

        public void Remover(string _termo){
            /// <summary>
            /// Lista que servira como backup para as linhas do .csv
            /// </summary>
            /// <param name="_nome"></param>
            /// <returns></returns>
            List<string> linhas = new List<string>();

            // StreamReader le o nosso .csv
            using(StreamReader arquivo = new StreamReader(PATH)){
                string linha;
                while((linha = arquivo.ReadLine()) != null){
                    linhas.Add(linha);
                }
            }
            //remove as linhas que tiverem o argumento (_termo) : Tagima p ex
            linhas.RemoveAll(l => l.Contains(_termo));

            //Reescreve o .csv
            using(StreamWriter output = new StreamWriter(PATH)){
                foreach(string ln in linhas)
                {
                    output.Write(ln + "\n");
                }
            }
        }


        public List<Produto> Filtrar(string _nome){
            return Ler().FindAll(x => x.Nome == _nome);
        }

        

        private string Separar(string _coluna){
            /// <summary>
            /// Separa as colunas
            /// </summary>
            /// <returns></returns>
            return _coluna.Split("=")[1]; 
        }

        /// <summary>
        /// Altera um produto
        /// </summary>
        /// <param name="_produtoAlterado">Objeto de Produto</param>
        public void Alterar(Produto _produtoAlterado){

            // Criamos uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas = new List<string>();

            // Utilizamos a bliblioteca StreamReader para ler nosso .csv
            using(StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }
            // codigo=2;nome=Ibanez;preco=7500
            // linhas.RemoveAll(z => z.Split(";")[0].Contains(_produtoAlterado.Codigo.ToString()));
            
            // codigo= 2; nome=Ibanez;preco=7500
            //linhas.RemoveAll(z => z.Split(";")[0].Split("=")[1] == _produtoAlterado.Codigo.ToString());

            linhas.RemoveAll(z => z.Split(";")[0].Contains(_produtoAlterado.Codigo.ToString()));

            // Adicionamos a linha alterada na lista de backup
            linhas.Add( PrepararLinha(_produtoAlterado) );

            // Reescrevemos nosso csv do zero
            ReescreverCSV(linhas);         
        }

        /// <summary>
        /// Reescreve o CSV (REFATORAÇAO)
        /// </summary>
        /// <param name="lines"></param>
        private void ReescreverCSV(List<string> lines){
            using(StreamWriter output = new StreamWriter(PATH)){
                foreach(string ln in lines)
                {
                    output.Write(ln + "\n");
                }
            }

        }

        public Produto(int _codigo, string _nome, float _preco){
            /// <summary>
            /// Metodo construtor:
            /// </summary>
            this.Nome = _nome;
            this.Codigo = _codigo;
            this.Preco = _preco;
        }
    }
}