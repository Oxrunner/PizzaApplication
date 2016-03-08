using PizzaApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PizzaApplication.Controllers
{
    public class PizzaController : Controller
    {
        PizzaStoreContext context = new PizzaStoreContext();

        public ActionResult Index()
        {
            Basket basket = new Basket();
            ViewBag.Basket = basket;
            ViewBag.Toppings = context.Toppings.ToList();
            return View(context.Pizzas.ToList());
        }

    }
}