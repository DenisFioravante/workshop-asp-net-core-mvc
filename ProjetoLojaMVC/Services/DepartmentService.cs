using ProjetoLojaMVC.Models;
using System.Collections.Generic;
using System.Linq;


namespace ProjetoLojaMVC.Services
{
    public class DepartmentService
    {
        private readonly ProjetoLojaMVCContext _context; //readonly faz com que a dependencia não seja alterada 

        public DepartmentService(ProjetoLojaMVCContext context)//criando a dependência  HAVERÁ UMA MESMA DEPENDÊNCIA NO SELLERSCONTROLER 
        {
            _context = context;
        }

        public List<Departament> FindALL()
        {
            return _context.Departament.OrderBy(x => x.Name).ToList();//irá retornar uma lista ordenada por nome
          
        }
    }
}
