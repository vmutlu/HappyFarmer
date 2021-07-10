using HappyFarmer.UI.Logging.ErrorLogRecord;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HappyFarmer.UI.Extensions
{
    public static class AuthorizeControlMiddlewareExtension
    {
        //Daha sonra kullanılacak
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    if (context.Session.GetString("ActiveCustomerId") is null)
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Lütfen uygulamaya giriş yapınız."
                        }.ToString());
                });
            });
        }
    }
}
