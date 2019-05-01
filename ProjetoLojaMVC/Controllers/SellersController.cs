using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoLojaMVC.Models;
using ProjetoLojaMVC.Services;

namespace ProjetoLojaMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        
        public SellersController (SellerService sellerService)//criando a dependencia
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]//INDICA QUE A AÇÃO É DE POST
        [ValidateAntiForgeryToken]//PROTEÇÃO CONTRA O ENVIO DE DADOS MALICIOSOS APROVEITANDO A AUTENTICAÇÃO
        public IActionResult Create(Seller seller)// Recebe o objeto da requisição e o estancia É A ESSA AÇÃO QUE IRÁ DE FATO INSERIR O VENDEODR NO BANCO
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));//depois de estanciar o objeto a ação irá redirecionar para o método Index
        }
    }
}