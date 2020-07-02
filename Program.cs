using System;
using System.Collections.Generic;

namespace Aula_27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            Produto p1 = new Produto();
            p1.Codigo = 1;
            p1.Nome = "sei la";
            p1.Preco = 3000f;
            p1.Cadastrar(p1);

            //Produto p2 = new Produto();
            //p2.Codigo = 2;
            //p2.Nome = "Strinberg";
            //p2.Preco = 1000f;

            //p2.Cadastrar(p2);

            p1.Remover("Gibson");

            Produto alterado = new Produto();
            alterado.Codigo = 2;
            alterado.Nome = "Fender2";
            alterado.Preco = 2000f;
            
            p1.Alterar(alterado);
            

            List<Produto> lista = new List<Produto>();
            lista = p1.Ler();
            //lista = p2.Ler();

            foreach (Produto item in lista){
                System.Console.WriteLine($"R$ {item.Preco} - {item.Nome}");
            }



        }
    }
}
