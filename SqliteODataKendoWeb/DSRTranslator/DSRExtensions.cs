using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqliteODataKendoWeb.DSRTranslator
{
    public static class DSRExtensions
    {
        public static string ToSqlQuery(this DataSourceRequest request, string tableName)
        {
            return new DSRQueryGenerator(request).GetQuery(tableName);
        }

        public static string ToSqlCountQuery(this DataSourceRequest request, string tableName)
        {
            return new DSRQueryGenerator(request).GetCountQuery(tableName);
        }
    }
}
