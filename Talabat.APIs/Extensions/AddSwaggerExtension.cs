namespace Talabat.APIs.Extensions
{
    public static class AddSwaggerExtension
    {
        public static WebApplication UseSwaggerMiddlewares(this WebApplication app) //// el caller hya hya el parameter ely hytb3tlk
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
