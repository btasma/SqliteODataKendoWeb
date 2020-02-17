using System;
using System.Text;

namespace SqliteODataKendoWeb.DSRTranslator
{
    internal class LimitClause
    {
        private int pageSize;

        public LimitClause(int pageSize)
        {
            this.pageSize = pageSize;
        }

        public string ToSql()
        {
            if (pageSize > 0)
            {
                return $"LIMIT {pageSize}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}