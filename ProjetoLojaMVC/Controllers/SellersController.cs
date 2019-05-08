using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoLojaMVC.Models;
using ProjetoLojaMVC.Models.ViewModels;
using ProjetoLojaMVC.Services;

namespace ProjetoLojaMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        
        public SellersController (SellerService sellerService, DepartmentService departmentService)//criando a dependencia
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindALL(); //busca todos os departamentos do banco
            var viewModel = new SellerFormViewModel { Departaments = departments };//pega todos os departamentos 
            return View(viewModel);

        }

        [HttpPost]//INDICA QUE A AÇÃO É DE POST
        [ValidateAntiForgeryToken]//PROTEÇÃO CONTRA O ENVIO DE DADOS MALICIOSOS APROVEITANDO A AUTENTICAÇÃO
        public IActionResult Create(Seller seller)// Recebe o objeto da requisição e o estancia É A ESSA AÇÃO QUE IRÁ DE FATO INSERIR O VENDEODR NO BANCO
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));//depois de estanciar o objeto a ação irá redirecionar para o método Index
        }

        public IActionResult Delete(int? id)//o (?) indica que o ID é opcional
        {
            if(id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);//pega o valor do ID
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete (int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);//pega o valor do ID
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

    }
}