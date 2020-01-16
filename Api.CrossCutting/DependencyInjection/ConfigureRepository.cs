using Api.Data.Implementation;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenceRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseSqlServer("Server=localhost,11433;Database=Api;Uid=SA;Pwd=DockerSql2017!;")
                );
        }

    }
}
