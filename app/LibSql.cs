// MIT LICENCE - Copyright (c) 2022
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections;
using System.Reflection;

namespace LibSql;

public class JsonStringEnumCamelCaseConverter : JsonStringEnumConverter
{
    public JsonStringEnumCamelCaseConverter() : base(JsonNamingPolicy.CamelCase)
    {
    }
}
[JsonConverter(typeof(JsonStringEnumCamelCaseConverter))]
public enum LibSqlOp { Execute, Close }

public class LibSqlRequest
{
    public LibSqlOp type { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LibSqlStatement? stmt { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<LibSqlArg>? args { get; set; }

    public LibSqlRequest(LibSqlOp type, string? sql = null, List<LibSqlArg>? args = null)
    {
        this.type = type;


        if (type == LibSqlOp.Execute)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql");
            }
            this.stmt = new LibSqlStatement(sql, args);
        }
    }
}


public class LibSqlStatement
{
    public string sql { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<LibSqlArg>? args { get; set; }

    public LibSqlStatement(string sql, List<LibSqlArg>? args = null)
    {
        if (args != null)
        {
            this.args = args;
        }
        this.sql = sql;
    }
}

class LibSqlRequestWrapper
{
    public List<LibSqlRequest> requests { get; set; }

    public LibSqlRequestWrapper(List<LibSqlRequest> requests)
    {
        this.requests = requests;
    }
}


enum LibSqlStatus { Open, Closed, Error }

public class LibSqlConnection
{
    private readonly HttpClient _httpClient;
    private LibSqlStatus _status;
    public readonly string org;
    public readonly string db;
    private readonly string _url;
    private string? _baton;

    public LibSqlConnection(string org, string db, string token)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        _status = LibSqlStatus.Closed;
        this.org = org;
        this.db = db;
        this._url = $"https://{db}-{org}.turso.io/v2/pipeline";
    }

    public async Task<Rows<T>> Query<T>(List<LibSqlRequest> request) where T : new()
    {
        var json = JsonSerializer.Serialize(new LibSqlRequestWrapper(request));
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var httpResponse = await _httpClient.PostAsync(_url, content);
        var result = await httpResponse.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<LibSqlResponse>(result);

        if (response == null)
        {
            throw new Exception("No response");
        }
        if (response.baton != null)
        {
            this._baton = response.baton;
        }

        var resultData = response.results[0].response.result;
        if (resultData == null || resultData.rows == null)
        {
            throw new Exception("No rows in response");
        }
        return new Rows<T>(resultData.rows, resultData.cols);

    }
    public async Task<int> Execute(List<LibSqlRequest> request)
    {
        var json = JsonSerializer.Serialize(new LibSqlRequestWrapper(request));
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var httpResponse = await _httpClient.PostAsync(_url, content);
        var result = await httpResponse.Content.ReadAsStringAsync();

        var response = JsonSerializer.Deserialize<LibSqlResponse>(result);


        if (response == null)
        {
            throw new Exception("No response");
        }
        if (response.baton != null)
        {
            this._baton = response.baton;
        }

        var resultData = response.results[0].response.result;
        if (resultData == null)
        {
            throw new Exception("No result in response");
        }
        return resultData.affected_row_count;
    }
}

[Serializable]
public class LibSqlArg
{
    public string type { get; set; }
    public string value { get; set; }

    public LibSqlArg(object value)
    {
        this.type = GetTypeString(value);
        this.value = value?.ToString()!;
    }

    private string GetTypeString(object value)
    {
        if (value == null)
            return "null";

        var type = value.GetType();

        return type switch
        {
            Type _ when type == typeof(int) || type == typeof(long) => "integer",
            Type _ when type == typeof(float) || type == typeof(double) || type == typeof(decimal) => "float",
            Type _ when type == typeof(string) => "text",
            Type _ when type == typeof(byte[]) => "blob",
            Type _ when type == typeof(Boolean) => "text",
            _ => throw new ArgumentException($"Unsupported type: {type.Name}")
        };

    }
}

public class LibSqlResponse
{
    public string? baton { get; set; }
    public List<LibSqlResult> results { get; set; } = new List<LibSqlResult>();
}

public class LibSqlResult
{
    public string type { get; set; } = string.Empty;
    public LibSqlResultResponse response { get; set; } = new LibSqlResultResponse();
}

public class LibSqlResultResponse
{
    public string type { get; set; } = string.Empty;
    public LibSqlResultData? result { get; set; }
}

