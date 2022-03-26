using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Bakery.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Bakery.Controllers
{
  public class FlavorsController  : Controller
  {
    private readonly BakeryContext _db;

    public FlavorsController(BakeryContext db)
    {
      _db=db;
    }
    public ActionResult Index()
    {
        List<Flavor> model = _db.Flavors.ToList();
        return View(model);
    }
    public ActionResult Create()
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor,int TreatId)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      if(TreatId!=0)
      {
        _db.FlavorTreat.Add(new FlavorTreat{TreatId=TreatId,FlavorId=flavor.FlavorId} );
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisFlavor = _db.Flavors
      .Include(m => m.JoinEntities)
      .ThenInclude(m => m.Treat)
      .FirstOrDefault(m => m.FlavorId == id);
      return View(thisFlavor);

    }

    public ActionResult Edit(int id)
    {
        var thisFlavor = _db.Flavors.FirstOrDefault(m => m.FlavorId == id);
        ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
        return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor,int TreatId)
    {
        if(TreatId!=0)
        {
          _db.FlavorTreat.Add(new FlavorTreat{ TreatId = TreatId,FlavorId = flavor.FlavorId});
          _db.SaveChanges();
        }  
        _db.Entry(flavor).State = EntityState.Modified;
        _db.SaveChanges();     
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        var thisFlavor = _db.Flavors.FirstOrDefault(m => m.FlavorId == id);
        return View(thisFlavor);
    }
        [HttpPost, ActionName("Delete")]
      public ActionResult DeleteConfirmed(int id)
      {
        var thisFlavor = _db.Flavors.FirstOrDefault(m => m.FlavorId == id);
        _db.Flavors.Remove(thisFlavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }  

    public ActionResult AddTreat(int id)
    {  
      var flaTreatEntries = _db.FlavorTreat.Where(m => m.FlavorId == id);        
        List<Treat> treatList = _db.Treats.ToList();
        List<Treat> treats = _db.Treats.ToList();     
        foreach(FlavorTreat ft in flaTreatEntries )
        {
          foreach(Treat trt in treats)
          {
            if(trt.TreatId== ft.TreatId)
            {
              treatList.Remove(trt);
            }
          }
        }
        ViewBag.TreatId = new SelectList(treatList, "TreatId", "TreatName");
        ViewBag.treatList = treatList.Count;
        var thisFlavor = _db.Flavors.FirstOrDefault(m => m.FlavorId == id);
        return View(thisFlavor);

    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor,int TreatId)
    {
        if(TreatId!=0)
        {
          _db.FlavorTreat.Add(new FlavorTreat{ TreatId = TreatId,FlavorId = flavor.FlavorId});
          _db.SaveChanges();
        } 
        return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
      //This function deletes only the treat from this particular flavor.ie.it deltes entry from FlavorTreat  table
        //This does not delete the treat from the treat table
          var joinEntry = _db.FlavorTreat.FirstOrDefault(m => m.FlavorTreatId == joinId);
          _db.FlavorTreat.Remove(joinEntry);
          _db.SaveChanges();
          return RedirectToAction("Index");
    }
    
  }
}