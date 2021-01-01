using HappyFarmer.API.Extensions.Response;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyFarmer.API.Extensions
{
    public static class FarmerControllerExtensions
    {
        public static async Task<FarmerMultipleObjectResponse<TEntity>> GetArrayResponseForGetAllAsync<TEntity>(this List<TEntity> dtoList, string messageContent)

        {
            FarmerMultipleObjectResponse<TEntity> response = new FarmerMultipleObjectResponse<TEntity>();
            response.StatusCode = StatusCodes.Status200OK;
            if (dtoList.Count() <= 0)
            {
                response.Message = messageContent;
                response.StatusCode = StatusCodes.Status204NoContent;
                response.Success = true;
            }

            response.Result = dtoList;

            return response;
        }

        public static async Task<FarmerSingleObjectResponse<TEntity>> GetSingleObjectResponseForGetByIdAsync<TEntity>(this TEntity dto, string messageContent)
        {
            FarmerSingleObjectResponse<TEntity> response = new FarmerSingleObjectResponse<TEntity> { StatusCode = StatusCodes.Status200OK, Success = true };

            if (dto == null)
            {
                response.Message = $"İstenilen {messageContent} bulunamadı. Veritabanında bulunmayan veya silinmiş bir veriye erişmeye çalıştınız!";
                response.StatusCode = StatusCodes.Status204NoContent;
            }

            response.Result = dto;
            return response;
        }
    }
}
