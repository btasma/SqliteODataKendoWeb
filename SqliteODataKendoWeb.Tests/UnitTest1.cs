using Kendo.Mvc;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqliteODataKendoWeb.DSRTranslator;
using System;
using System.Collections.Generic;

namespace SqliteODataKendoWeb.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dsr = new DataSourceRequest();

            dsr.Aggregates = null;
            dsr.Filters = null;
            dsr.Groups = null;
            dsr.Page = 1;
            dsr.PageSize = 2;

            var sd = new SortDescriptor();
            sd.Member = "url";
            sd.SortDirection = ListSortDirection.Ascending;
            sd.SortCompare = null;
            dsr.Sorts = new List<SortDescriptor>();
            dsr.Sorts.Add(sd);

            var fd = new CompositeFilterDescriptor();
            fd.LogicalOperator = FilterCompositionLogicalOperator.And;
            fd.FilterDescriptors = new Kendo.Mvc.Infrastructure.Implementation.FilterDescriptorCollection();


            var fd2 = new CompositeFilterDescriptor();
            fd2.LogicalOperator = FilterCompositionLogicalOperator.And;
            fd2.FilterDescriptors = new Kendo.Mvc.Infrastructure.Implementation.FilterDescriptorCollection();

            var fd21 = new FilterDescriptor();
            fd21.Member = "blogId";
            fd21.Operator = FilterOperator.Contains;
            fd21.Value = "1";
            fd2.FilterDescriptors.Add(fd21);

            var fd22 = new FilterDescriptor();
            fd22.Member = "url";
            fd22.Operator = FilterOperator.Contains;
            fd22.Value = "2";
            fd2.FilterDescriptors.Add(fd22);

            fd.FilterDescriptors.Add(fd2);

            var fd3 = new FilterDescriptor();
            fd3.Member = "posts";
            fd3.Operator = FilterOperator.Contains;
            fd3.Value = "3";
            fd.FilterDescriptors.Add(fd3);

            dsr.Filters = new List<IFilterDescriptor>();
            dsr.Filters.Add(fd);

            var query = new DSRQueryGenerator(dsr).GetQuery("Blogs");
            Console.WriteLine(query);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var dsr = new DataSourceRequest();

            dsr.Aggregates = null;
            dsr.Filters = null;
            dsr.Groups = null;
            dsr.Page = 1;
            dsr.PageSize = 2;

            var sd = new SortDescriptor();
            sd.Member = "url";
            sd.SortDirection = ListSortDirection.Ascending;
            sd.SortCompare = null;
            dsr.Sorts = new List<SortDescriptor>();
            dsr.Sorts.Add(sd);

            var fd3 = new FilterDescriptor();
            fd3.Member = "posts";
            fd3.Operator = FilterOperator.Contains;
            fd3.Value = "3";

            dsr.Filters = new List<IFilterDescriptor>();
            dsr.Filters.Add(fd3);

            var query = new DSRQueryGenerator(dsr).GetQuery("Blogs");
            Console.WriteLine(query);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var dsr = new DataSourceRequest();

            dsr.Aggregates = null;
            dsr.Filters = null;
            dsr.Groups = null;
            dsr.Page = 1;
            dsr.PageSize = 2;

            var sd = new SortDescriptor();
            sd.Member = "url";
            sd.SortDirection = ListSortDirection.Ascending;
            sd.SortCompare = null;
            dsr.Sorts = new List<SortDescriptor>();
            dsr.Sorts.Add(sd);

            var fd = new CompositeFilterDescriptor();
            fd.LogicalOperator = FilterCompositionLogicalOperator.And;
            fd.FilterDescriptors = new Kendo.Mvc.Infrastructure.Implementation.FilterDescriptorCollection();


            var fd21 = new FilterDescriptor();
            fd21.Member = "blogId";
            fd21.Operator = FilterOperator.Contains;
            fd21.Value = "1";
            fd.FilterDescriptors.Add(fd21);

            var fd22 = new FilterDescriptor();
            fd22.Member = "url";
            fd22.Operator = FilterOperator.Contains;
            fd22.Value = "2";
            fd.FilterDescriptors.Add(fd22);

            dsr.Filters = new List<IFilterDescriptor>();
            dsr.Filters.Add(fd);

            var query = new DSRQueryGenerator(dsr).GetQuery("Blogs");
            Console.WriteLine(query);
        }
    }
}
