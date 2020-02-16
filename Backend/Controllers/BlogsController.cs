using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace SqliteODataKendoWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : Controller
    {
        private readonly BloggingContext _context;

        public BlogsController(BloggingContext context)
        {
            _context = context;
        }

        // HTTP https://localhost:44393/Blogs
        // ODATA https://localhost:44393/odata/Blogs?$filter=contains(Url,%20%27msdn%27)&$top=10
        [HttpGet]
        [EnableQuery()]
        public IQueryable<dynamic> Get()
        {
            return _context.Blogs.AsQueryable();
        }

        // HTTP https://localhost:44393/Blogs/2
        // ODATA https://localhost:44393/odata/Blogs(2)?$select=Url
        [HttpGet("{key}")]
        [ODataRoute("({key})")]
        [EnableQuery()]
        public Blog Get([FromODataUri]int key)
        {
            return _context.Blogs.Find(key);
        }

        // HTTP https://localhost:44393/Blogs/?page=1&pageSize=5
        [HttpGet("DSR")]
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<dynamic> queryable = _context.Blogs.AsQueryable();
            DataSourceResult dsr = queryable.ToDataSourceResult(request);
            JsonResult result = Json(dsr);
            return result;
        }

        // HTTP https://localhost:44393/Blogs/Add
        [HttpGet("Add")]
        public void GetAddBlog()
        {
            _context.Add(new Blog { Url = $"http://{Guid.NewGuid()}.com" });
            _context.SaveChanges();
        }
    }
}
