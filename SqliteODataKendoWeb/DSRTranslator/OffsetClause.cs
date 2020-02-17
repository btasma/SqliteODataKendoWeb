using System;
using System.Text;

namespace SqliteODataKendoWeb.DSRTranslator
{
    internal class OffsetClause
    {
        private int page;
        private int pageSize;

        public OffsetClause(int page, int pageSize)
        {
            this.page = page;
            this.pageSize = pageSize;
        }
        
        public string ToSql()
        {
            if ((page -1 ) > 0)
            {
                var offset = (page - 1) * pageSize;
                return $"OFFSET {offset}";
            }
            else
            {
                return $"OFFSET 0";
            }
        }
    }
}