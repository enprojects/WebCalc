using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCalc.Interfaces;
using WebCalc.Models;

namespace WebCalc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICalculateService _serv;

        public HomeController(ICalculateService serv)
        {
            _serv = serv;
        }

        public IActionResult Index()
        {
            return View();
        }
         
    }
}
