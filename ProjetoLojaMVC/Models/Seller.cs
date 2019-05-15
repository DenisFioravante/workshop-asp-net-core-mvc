using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLojaMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]//salário com dias casa DECIMAIS
        public double BaseSalary { get; set; }

        [Display(Name = "Birth Date")]//Irá aparecer na página da forma que foi escrita
        [DataType(DataType.Date)]//customiza o forma da DATA na página
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]//configura o formato da data
        public DateTime BirthDate { get; set; }
        public Departament Departament { get; set; }
        public int DepartamentId { get; set; }// ao criar essa prorpiedade o entiti framework irá garantir que o ID nunca seja nulo
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Departament departament)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Departament = departament;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
