using HappyFarmer.UI.Logging.ErrorLogRecord;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace HappyFarmer.UI.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILog logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"\n Uygulamada bir hata tespit edildi: \n {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Tarih: " + DateTime.Now.ToShortDateString() + " \n Saat: " + DateTime.Now.ToShortTimeString() + "\n Hata Mesajı : " + "Üzgünüm Bir Hata Yakaladım ve Senin için Logladım :)"
                        }.ToString());
                    }
                });
            });
        }
    }
}
