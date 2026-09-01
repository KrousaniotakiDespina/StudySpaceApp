using Microsoft.AspNetCore.Identity;
using StudySpaceApp.Models;
using StudySpaceApp.DAO;
using StudySpaceApp.Helpers;
using StudySpaceApp.Service;

namespace StudySpaceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSession();

            builder.Services.AddScoped<DBHelper>();

            builder.Services.AddScoped<IUserDAO, UserDAOImpl>();
            builder.Services.AddScoped<ITodoTaskDAO, TodoTaskDAOImpl>();
            builder.Services.AddScoped<INoteDAO, NoteDAOImpl>();

            builder.Services.AddScoped<IUserService, UserServiceImpl>();
            builder.Services.AddScoped<ITodoTaskService, TodoTaskServiceImpl>();
            builder.Services.AddScoped<INoteService, NoteServiceImpl>();

            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
