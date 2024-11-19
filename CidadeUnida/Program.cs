using CidadeUnida.Services;

namespace CidadeUnida
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddScoped<ISessao, Sessao>();

            // 1. Adicionar o  Serviço de Gerenciamento de Sessão.

            builder.Services.AddSession(options =>
            {

                /*
                  A propriedade IdleTimeout refere-se ao tempo de expiração da sessão por inatividade.
                  O tempo padrão para a inatividade da aplicação ASP.NET Core é de 20 minutos.
                 */

                options.IdleTimeout = TimeSpan.FromMinutes(10); // 10 minutos para expirar a sessão.
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}