using CadastroPaciente.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPaciente.Controllers
{
    public class LoginController : Controller
    {
      
        public IActionResult Index()
        {

            if (Request.Query["autenticado"] == "NAO")
            {

            
                ViewBag.Erro = "Usuário ou Senha Inválidos";
            }
            else
            {
                ViewBag.Erro = "";
            }
        
            return View();
        }

        public void Autenticar(string usuario,string senha)
        {
            Usuario usu = new Usuario(usuario, senha);
            if (usu.autenticar())
            {
                Response.Cookies.Append("Autenticado", "SIM");
                Response.Cookies.Append("usuario", usuario);


                Response.Redirect("/home");
            }
            else
            {



                
                Response.Redirect("/Login?Autenticado=NAO");
            }
        }

        public void sair()
        {
            Response.Cookies.Delete("Autenticado");
            Response.Cookies.Delete("usuario");
            Response.Redirect("/");
        }
    }
}
