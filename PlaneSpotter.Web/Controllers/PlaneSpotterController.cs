using PlaneSpotter.Services;
using PlaneSpotter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
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
            string PlaneSpotterAPIServiceURL = WebConfigurationManager.AppSettings["PlaneSpotterAPIServiceURL"];

            var objPS = new PlaneSpotterViewModel();

            objPS.PlaneSpotterAPIServiceURL = PlaneSpotterAPIServiceURL;

            ViewBag.Message = "Your application description page.";

            return View("Index", objPS);
        }
    }
}
