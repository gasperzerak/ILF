using System;
using System.Data;
using Varigence.Languages.Biml.Table;

public static class HelperMethods
{

    public static string getColumnSql(AstTableColumnBaseNode column)
    {
        string returnValue = null;
        returnValue = ",[" + column.Name + "] " + getBiml2DBType(convertDBType2Biml(column.DataType.ToString()));





        if (column.Length > 0)
        {
            if (column.DataType == DbType.String)
            {
                returnValue += " (" + column.Length + ")";
            }
            else if (column.DataType == DbType.AnsiString)
            {
                returnValue += " (" + column.Length + ")";
            }
            else if (column.DataType == DbType.StringFixedLength)
            {
                returnValue += " (" + column.Length + ")";
            }
            else if (column.DataType == DbType.AnsiStringFixedLength)
            {
                returnValue += " (" + column.Length + ")";
            }

        }
        else if (column.Length < 0)
        {
            if (column.DataType == DbType.String || column.DataType == DbType.AnsiString)
            {
                returnValue += " (max)";
            }
        }
        else if (column.DataType == DbType.Decimal)
        {
            returnValue += " (" + column.Precision + "," + column.Scale + ")";
        }

        if (column.IsNullable)
        {
            returnValue += " null";
        }
        else
        {
            returnValue += " not null";
        }




        return returnValue;
    }

    public static string getBiml2DBType(string t)
    {
        switch (t.ToLower())
        {
            case "ansistring": return "varchar";
            case "string": return "nvarchar";
            case "byte": return "tinyint";
            case "int16": return "smallint";
            case "int32": return "int";
            case "int64": return "bigint";
            case "double": return "float";
            case "single": return "real";
            case "stringfixedlength": return "nchar";
            case "date": return "date";
            case "datetime": return "datetime";
            case "time": return "time";
            case "datetime2": return "datetime2";
            case "ansistringfixedlength": return "char";
            case "binary": return "varbinary(8)";
            case "decimal": return "decimal";
            case "boolean": return "bit";
            case "guid": return "uniqueidentifier";
            default: throw new ArgumentException(string.Format("The parameter value {0} cannot converted.", t));
        }
    }

    public static bool isBimlType(string bimlDbType)
    {
        switch (bimlDbType.ToLower())
        {
            case "int64":
            case "binary":
            case "boolean":
            case "ansistringfixedlength":
            case "date":
            case "datetime":
            case "datetime2":
            case "datetimeoffset":
            case "decimal":
            case "double":
            case "object":
            case "int32":
            case "currency":
            case "stringfixedlength":
            case "string":
            case "single":
            case "int16":
            case "time":
            case "byte":
            case "guid":
            case "ansistring":
            case "xml": return true;
            default: return false;
        }
    }

    public static string convertDBType2Biml(string dbType)
    {
        switch (dbType.ToLower())
        {
            case "bigint": return "Int64";
            case "binary": return "Binary";
            case "bit": return "Boolean";
            case "char": return "AnsiStringFixedLength";
            case "date": return "Date";
            case "datetime": return "DateTime";
            case "datetime2": return "DateTime2";
            case "datetimeoffset": return "DateTimeOffset";
            case "decimal": return "Decimal";
            case "float": return "Double";
            case "geography": return "Object";
            case "geometry": return "Object";
            case "hierarchyid": return "Object";
            case "int": return "Int32";
            case "money": return "Currency";
            case "nchar": return "StringFixedLength";
            case "numeric": return "Decimal";
            case "nvarchar": return "String";
            case "real": return "Single";
            case "rowversion": return "Binary";
            case "smalldatetime": return "DateTime";
            case "smallint": return "Int16";
            case "smallmoney": return "Currency";
            case "sql_variant": return "Object";
            case "time": return "Time";
            case "tinyint": return "Byte";
            case "uniqueidentifier": return "Guid";
            case "varbinary": return "Binary";
            case "varchar": return "AnsiString";
            case "xml": return "Xml";
            default:
                // deprecated data types
                if (dbType == "image") return "Binary";
                else if (dbType == "ntext") return "String";
                else if (dbType == "text") return "AnsiString";
                else if (dbType == "timestamp") return "Binary";
                // Biml data types
                else if (isBimlType(dbType)) return dbType;
                // unknown data types
                else throw new ArgumentException(string.Format("The parameter value {0} cannot converted.", dbType));
        }
    }
}