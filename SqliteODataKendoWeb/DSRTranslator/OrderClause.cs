using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqliteODataKendoWeb.DSRTranslator
{
    internal class OrderClause
    {
        private IList<SortDescriptor> sorts;

        public OrderClause(IList<SortDescriptor> sorts)
        {
            this.sorts = sorts;
        }

        public string ToSql()
        {
            if(sorts == null || !sorts.Any())
                return string.Empty;

            var query = "ORDER BY ";

            var orderList = new List<string>();
            foreach (var sort in sorts)
            {
                orderList.Add($"\"{sort.Member}\" {(sort.SortDirection == ListSortDirection.Ascending ? "ASC" : "DESC")}");
            }

            query += string.Join(", ", orderList);

            return query;
        }
    }
}