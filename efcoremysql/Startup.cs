using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace efcoremysql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            // InsertData();
            PrintData();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        private static void InsertData()
        {
            using (var context = new MyDbContext())
            {

                var album = new Album()
                {
                    Nome = "Acústico MTV - Legião Urbana",
                    Ano = 1999,
                    Email = "contato@legiaourbana.com.br"
                };

                context.Albuns.Add(album);

                context.Musicas.Add(new Musica
                {
                    Nome = "Índios",
                    Album = album
                });
                context.Musicas.Add(new Musica
                {
                    Nome = "Pais e Filhos",
                    Album = album
                });

                context.Musicas.Add(new Musica
                {
                    Nome = "Eu sei",
                    Album = album
                });

                context.SaveChanges();
            }
        }

        private static void PrintData()
        {
            using (var context = new MyDbContext())
            {
                var musicas = context.Musicas.Include(m => m.Album);
                foreach (var musica in musicas)
                {
                    var data = new StringBuilder();
                    data.AppendLine($"Musica: {musica.Nome}");
                    data.AppendLine($"Album: {musica.Album.Nome}");
                    Console.WriteLine(data.ToString());
                }
            }
        }

    }
}
