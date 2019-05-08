using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLojaMVC.Data;
using ProjetoLojaMVC.Models;
using Microsoft.EntityFrameworkCore;

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

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Departament).FirstOrDefault(obj => obj.Id == id);//POR DEFAUL ELE SÓ TRAZ O NOME DO VENDEDOR 
            //CONTUDO COM A EXPRESSÃO LAMBDA FOI INCLUÍDO O DEPARTAMENTO PARA INCLUIR A EXPRESSÃO LABDA FOI CHAMADA A BIBLIOTECA 
            // using Microsoft.EntityFrameworkCore;
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();//FAZ A ALTERAÇÃO NO BANCO DE DADOS
        }

    }
}
