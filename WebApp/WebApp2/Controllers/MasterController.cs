using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Controllers
{
    public abstract class MasterController : ControllerBase
    {
        public long userId { get; set; }

        public MasterController(IHttpContextAccessor httpContext)
        {
            userId = (long)httpContext.HttpContext.Items["UserId"];
        }
    }
}
