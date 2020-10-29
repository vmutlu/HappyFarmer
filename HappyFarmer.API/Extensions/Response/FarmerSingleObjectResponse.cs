using Newtonsoft.Json;

namespace HappyFarmer.API.Extensions.Response
{
    public class FarmerSingleObjectResponse<TEntity> : FarmerBaseResponse
    {
        [JsonProperty("result")]
        public TEntity Result { get; set; }
    }
}
