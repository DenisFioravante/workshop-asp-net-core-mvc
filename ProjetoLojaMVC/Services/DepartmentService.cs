using ProjetoLojaMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLojaMVC.Services
{
    public class DepartmentService
    {
        private readonly ProjetoLojaMVCContext _context; //readonly faz com que a dependencia não seja alterada 

        public DepartmentService(ProjetoLojaMVCContext context)//criando a dependência  HAVERÁ UMA MESMA DEPENDÊNCIA NO SELLERSCONTROLER 
        {
            _context = context;
        }

        public async Task<List<Departament>> FindALLAsync()//o TASK deixa a execução assíncrona  
        {
            return await _context.Departament.OrderBy(x => x.Name).ToListAsync();//irá retornar uma lista ordenada por nome
            //await indica que será tratado como assíncrono           
        }
    }
}
