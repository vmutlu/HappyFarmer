using Newtonsoft.Json;
using System.Collections.Generic;

namespace HappyFarmer.API.Extensions.Response
{
    public class FarmerMultipleObjectResponse<TEntity> : FarmerBaseResponse
    {
        [JsonProperty("result")]
        public IEnumerable<TEntity> Result { get; set; }
    }
}
