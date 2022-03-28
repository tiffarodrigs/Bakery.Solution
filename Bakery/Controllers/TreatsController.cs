using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Bakery.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
namespace Bakery.Controllers
{
  //[Authorize]
  public class TreatsController  : Controller
  {
    private readonly BakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager,BakeryContext db)
    {
      _userManager = userManager;
      _db=db;
    }

   // public ActionResult Index()
    public async Task<ActionResult> Index()
    {
      //      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // Console.WriteLine("userId" +userId);
      // var currentUser = await _userManager.FindByIdAsync(userId);
      // var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
      // return View(userTreats);
        List<Treat> model = _db.Treats.ToList();
        return View(model);
    }
    [Authorize]
   public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat,int FlavorId)
    {
    var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var currentUser = await _userManager.FindByIdAsync(userId);
    treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if(FlavorId!=0)
      {
        _db.FlavorTreat.Add(new FlavorTreat{FlavorId=FlavorId,TreatId=treat.TreatId} );
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
[AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
      .Include(m => m.JoinEntities)
      .ThenInclude(m => m.Flavor)
      .FirstOrDefault(m => m.TreatId == id);
      return View(thisTreat);

    }
[Authorize]
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
[Authorize]
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
[Authorize]
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
    [Authorize]
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