using Api.Data.Repository;
using Api.Domain.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenceRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddDbContext<MyContext>(
                options => options.UseSqlServer("Server=localhost,11433;Database=Api;Uid=SA;Pwd=DockerSql2017!;")
                );
        }
    }
}
