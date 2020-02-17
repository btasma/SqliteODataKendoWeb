using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqliteODataKendoWeb.DSRTranslator
{
    public static class OperatorHelpers
    {
        public static string LogicalOperatorToString(FilterCompositionLogicalOperator op)
        {
            return op == FilterCompositionLogicalOperator.And ? "AND" : "OR";
        }
    }
}
