using CadastroPaciente.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace CadastroPaciente.Controllers
{
    public class PacienteController : Controller
    {
        // GET: PacienteController
        
        public ActionResult Index()
        {
            string v_Paciente = Request.Query["paciente"];
            Paciente paciente = new Paciente();
            paciente = paciente.Ver(v_Paciente);

            string usuario = Request.Cookies["usuario"];
            Log.salvar(v_Paciente, usuario);

            ViewBag.usuario = usuario;


            return View(paciente);
        }

        // GET: PacienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PacienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PacienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PacienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PacienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
