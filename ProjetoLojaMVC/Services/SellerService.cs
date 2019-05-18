using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLojaMVC.Data;
using ProjetoLojaMVC.Models;
using Microsoft.EntityFrameworkCore;
using ProjetoLojaMVC.Services.Excepiton;

namespace ProjetoLojaMVC.Services
{
    public class SellerService
    {
        private readonly ProjetoLojaMVCContext _context; //readonly faz com que a dependencia não seja alterada 

        public SellerService (ProjetoLojaMVCContext context)//criando a dependência  HAVERÁ UMA MESMA DEPENDÊNCIA NO SELLERSCONTROLER 
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();//retorna uma lista com todos os vendedores
        }

        public async Task InsertAsync(Seller obj)//INSERE O FUNCIONÁRIO CADASTRADO NO BANCO DE DADOS
        {
            //obj.Departament = _context.Departament.First();//associa o primeiro departamento ao novo vendadedor cadastrado (APENAS PARA TESTE)
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Departament).FirstOrDefaultAsync(obj => obj.Id == id);//POR DEFAUL ELE SÓ TRAZ O NOME DO VENDEDOR 
            //CONTUDO COM A EXPRESSÃO LAMBDA FOI INCLUÍDO O DEPARTAMENTO PARA INCLUIR A EXPRESSÃO LABDA FOI CHAMADA A BIBLIOTECA 
            // using Microsoft.EntityFrameworkCore;
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();//FAZ A ALTERAÇÃO NO BANCO DE DADOS
        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");

            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();

            }

            catch (DbConcurrencyException e)//caso tenha alguma concorrencia no banco de DADOS
            {
                throw new DbConcurrencyException(e.Message);//ESSA É UMA EXCESSÃO DA CAMADA DE DADOS, NOS ESTAMOS CONTROLANDO AS EXECSSÕES DE SERVIÇO   
            }
          
            
        }
    }
}
