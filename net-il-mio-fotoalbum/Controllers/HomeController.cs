﻿using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace net_il_mio_fotoalbum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var ctx = new FotoContext();
             

            var fotos = ctx.Fotos.ToArray();


            return View(fotos);
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


        //SHOW


        [HttpGet]
        public IActionResult Show(int Id)
        {
            using var ctx = new FotoContext();

           

            Foto fotoTrovata = ctx.Fotos.Where(foto => foto.Id == Id).Include(foto => foto.Categorie)
                .FirstOrDefault();

            if (fotoTrovata == null)
            {
                return NotFound($"formData {Id} non trovata");
            }

            return View(fotoTrovata);
        }



        // CREATE

        //[Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            using (FotoContext ctx = new FotoContext())
            {
                List<Categoria> categorie = ctx.Categorie.ToList();
                List<SelectListItem> listCategorie = new List<SelectListItem>();

                foreach (Categoria categoria in categorie)
                {
                    listCategorie.Add(new SelectListItem()
                    {
                        Text = categoria.Name,
                        Value = categoria.Id.ToString()
                    });
                }
                
             
                FotoFormModel model = new FotoFormModel();
                model.Foto = new Foto();

                model.CategorieSelezionabili = listCategorie;
               
          

                return View("Create", model);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FotoFormModel formData)
        {

            if (!ModelState.IsValid)
            {

                using (FotoContext ctx = new FotoContext())
                {
                    List<Categoria> categorie = ctx.Categorie.ToList();
                    List<SelectListItem> listCategorie = new List<SelectListItem>();

                    foreach (Categoria categoria in categorie)
                    {
                        listCategorie.Add(new SelectListItem()
                        {
                            Text = categoria.Name,
                            Value = categoria.Id.ToString()
                        });
                    }

                    formData.CategorieSelezionabili = listCategorie;

                    return View("Create", formData);
                }

            }

            using (FotoContext ctx = new FotoContext())
            {


                string url = "/img/";
                Foto nuovaFoto = new Foto();
                nuovaFoto.Title = formData.Foto.Title;
                nuovaFoto.Description = formData.Foto.Description;
                nuovaFoto.Url = url + formData.Foto.Url;
                nuovaFoto.Categorie = new List<Categoria>();


                if (formData.CategorieSelezionate != null)
                {
                    foreach (string selectedCategoriaId in formData.CategorieSelezionate)
                    {
                        int selectedCategoriaInt = int.Parse(selectedCategoriaId);
                        Categoria categoria = ctx.Categorie.Where(i => i.Id == selectedCategoriaInt).FirstOrDefault();
                        nuovaFoto.Categorie.Add(categoria);
                    }
                }

                ctx.Fotos.Add(nuovaFoto);
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }
        }


    }
}