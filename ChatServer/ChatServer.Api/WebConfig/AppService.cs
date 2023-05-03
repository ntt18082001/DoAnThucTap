using AutoMapper;
using ChatServer.Api.Common.AddHeaderSwaggen;
using ChatServer.Api.Common.Mailer;
using ChatServer.Api.Services;
using ChatServer.Api.Services.Interfaces;
using ChatServer.Data;
using ChatServer.Data.Interfaces.Repositories;
using ChatServer.Data.Interfaces.UnitOfWork;
using ChatServer.Data.Repositories;
using ChatServer.Data.Services;
using ChatServer.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Configuration;
using System.Text;

namespace ChatServer.Api.WebConfig
{
	public static class AppService
	{
		public static void AddAppService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});
				c.OperationFilter<AddAuthHeaderOperationFilter>();
			});

			services.AddDbContext<AppChatDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("Database"));
			});

			services.AddCors(options =>
			{
				var frontendURL = configuration.GetValue<string>("Frontend_Url");
				options.AddDefaultPolicy(builder =>
				{
					builder.WithOrigins(frontendURL)
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials();
				});
			});

			var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = configuration["Jwt:Issuer"],
					ValidAudience = configuration["Jwt:Audience"],
				};
				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Query["access_token"];

						// If the request is for our hub...
						var path = context.HttpContext.Request.Path;
						if (!string.IsNullOrEmpty(accessToken) &&
							(path.StartsWithSegments("/realtime")))
						{
							// Read the token out of the query string
							context.Token = accessToken;
						}
						return Task.CompletedTask;
					}
				};
			});

			// Add UnitOfWork
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			// Add repositories
			services.AddTransient<ILoginRepository, LoginRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IFriendRequestRepository, FriendRequestRepository>();
			services.AddTransient<IMessageRepository, MessageRepository>();
			// Add services
			services.AddTransient<LoginService>();
			services.AddTransient<UserService>();
			services.AddTransient<FriendRequestService>();
			services.AddTransient<MessageService>();

			// Add file storage
			services.AddScoped<IFileStorageService, FileStorageService>();

			// Cấu hình AutoMapper
			var mapperConfig = new MapperConfiguration(config =>
			{
				config.AddProfile(new AutoMapperProfile());
			});
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);

			// Add token service
			services.AddScoped<ITokenService, TokenService>();

			services.Configure<FormOptions>(options =>
			{
				options.MemoryBufferThreshold = Int32.MaxValue;
			});
			services.AddSignalR();

			// Khởi tạo thông tin mail
			AppMailConfiguration mailConfig = new();
			mailConfig.LoadFromConfig(configuration);
			services.AddSingleton(mailConfig);
		}
	}
}
