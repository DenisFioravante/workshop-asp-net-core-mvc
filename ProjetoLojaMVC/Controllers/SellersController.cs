using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoLojaMVC.Models;
using ProjetoLojaMVC.Models.ViewModels;
using ProjetoLojaMVC.Services;
using ProjetoLojaMVC.Services.Excepiton;

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
        public async Task<IActionResult>Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindALLAsync(); //busca todos os departamentos do banco
            var viewModel = new SellerFormViewModel { Departaments = departments };//pega todos os departamentos 
            return View(viewModel);

        }

        [HttpPost]//INDICA QUE A AÇÃO É DE POST
        [ValidateAntiForgeryToken]//PROTEÇÃO CONTRA O ENVIO DE DADOS MALICIOSOS APROVEITANDO A AUTENTICAÇÃO
        public async Task<IActionResult> Create(Seller seller)// Recebe o objeto da requisição e o estancia É A ESSA AÇÃO QUE IRÁ DE FATO INSERIR O VENDEODR NO BANCO
        {
            if (!ModelState.IsValid)//testa se a view é válida
            {
                var departaments = await _departmentService.FindALLAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };

                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));//depois de estanciar o objeto a ação irá redirecionar para o método Index
        }

        public async Task<IActionResult> Delete(int? id)//o (?) indica que o ID é opcional
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});//será direcionar para a página de erro
            }

            var obj =  await _sellerService.FindByIdAsync(id.Value);//pega o valor do ID
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete (int id)
        {
           await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);//pega o valor do ID
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }
        //método get
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            //abrir a tela de edição
            List<Departament> departaments = await _departmentService.FindALLAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departaments = departaments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)//testa se a view é válida
            {
                var departaments = await _departmentService.FindALLAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };

                return View(viewModel);
            }

            if ( id!= seller.Id )
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));

            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
                

        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier//o Current? é opcional o ?? caso ele seja nulo executa HttpContext.TraceIdentifier


            };

            return View(viewModel);
        }

    }
}