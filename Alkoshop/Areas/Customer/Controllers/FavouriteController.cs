﻿using Alkoshop.Database;
using Alkoshop.Models;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alkoshop.Areas.Customer.Controllers
{
    [Authorize]
    public class FavouriteController : Controller
    {
        // GET: Customer/Favourite
        public ActionResult Index()
        {

            Alkoshop.Models.Customer customer = Session["User"] as Alkoshop.Models.Customer;
            OracleConnection connection = DBMain.GetConnection();
            IList<Product> favProducts = DBGetData.getFavForCustomer(connection, customer.ID);

            Session["conn"] = DBMain.GetConnection();

            IList<Category> alcoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 1);
            IList<Category> tabaccoCategories = DBGetData.getCategories((OracleConnection)Session["conn"], 2);
            ViewBag.AlcoCategories = alcoCategories;
            ViewBag.TabaccoCategories = tabaccoCategories;

            return View(favProducts);
        }

        public ActionResult Add(int productId)
        {
            OracleConnection connection = DBMain.GetConnection();
            Alkoshop.Models.Customer customer = Session["User"] as Alkoshop.Models.Customer;
            IList<Product> favProducts = DBGetData.getFavForCustomer(connection, customer.ID);
            foreach(Product p in favProducts)
            {
                if(p.Id == productId)
                {
                    TempData["message-nosuccess"] = "Tento produkt jiz mate v oblibenych";
                    return RedirectToAction("Index", "Favourite");
                }
            }
            DBGetData.addProductToFav(connection, customer.ID, productId);
            TempData["message-success"] = "Produkt byl pridan k vasim oblibenym";
            return RedirectToAction("Index", "Favourite");
        }
        public ActionResult Remove(int productId)
        {
            OracleConnection connection = DBMain.GetConnection();
            Alkoshop.Models.Customer customer = Session["User"] as Alkoshop.Models.Customer;
            DBGetData.removeProductFromFav(connection, customer.ID, productId);
            TempData["message-success"] = "Produkt byl odebran z Vasich oblibenych";
            return RedirectToAction("Index", "Favourite");
        }
    }
}