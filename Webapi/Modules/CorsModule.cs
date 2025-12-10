namespace Webapi.Modules
{
    public static class CorsModule
    {
        private const string AllowsAny = "_allowsAny";

        public static IServiceCollection AddDesafioCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowsAny, builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseDesafioCors(this IApplicationBuilder app)
        {
            app.UseCors(AllowsAny);

            return app;
        }
    }
}
