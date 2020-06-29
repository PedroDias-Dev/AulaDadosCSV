using System;

namespace Aula_27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            Produto p1 = new Produto();
            p1.Codigo = 1;
            p1.Nome = "Gibson";
            p1.Preco = 3000f;

            p1.Cadastrar(p1);

            Produto p2 = new Produto();
            p1.Codigo = 2;
            p1.Nome = "Strinberg";
            p1.Preco = 1000f;

            p1.Cadastrar(p2);


            
        }
    }
}
