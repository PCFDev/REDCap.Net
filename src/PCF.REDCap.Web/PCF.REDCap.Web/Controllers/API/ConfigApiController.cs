using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using PCF.REDCap.Web.Attributes;
using PCF.REDCap.Web.Data.Repositories;
using PCF.REDCap.Web.Helpers;
using PCF.REDCap.Web.Http;
using PCF.REDCap.Web.Models.API.Input.Config;
using PCF.REDCap.Web.Models.API.View.Config;

namespace PCF.REDCap.Web.Controllers.API
{
    [ClaimAuthorize(ClaimTypes.GroupSid, "S-1-5-21-620581126-1294811165-624655392-31026")]
    public class ConfigApiController : ApiController
    {
        [HttpDelete, ValidateAntiForgeryHeader]
        public HttpResponseMessage Delete(int Id)
        {
            var config = Repositories.Instance.Config.Get(Id);
            if (config == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Success = false });//TODO: Proper error response models

            if (Repositories.Instance.Config.Delete(config.Id))
                return Request.CreateResponse(HttpStatusCode.OK, new DeleteViewModel() { Success = true });
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);//TODO: Proper error response models
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var configs = Repositories.Instance.Config.Get();
            return Request.CreateResponse(HttpStatusCode.OK, new GetViewModel(configs) { Success = true });
        }

        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            var config = Repositories.Instance.Config.Get(Id);
            if (config == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Success = false });//TODO: Proper error response models
            return Request.CreateResponse(HttpStatusCode.OK, new GetIdViewModel(config) { Success = true });
        }

        [HttpPatch, ValidateAntiForgeryHeader]
        public HttpResponseMessage Patch(int Id, [FromBody]PatchInputModel model)
        {
            var config = Repositories.Instance.Config.Get(Id);
            if (config == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Success = false });//TODO: Proper error response models

            //TODO: Change tracking
            if (ModelState.IsValid)
            {
                config = Repositories.Instance.Config.Update(config.Id, model.GetDTO(config));
                if (config != null)
                    return Request.CreateResponse(HttpStatusCode.OK, new PatchViewModel(config) { Success = true });
                ModelState.AddModelError("", "Error saving config.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);//TODO: Proper error response models
        }

        [HttpPost, ValidateAntiForgeryHeader]
        public HttpResponseMessage Post([FromBody]PostInputModel model)
        {
            if (ModelState.IsValid)
            {
                var config = Repositories.Instance.Config.Add(model.GetDTO());
                if (config != null)
                {
                    var url = new Uri(Url.Link("API/Configs", new { id = config.Id }));
                    return Request.CreateRedirectResponse(HttpStatusCode.Created, url, new PostViewModel(config) { Success = true });
                }
                ModelState.AddModelError("", "Error saving config.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);//TODO: Proper error response models
        }

        [HttpPut, ValidateAntiForgeryHeader]
        public HttpResponseMessage Put(int Id, [FromBody]PutInputModel model)
        {
            var config = Repositories.Instance.Config.Get(Id);
            if (config == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Success = false });//TODO: Proper error response models

            if (ModelState.IsValid)
            {
                config = Repositories.Instance.Config.Update(config.Id, model.GetDTO());
                if (config != null)
                {
                    var url = new Uri(Url.Link("API/Configs", new { id = config.Id }));
                    return Request.CreateRedirectResponse(HttpStatusCode.Created, url, new PutViewModel(config) { Success = true });
                }
                ModelState.AddModelError("", "Error saving config.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);//TODO: Proper error response models
        }
    }
}