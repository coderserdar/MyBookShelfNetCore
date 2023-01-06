﻿using MyBookShelf.Business.Contexts;

namespace MyBookShelf.Web;

public class Startup {
    public IConfiguration configRoot {
        get;
    }
    
    public Startup(IConfiguration configuration) {
        configRoot = configuration;
        using var client = new DatabaseContext();
        client.Database.EnsureCreated();
    }
    
    public void ConfigureServices(IServiceCollection services) {
        services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
        services.AddSession(options => {  
            options.IdleTimeout = TimeSpan.FromMinutes(10); 
        });  
        services.AddRazorPages();
        services.AddMvc();
    }
    
    public void Configure(WebApplication app, IWebHostEnvironment env) {
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.UseSession();
        app.Run();
    }
}