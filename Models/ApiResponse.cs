namespace MediReserva.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public ApiResponse(T? data, bool success = true, string? message = null)
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }

}
