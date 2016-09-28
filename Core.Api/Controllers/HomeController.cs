
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcEfSample.Models;
using CoreProfiler;
using CoreProfiler.Web;
using System.Threading;

namespace MvcEfSample.Controllers
{
    public class HomeController : Controller
    {
        private WebsiteDbContext dbContext;
        public HomeController(WebsiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<JsonResult> Index()
        {
            using (ProfilingSession.Current.Step("Handle Request - /"))
            {                
                using (ProfilingSession.Current.Step(() => "Load Data"))
                {
                    var articles = await dbContext.Articles.OrderByDescending(a => a.Id)
                        .Take(10)
                        .ToArrayAsync();

                    return new JsonResult(articles);
                }
            }
        }
    }
}