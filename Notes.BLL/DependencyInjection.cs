using Microsoft.Extensions.DependencyInjection;
using Notes.BLL.Common;
using Notes.BLL.Interfaces;
using Notes.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutomapperProfile).Assembly);

            services.AddScoped<INoteService, NoteService>();

            return services;
        }
    }
}
