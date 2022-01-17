using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore_App.BookStoreRepository;
using BookStore_App.Manager;
using BookStoreManager.Manager;
using BookStoreRepository;
using BookStoreRepository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BookStore_App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookManager, BookManager>();
            services.AddTransient<IWishlistRepository, WishlistRepository>();
            services.AddTransient<IWishlistManager, WishlistManager>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartManager, CartManager>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IAddressManager, AddressManager>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "BookStore", Description = "Books", Version = "1.0" });


            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //based on this routing it redirect our request to the corresponding controller and action
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");//pattern of routing in mvc with default values
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "BookStore(V 1.0)");
            });
        }


    }
}
