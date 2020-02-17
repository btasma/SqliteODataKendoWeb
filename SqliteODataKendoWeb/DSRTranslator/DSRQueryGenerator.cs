using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteODataKendoWeb.DSRTranslator
{
    public class DSRQueryGenerator
    {
        private readonly DataSourceRequest request;

        public DSRQueryGenerator(DataSourceRequest request)
        {
            this.request = request;
        }

        public string GetQuery(string tableName)
        {
            var query = string.Join(" ", new string[]
            {
                $"SELECT * FROM \"{tableName}\"",
                new WhereClause(request.Filters).ToSql(),
                new OrderClause(request.Sorts).ToSql(),
                new LimitClause(request.PageSize).ToSql(),
                new OffsetClause(request.Page, request.PageSize).ToSql()
            });

            query += ";";
            return query;
        }

        public string GetCountQuery(string tableName)
        {
            var query = string.Join(" ", new string[]
            {
                $"SELECT COUNT(*) FROM \"{tableName}\"",
                new WhereClause(request.Filters).ToSql()
            });

            query += ";";
            return query;
        }
    }
}
