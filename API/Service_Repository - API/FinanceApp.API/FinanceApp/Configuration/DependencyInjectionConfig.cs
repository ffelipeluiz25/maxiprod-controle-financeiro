using FinanceApp.Repository;
using FinanceApp.Repository.Interface;
using FinanceApp.Services;
using FinanceApp.Services.Interface;

namespace FinanceApp.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependenciesRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IRelatorioRepository, RelatorioRepository>();
            return services;
        }

        public static IServiceCollection ResolveDependenciesServices(this IServiceCollection services)
        {
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IRelatorioService, RelatorioService>();
            return services;
        }
    }
}