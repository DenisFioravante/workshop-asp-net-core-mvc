using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLojaMVC.Data;
using ProjetoLojaMVC.Models;

namespace ProjetoLojaMVC.Services
{
    public class SellerService
    {
        private readonly ProjetoLojaMVCContext _context; //readonly faz com que a dependencia não seja alterada 

        public SellerService (ProjetoLojaMVCContext context)//criando a dependência  HAVERÁ UMA MESMA DEPENDÊNCIA NO SELLERSCONTROLER 
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();//retorna uma lista com todos os vendedores
        }

        public void Insert(Seller obj)//INSERE O FUNCIONÁRIO CADASTRADO NO BANCO DE DADOS
        {
            //obj.Departament = _context.Departament.First();//associa o primeiro departamento ao novo vendadedor cadastrado (APENAS PARA TESTE)
            _context.Add(obj);
            _context.SaveChanges();
        }



    }
}
