using CadastroPaciente.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPaciente.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        
        public IActionResult Index()
        {

            if (Request.Cookies["autenticado"] == "SIM") { 

            try { 
            string pesquisa = Request.Form["pesquisa"].ToString();
                string tipo = Request.Form["tipo"].ToString();

                if (!String.IsNullOrEmpty(pesquisa) && !String.IsNullOrEmpty(tipo))
                {
                    
                    Paciente paciente = new Paciente();


                    ViewBag.pacientes = paciente.Listar(tipo,pesquisa);

                }
            }
            catch {
                ViewBag.pacientes = new List<Paciente>();
            }

                ViewBag.usuario = Request.Cookies["usuario"];

            return View();
            }
            else
            {
                Response.Redirect("/Login");
                ViewBag.pacientes = new List<Paciente>();
                
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
