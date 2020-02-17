using Kendo.Mvc;
using System;

namespace SqliteODataKendoWeb.DSRTranslator
{
    internal class FilterOperatorParser
    {
        private FilterDescriptor fd;

        public FilterOperatorParser(FilterDescriptor fd)
        {
            this.fd = fd;
        }

        public string ToSql()
        {
            switch (fd.Operator)
            {
                case FilterOperator.IsLessThan:
                    return IsLessThan();
                case FilterOperator.IsLessThanOrEqualTo:
                    return IsLessThanOrEqualTo();
                case FilterOperator.IsEqualTo:
                    return IsEqualTo();
                case FilterOperator.IsNotEqualTo:
                    return IsNotEqualTo();
                case FilterOperator.IsGreaterThanOrEqualTo:
                    return IsGreaterThanOrEqualTo();
                case FilterOperator.IsGreaterThan:
                    return IsGreaterThan();
                case FilterOperator.StartsWith:
                    return StartsWith();
                case FilterOperator.EndsWith:
                    return EndsWith();
                case FilterOperator.Contains:
                    return Contains();
                case FilterOperator.IsContainedIn:
                    return IsContainedIn();
                case FilterOperator.DoesNotContain:
                    return DoesNotContain();
                case FilterOperator.IsNull:
                    return IsNull();
                case FilterOperator.IsNotNull:
                    return IsNotNull();
                case FilterOperator.IsEmpty:
                    return IsEmpty();
                case FilterOperator.IsNotEmpty:
                    return IsNotEmpty();
                case FilterOperator.IsNullOrEmpty:
                    return IsNullOrEmpty();
                case FilterOperator.IsNotNullOrEmpty:
                    return IsNotNullOrEmpty();
                default:
                    throw new NotImplementedException();
            }
        }

        private string StartsWith()
        {
            return $"LOWER(\"{fd.Member}\") LIKE LOWER(\"{fd.Value}%\")";
        }

        private string EndsWith()
        {
            return $"LOWER(\"{fd.Member}\") LIKE LOWER(\"%{fd.Value}\")";
        }

        private string Contains()
        {
            return $"LOWER(\"{fd.Member}\") LIKE LOWER(\"%{fd.Value}%\")";
        }

        private string IsContainedIn()
        {
            return $"LOWER(\"{fd.Member}\") LIKE LOWER(\"%{fd.Value}%\")";
        }

        private string IsEqualTo()
        {
            return $"LOWER(\"{fd.Member}\") = LOWER(\"{fd.Value}\")";
        }

        private string IsNotEqualTo()
        {
            return $"LOWER(\"{fd.Member}\") != LOWER(\"{fd.Value}\")";
        }

        private string IsLessThan()
        {
            return $"\"{fd.Member}\" < {Convert.ToInt32(fd.Value)}";
        }

        private string IsLessThanOrEqualTo()
        {
            return $"\"{fd.Member}\" <= {Convert.ToInt32(fd.Value)}";
        }        

        private string IsGreaterThan()
        {
            return $"\"{fd.Member}\" > {Convert.ToInt32(fd.Value)}";
        }

        private string IsGreaterThanOrEqualTo()
        {
            return $"\"{fd.Member}\" >= {Convert.ToInt32(fd.Value)}";
        }

        private string DoesNotContain()
        {
            return $"LOWER(\"{fd.Member}\") NOT LIKE LOWER(\"%{fd.Value}%\")";
        }

        private string IsNull()
        {
            return $"\"{fd.Member}\" IS NULL";
        }

        private string IsNotNull()
        {
            return $"\"{fd.Member}\" IS NOT NULL";
        }

        private string IsEmpty()
        {
            return $"\"{fd.Member}\" = \"\"";
        }

        private string IsNotEmpty()
        {
            return $"\"{fd.Member}\" != \"\"";
        }

        private string IsNullOrEmpty()
        {
            return $"(\"{fd.Member}\" IS NULL OR \"{fd.Member}\" = \"\")";
        }

        private string IsNotNullOrEmpty()
        {
            return $"(\"{fd.Member}\" IS NOT NULL AND \"{fd.Member}\" != \"\")";
        }
    }
}