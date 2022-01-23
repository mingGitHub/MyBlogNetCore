using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.WebApi.Utility._Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("NoAuthorize")]
        public string NoAuthorize()
        {
            return "this is NoAuthorize";
        }

        [HttpGet("Authorize")]
        [Authorize]
        public string Authorize()
        {
            return "this is Authorize";
        }

        [TypeFilter(typeof(CustomResourceFilterAttrubute))]
        [HttpGet("GetCache")]
        public IActionResult GetCache(string name)
        {
            return new JsonResult( new { 
                  name=name,
                  age=18,
                  sex=true
            });
        }
    }
}
