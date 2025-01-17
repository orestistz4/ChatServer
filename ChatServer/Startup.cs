﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatServer.Hubs;
using ChatServer.Models;
using ChatServer.Models.Rooms;
using ChatServer.Models.SaveDatsFromHub;
using ChatServer.Models.UserRoom;
using ChatServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatServer
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940



		public IConfiguration _config { get; }
		public Startup(IConfiguration config)
		{
			_config = config;
		}


		public void ConfigureServices(IServiceCollection services)
		{
			//-------gia to identity
			services.AddIdentity<IdentityUser, IdentityRole>((options) =>
			{
				options.Password.RequiredLength = 8;
				options.Password.RequiredUniqueChars = 3;
				options.SignIn.RequireConfirmedEmail = true;
			})
			   .AddEntityFrameworkStores<AppDbContext>()
			   .AddDefaultTokenProviders();

			//------



			services.AddDbContextPool<AppDbContext>(options=>options.UseSqlServer(_config.GetConnectionString("CryptoDBConnection")));

			services.AddMvc();
			services.AddTransient<IRoom,RoomHandler>();
			services.AddTransient<IUserRooms,SQLUserRooms>();
			services.AddTransient<IGroupsMessagesSave,SQLSaveMessage>();
			
			services.AddSignalR();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{

			//set the globa service
			GlobalService.ServiceProvider = app.ApplicationServices;
			GlobalService.Configuration = _config;
			GlobalService.UpdateDatabase();
			//
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
		
			app.UseSignalR(config =>
			{
				config.MapHub<MessageHub>("/messages");
			});
			app.UseMvc();
		}
	}
}
