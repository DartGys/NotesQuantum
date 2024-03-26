using Notes.WebUI.Data;
using Notes.WebUI.Interfaces;

namespace Notes.WebUI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddHttpClient("NoteApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44331/");
            });

            services.AddScoped<INoteService, NoteService>();

            return services;
        }
    }
}
