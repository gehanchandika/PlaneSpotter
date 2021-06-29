using PlaneSpotter.Services;
using System;
using System.Collections.Generic;
using PlaneSpotter.Web.API.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using PlaneSpotter.Models;
using System.Net.Http;
using System.Net;

namespace PlaneSpotter.Web.API.Controllers
{

    [RoutePrefix("v1/spotter")]
    public class PlaneSpotterController : BaseController
    {
        private readonly IPlaneSpotterService planeSpotterService;

        public PlaneSpotterController(IPlaneSpotterService planeSpotterService)
        {
            this.planeSpotterService = planeSpotterService;
        }

        [HttpGet]
        [Route("GetAllSpottedPlanes")]
        public HttpResponseMessage GetAllSpottingInfo()
        {
            try
            {
                var list = planeSpotterService.GetAllSpottingInfo();
                var responce = Request.CreateResponse(HttpStatusCode.OK, list);
                return responce;
            }
            catch (Exception ex)
            {
                var responce = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return responce;
            }
        }

        [HttpDelete]
        [Route("DeleteSelectedSpottedPlanes")]
        public HttpResponseMessage DeleteSelectedSpottedPlanesInfo(long id)
        {
            try
            {
                planeSpotterService.DeleteSelectedSpottedPlanes(id);
                var responce = Request.CreateResponse(HttpStatusCode.OK);
                return responce;
            }
            catch (Exception ex)
            {
                var responce = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return responce;
            }
        }

        [HttpPost]
        [Route("SaveSpottedPlanes")]
        public HttpResponseMessage SaveSpottedPlanesInfo([FromBody] SpottingModel model)
        {
            try
            {
                planeSpotterService.SavePlaneSpotInfo(model);
                var responce = Request.CreateResponse(HttpStatusCode.OK);
                return responce;
            }
            catch (Exception ex)
            {
                var responce = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return responce;
            }
        }

        [HttpPut]
        [Route("UpdateSpottedPlanes")]
        public HttpResponseMessage UpdateSpottedPlanesInfo([FromBody] SpottingModel model)
        {
            try
            {
                planeSpotterService.UpdatePlaneSpotInfo(model);
                var responce = Request.CreateResponse(HttpStatusCode.OK);
                return responce;
            }
            catch (Exception ex)
            {
                var responce = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return responce;
            }
        }
    }
}