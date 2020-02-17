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
using SqliteODataKendoWeb.DSRTranslator;
using System.Collections;

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

        // HTTP https://localhost:44393/Blogs/DSR?page=1&pageSize=5
        [HttpGet("DSR")]
        public JsonResult GetDSR([DataSourceRequest]DataSourceRequest request)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                var query = request.ToSqlQuery("Blogs");
                var data = conn.Query(query);

                var countQuery = request.ToSqlCountQuery("Blogs");
                var count = conn.ExecuteScalar<int>(countQuery);

                DataSourceResult dsr = new DataSourceResult();
                dsr.Data = data;
                dsr.Total = count;
                JsonResult result = Json(dsr);
                return result;
            }
        }

        // HTTP https://localhost:44393/Blogs/EFDSR?page=1&pageSize=5
        [HttpGet("EFDSR")]
        public JsonResult GetEFDSR([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<dynamic> queryable = _context.Blogs.AsQueryable();
            DataSourceResult dsr = queryable.ToDataSourceResult(request);
            JsonResult result = Json(dsr);
            return result;
        }

        // HTTP https://localhost:44393/Blogs/KendoExampleDSR?page=1&pageSize=5
        [HttpGet("KendoExampleDSR")]
        public JsonResult GetKendoExampleDSR([DataSourceRequest]DataSourceRequest request)
        {
            var result = Json(this.products.ToDataSourceResult(request));
            return result;
        }

        // HTTP https://localhost:44393/Blogs/Add
        [HttpGet("Add")]
        public void GetAddBlog()
        {
            _context.Add(new Blog { Url = $"http://{Guid.NewGuid()}.com" });
            _context.SaveChanges();
        }

        private IEnumerable products = new[] {
           new { ProductName = "Chai", CategoryName = "Beverages", QuantityPerUnit = "10 boxes x 20 bags" },
           new { ProductName = "Chang", CategoryName = "Beverages", QuantityPerUnit = "20 boxes x 20 bags" },
           new { ProductName = "Aniseed Syrup", CategoryName = "Condiments", QuantityPerUnit = "12 - 550 ml bottles" },
           new { ProductName = "Chef Anton's Cajun Seasoning", CategoryName = "Condiments", QuantityPerUnit = "48 - 6 oz jars" },
           new { ProductName = "Chef Anton's Gumbo Mix", CategoryName = "Condiments", QuantityPerUnit = "36 boxes" },
           new { ProductName = "Grandma's Boysenberry Spread", CategoryName = "Condiments", QuantityPerUnit = "12 - 8 oz jars" },
           new { ProductName = "Uncle Bob's Organic Dried Pears", CategoryName = "Produce", QuantityPerUnit = "12 - 1 lb pkgs." },
           new { ProductName = "Northwoods Cranberry Sauce", CategoryName = "Condiments", QuantityPerUnit = "12 - 12 oz jars" },
           new { ProductName = "Mishi Kobe Niku", CategoryName = "Meat/Poultry", QuantityPerUnit = "18 - 500 g pkgs." },
           new { ProductName = "Ikura", CategoryName = "Seafood", QuantityPerUnit = "12 - 200 ml jars" }
       };
    }
}
