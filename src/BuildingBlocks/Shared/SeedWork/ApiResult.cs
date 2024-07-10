using System.Text.Json.Serialization;

namespace Shared.SeedWork
{
    public class ApiResult<T>
    {
        [JsonConstructor]
        public ApiResult(bool isSucceeded, string? message = null)
        {
            Message = message ?? string.Empty;
            IsSucceeded = isSucceeded;
        }

        public ApiResult(bool isSucceeded, T data, string? message = null)
        {
            Data = data;
            Message = message ?? string.Empty;
            IsSucceeded = isSucceeded;
        }

        public bool? IsSucceeded { get; set; } = false;
        public string? Message { get; set; } = string.Empty;
        public T? Data { get; } = default;
    }
}
