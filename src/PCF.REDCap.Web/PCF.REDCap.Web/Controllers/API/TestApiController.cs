using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PCF.REDCap.Web.Controllers.API
{
    public class PatchInputModel
    {
        public string Value { get; set; }
    }

    public class TestApiController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPatch]
        public void Patch(int id, [FromBody]PatchInputModel model)
        {
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}