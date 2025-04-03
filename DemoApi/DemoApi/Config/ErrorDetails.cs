using System.Text.Json.Serialization;

namespace DemoApi.Config
{
    public class ErrorDetails
    {
        
        [JsonPropertyName("status")]  
        public required int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public required string Message { get; set; }

       
        [JsonPropertyName("detailed")]
        public string? Detailed { get; set; }  

        [JsonPropertyName("stackTrace")]
        public string? StackTrace { get; set; } 
       
        public ErrorDetails() { }
    }
}
