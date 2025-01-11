using Ecom.Services.Product.Api.Model;

namespace Ecom.Services.Product.Api
{
    public static class ExceptionHandler
    {
        public static void Handle(Exception ex, ResponseModel reponse)
        {
            reponse.Message = ex.Message;
            reponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            //Handle Logging
            Console.WriteLine(ex.ToString());
        }
    }
}