public class LibSqlResultData
{
    public int affected_row_count { get; set; }
    public LibSqlOp type { get; set; }
    public List<List<Row>> rows { get; set; } = new List<List<Row>>();
    public List<Column> cols { get; set; } = new List<Column>();
}

public class Column
{
    public string name { get; set; } = string.Empty;
    public string decltype { get; set; } = string.Empty;
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ColumnNameAttribute : Attribute
{
    public string Name { get; }
    public ColumnNameAttribute(string name)
    {
        Name = name;
    }
}

public class Row
{
    public string type { get; set; } = string.Empty;
    public JsonElement value { get; set; }
}

public class Rows<T> : IEnumerable<T> where T : new()
{
    private List<List<Row>> _rows;
    private List<Column> _columns;

    public Rows(List<List<Row>> rows, List<Column> columns)
    {
        this._rows = rows;
        this._columns = columns;
    }

    public IEnumerator<T> GetEnumerator()
    {
        // Cache the properties and their corresponding column names
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var row in _rows)
        {
            var obj = new T();

            for (int i = 0; i < row.Count; i++)
            {
                var cell = row[i];
                var columnName = _columns[i].name;

                // Find the property corresponding to this column
                var property = FindProperty(properties, columnName);

                if (property != null && property.CanWrite)
                {
                    object? value = ConvertCellValue(cell.type, cell.value, property.PropertyType);
                    property.SetValue(obj, value);
                }
            }
            yield return obj;
        }
    }
    private PropertyInfo? FindProperty(PropertyInfo[] properties, string columnName)
    {
        foreach (var prop in properties)
        {
            var attr = prop.GetCustomAttribute<ColumnNameAttribute>();
            if (attr != null)
            {
                if (string.Equals(attr.Name, columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return prop;
                }
            }
            else
            {
                if (string.Equals(prop.Name, columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return prop;
                }
            }
        }
        return null;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private object? ConvertCellValue(string cellType, JsonElement cellValue, Type targetType)
    {
        if (cellValue.ValueKind == JsonValueKind.Null || cellValue.ValueKind == JsonValueKind.Undefined)
            return null;

        try
        {
            switch (cellType.ToLowerInvariant())
            {
                case "integer":
                    return ParseInteger(cellValue, targetType);
                case "float":
                    return ParseFloat(cellValue, targetType);
                case "text":
                    return ParseText(cellValue, targetType);
                case "boolean":
                    return ParseBoolean(cellValue, targetType);
                case "blob":
                    // Handle blob data if needed
                    return null;
                case "null":
                    return null;
                default:
                    // Handle other types if any
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }
    private object? ParseInteger(JsonElement cellValue, Type targetType)
    {
        if (cellValue.ValueKind == JsonValueKind.String)
        {
            var strValue = cellValue.GetString();
            if (long.TryParse(strValue, out long longValue))
                return Convert.ChangeType(longValue, targetType);
        }
        else if (cellValue.ValueKind == JsonValueKind.Number)
        {
            if (cellValue.TryGetInt64(out long longValue))
                return Convert.ChangeType(longValue, targetType);
        }
        return null;
    }

    private object? ParseFloat(JsonElement cellValue, Type targetType)
    {
        if (cellValue.ValueKind == JsonValueKind.Number)
        {
            if (cellValue.TryGetDouble(out double doubleValue))
                return Convert.ChangeType(doubleValue, targetType);
        }
        else if (cellValue.ValueKind == JsonValueKind.String)
        {
            var strValue = cellValue.GetString();
            if (double.TryParse(strValue, out double doubleValue))
                return Convert.ChangeType(doubleValue, targetType);
        }
        return null;
    }

    private object? ParseText(JsonElement cellValue, Type targetType)
    {
        if (cellValue.ValueKind == JsonValueKind.String)
        {
            var strValue = cellValue.GetString();
            return Convert.ChangeType(strValue, targetType);
        }
        return null;
    }

    private object? ParseBoolean(JsonElement cellValue, Type targetType)
    {
        if (cellValue.ValueKind == JsonValueKind.String)
        {
            var strValue = cellValue.GetString();
            if (bool.TryParse(strValue, out bool boolValue))
                return boolValue;
        }
        else if (cellValue.ValueKind == JsonValueKind.True)
        {
            return true;
        }
        else if (cellValue.ValueKind == JsonValueKind.False)
        {
            return false;
        }
        return null;
    }
}