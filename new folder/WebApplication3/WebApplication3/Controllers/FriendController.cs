using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class FriendController : Controller
    {
        //
        // GET: /Friend/
        DataClasses1DataContext dc = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View(dc.Friends.ToList());
        }
        public ActionResult Add()
        { return View(); }
      
        [HttpPost]
        public ActionResult Index(Friend f)
        {
            if (ModelState.IsValid)
            {
                Friend p = new Friend();
                p.FriendName = f.FriendName;
                p.Place = f.Place;
                p.Fid = f.Fid;
                dc.Friends.InsertOnSubmit(p);
                dc.SubmitChanges();

                return RedirectToAction("Index");
            
            }

            return View("Add");
        }
         public ActionResult Edit(int id)
        { 
            return View(dc.Friends.First(s=>s.FriendID==id));
        }
        [HttpPost]
         public ActionResult EditOK(int id)
         {

             var a = dc.Friends.FirstOrDefault(s => s.FriendID== id);
             a.Fid=Request["fid"];
             a.FriendName = Request["name"];
             a.Place = Request["place"];
             dc.SubmitChanges();
             return RedirectToAction("Index");
         }
        
         public ActionResult Delete(int id)
         {
             return View(dc.Friends.First(s => s.Fid == id.ToString()));
         }
        
         public ActionResult DeleteOK(int id)
         {
             var s = dc.Friends.First(x => x.Fid == id.ToString());
             dc.Friends.DeleteOnSubmit(s);
             dc.SubmitChanges();
             return RedirectToAction("Index");
         }
         public ActionResult Valid()
         {
             return View();
         }

         [HttpPost]
         public ActionResult Valid(WebApplication3.Models.Friend s)
         {
             if (s.FriendName == null)
             {
                 ModelState.AddModelError("Name", "Please provide a valid Name");
             }
             if (ModelState.IsValid)
             {
                 string name = Request["Name"];
                 string pt = Request["Place"];

                 Friend p = new Friend();
                 p.FriendName = name;
                 p.Place = pt;

                 dc.Friends.InsertOnSubmit(p);
                 dc.SubmitChanges();

                 return RedirectToAction("Index");
             }
             return View();
         }
	}
}