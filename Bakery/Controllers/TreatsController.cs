using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Bakery.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Bakery.Controllers
{
  public class TreatsController  : Controller
  {
    private readonly BakeryContext _db;

    public TreatsController(BakeryContext db)
    {
      _db=db;
    }
    public ActionResult Index()
    {
        List<Treat> model = _db.Treats.ToList();
        return View(model);
    }
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat,int FlavorId)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if(FlavorId!=0)
      {
        _db.FlavorTreat.Add(new FlavorTreat{FlavorId=FlavorId,TreatId=treat.TreatId} );
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
      .Include(m => m.JoinEntities)
      .ThenInclude(m => m.Flavor)
      .FirstOrDefault(m => m.TreatId == id);
      return View(thisTreat);

    }

    public ActionResult Edit(int id)
    {
        var thisTreat = _db.Treats.FirstOrDefault(m => m.TreatId == id);
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
        return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat,int FlavorId)
    {
        if(FlavorId!=0)
        {
          _db.FlavorTreat.Add(new FlavorTreat{ FlavorId = FlavorId,TreatId = treat.TreatId});
          _db.SaveChanges();
        }  
        _db.Entry(treat).State = EntityState.Modified;
        _db.SaveChanges();     
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        var thisTreat = _db.Treats.FirstOrDefault(m => m.TreatId == id);
        return View(thisTreat);
    }
        [HttpPost, ActionName("Delete")]
      public ActionResult DeleteConfirmed(int id)
      {
        var thisTreat = _db.Treats.FirstOrDefault(m => m.TreatId == id);
        _db.Treats.Remove(thisTreat);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }  

    public ActionResult AddFlavor(int id)
    {  
      var flaTreatEntries = _db.FlavorTreat.Where(m => m.TreatId == id);        
        List<Flavor> flavorList = _db.Flavors.ToList();
        List<Flavor> flavors = _db.Flavors.ToList();     
        foreach(FlavorTreat ft in flaTreatEntries )
        {
          foreach(Flavor fl in flavors)
          {
            if(fl.FlavorId== ft.FlavorId)
            {
              flavorList.Remove(fl);
            }
          }
        }
        ViewBag.FlavorId = new SelectList(flavorList, "FlavorId", "FlavorName");
        ViewBag.flavorList = flavorList.Count;
        var thisTreat = _db.Treats.FirstOrDefault(m => m.TreatId == id);
        return View(thisTreat);

    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat,int FlavorId)
    {
        if(FlavorId!=0)
        {
          _db.FlavorTreat.Add(new FlavorTreat{ FlavorId = FlavorId,TreatId = treat.TreatId});
          _db.SaveChanges();
        } 
        return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
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