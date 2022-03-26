 
using Microsoft.AspNetCore.Mvc;
using Bakery.Models;
using System.Collections.Generic;
namespace Bakery.Controllers
{
  public class HomeController  : Controller
  {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }
  }
}