using PlaneSpotter.Services;
using PlaneSpotter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlaneSpotter.Web.Controllers
{
    public class PlaneSpotterController : Controller
    {
        private readonly IPlaneSpotterService planeSpotterService;

        public PlaneSpotterController(IPlaneSpotterService _planeSpotterService)
        {
            this.planeSpotterService = _planeSpotterService;
        }

        public ActionResult Index()
        {

            //var x = planeSpotterService.GetAllSpottingInfo();

            ////Save
            ////planeSpotterService.SaveSpot();


            //return View(x);


            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}
