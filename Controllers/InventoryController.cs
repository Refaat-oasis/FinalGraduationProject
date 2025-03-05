using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ThothSystemVersion1.BusinessLogicLayers;
using ThothSystemVersion1.Database;
using ThothSystemVersion1.Hubs;
using ThothSystemVersion1.Models;

namespace ThothSystemVersion1.Controllers
{
    public class InventoryController : Controller
    {

        private readonly InventoryBussinesLogicLayer _businessLogicL;
        private readonly IHubContext<ProductHub> productHub;
        private readonly ThothContext con;
        public InventoryController(InventoryBussinesLogicLayer businessLogicL, IHubContext<ProductHub> productHub, ThothContext context)
        {
            _businessLogicL = businessLogicL;
            this.productHub = productHub;
             con = context;
        }

        // refaat section
        [HttpGet]
        public IActionResult NewPaper() { 
            return View();
        }
        [HttpPost]
        public IActionResult NewPaper(Paper newPaper) {
            try {

                if (!ModelState.IsValid)
                {
                    return View("");

                }
                else {

                    _businessLogicL.addPaper(newPaper);
                    return RedirectToAction("", "");
                }
            
            } catch (Exception ex) {
                return BadRequest(ex);
            }
            
        }
        
        [HttpGet]
        public IActionResult NewInk() { 
            return View();
        }
        [HttpPost]
        public IActionResult NewInk(Ink newInk) {
            try
            {

                if (!ModelState.IsValid)
                {
                    _businessLogicL.addInk(newInk);
                    return RedirectToAction("", "");
                }
                else { 
                    return View() ;
                }

            } catch (Exception ex) {

                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult NewSupply() {
             return View();
        }
        [HttpPost]
        public IActionResult NewSupply(Supply newSupply) {

            try
            {

                if (!ModelState.IsValid)
                {
                    _businessLogicL.addSupply(newSupply);
                    return RedirectToAction("", "");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }



        // Mariam section
        [HttpGet]
        public IActionResult EditVendor(int vendorID) {   
            Vendor foundVendor = _businessLogicL.GetVendorByID(vendorID);
            return View("~/Views/Inventory/EditVendor.cshtml", foundVendor);      
        
        }

        [HttpPost]
        public IActionResult EditVendor(int vendorID, Vendor vendor) {
            if (vendor == null) {
                return BadRequest("invalid data was added");
                
            }
            _businessLogicL.EditVendor(vendorID, vendor);
            return RedirectToAction("ViewAllVendor","Inventory");
        
        }


        // Sherwet section


        // Sandra section
        [HttpGet]
        public IActionResult ViewAllVendor()
        {
            List<Vendor> vendorList = _businessLogicL.ViewAllVendor();
            return View("~/Views/Inventory/ViewAllVendor.cshtml", vendorList);
        }



        // signalR using view the inventory

        public async Task<IActionResult> ViewAllinventory()
        {
            var papers = con.Papers.ToList();
            await productHub.Clients.All.SendAsync("CheckPaperReorderPoint");
            return View(papers);
        }

        //public IActionResult ViewAllinventory()
        //{
        //    var papers = con.Papers.ToList();
        //    productHub.Clients.All.SendAsync("CheckReorderPoint", papers, "paper");
        //    return View(papers);
        //}
        //public IActionResult ViewAllinventory1()
        //{
        //    var inks = con.Inks.ToList();
        //    productHub.Clients.All.SendAsync("CheckReorderPoint", inks, "ink");
        //    return View(inks);
        //}
        //public IActionResult ViewAllinventory2()
        //{
        //    var supplies = con.Supplies.ToList();
        //    productHub.Clients.All.SendAsync("CheckReorderPoint", supplies, "supply");
        //    return View(supplies);
        //}

    }
}
