using System;
using System.Text.Json;

namespace Dapper.DAL.Extensions
{
    public static class JsonConverterExtensions
    {
        public static string ConvertToJsonString<T>(this T data) => $"{JsonSerializer.Serialize(data)}" ;
    }
}
