using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.API.Extensions.Response
{
    public class FarmerMultipleObjectResponse<TEntity> : FarmerBaseResponse
    {
        [JsonProperty("result")]
        public IEnumerable<TEntity> Result { get; set; }
    }
}
