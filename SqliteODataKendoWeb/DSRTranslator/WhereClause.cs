using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SqliteODataKendoWeb.DSRTranslator
{
    internal class WhereClause
    {
        private IList<IFilterDescriptor> filters;

        public WhereClause(IList<IFilterDescriptor> filters)
        {
            this.filters = filters;
        }

        public string ToSql()
        {
            if (filters == null || !filters.Any())
                return string.Empty;

            var query = "WHERE ";
            query += string.Join(" ", ParseFilters(filters));
            return query;
        }

        private List<string> ParseFilters(IList<IFilterDescriptor> filtersList)
        {
            var querySegments = new List<string>();
            foreach (var filter in filtersList)
            {
                if (filter is CompositeFilterDescriptor)
                {
                    var fd = filter as CompositeFilterDescriptor;
                    var subSegments = ParseFilters(fd.FilterDescriptors);
                    if (subSegments.Count != 2)
                    {
                        throw new NotImplementedException();
                    }
                    querySegments.Add($"({subSegments[0]} {(OperatorHelpers.LogicalOperatorToString(fd.LogicalOperator))} {subSegments[1]})");
                }
                else if (filter is FilterDescriptor)
                {
                    var fd = filter as FilterDescriptor;
                    querySegments.Add(new FilterOperatorParser(fd).ToSql());
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            return querySegments;
        }


    }
}