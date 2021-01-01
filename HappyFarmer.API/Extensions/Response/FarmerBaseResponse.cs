using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HappyFarmer.API.Extensions.Response
{
    public abstract class FarmerBaseResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = true;

        [JsonProperty("message")]
        public string Message { get; set; } = "İşlem Başarılı!!";

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
    }
}
