﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoLojaMVC.Models;

namespace ProjetoLojaMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Departament> list = new List<Departament>();

            list.Add(new Departament { Id = 1, Name = "Eletronics"});
            list.Add(new Departament { Id = 2, Name = "Fashion" });
            return View(list); /*dessa formar está enviado dados do controller para a view*/
        }
    }
}