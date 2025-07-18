namespace infertility_system
{
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using infertility_system.Dtos.Admin;
    using infertility_system.Dtos.User;
    using infertility_system.Interfaces;
    using infertility_system.Repository;
    using infertility_system.Service;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Security.Claims;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<Dtos.Email.EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig); // Register the email configuration as a singleton

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<Data.AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Configure the DbContext with SQL Server connection string
            builder.Services.AddScoped<Interfaces.ICustomerRepository, Repository.CustomerRepository>(); // Register the customer repository
            builder.Services.AddScoped<Interfaces.IDoctorRepository, Repository.DoctorRepository>();
            builder.Services.AddScoped<Interfaces.IServiceRepository, Repository.ServiceRepository>();
            builder.Services.AddScoped<Interfaces.IOrderRepository, Repository.OrderRepository>();
            builder.Services.AddScoped<Interfaces.IBookingRepository, Repository.BookingRepository>();
            builder.Services.AddScoped<Interfaces.IDoctorScheduleRepository, Repository.DoctorScheduleRepository>();
            builder.Services.AddScoped<Interfaces.IFeedbackRepository, Repository.FeedbackRepository>(); // Register the feedback repository
            builder.Services.AddScoped<Interfaces.IConsulationResultRepository, Repository.ConsulationResultRepository>(); // Register the authentication service
            builder.Services.AddScoped<Interfaces.IManagerRepository, Repository.ManagerRepository>(); // Register the authentication service
            builder.Services.AddScoped<Interfaces.ITreatementRoadmapRepository, Repository.TreatmentRoadmapRepository>(); // Register the treatment roadmap repository

            builder.Services.AddScoped<Interfaces.IMedicalRecordRepository, Repository.MedicalRecordRepository>(); // Create, Update medicalRecord
            builder.Services.AddScoped<Interfaces.IMedicalRecordDetailRepository, Repository.MedicalRecordDetailRepository>();

            builder.Services.AddScoped<Interfaces.IOrderDetailRepository, Repository.OrderDetailRepository>(); // Register the order detail repository
            builder.Services.AddScoped<Interfaces.IEmbryoRepository, Repository.EmbryoRepository>(); // Register the embryo repository
            builder.Services.AddScoped<Interfaces.IUserRepository, Repository.UserRepository>(); // Register the user repository
            builder.Services.AddScoped<Interfaces.IMedicalRecordDetailRepository, Repository.MedicalRecordDetailRepository>(); // Register the medical record detail repository

            builder.Services.AddScoped<ICustomerRepository, Repository.CustomerRepository>(); // Register the customer repository
            builder.Services.AddScoped<IDoctorRepository, Repository.DoctorRepository>();
            builder.Services.AddScoped<IServiceRepository, Repository.ServiceRepository>();
            builder.Services.AddScoped<IPaymentRepository, Repository.PaymentRepository>();
            builder.Services.AddScoped<IBlogPostRepository, Repository.BlogPostRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Google Authentication Services
            builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IVnpayService, VnpayService>();

            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<RegitsterDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ChangePasswordDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RegisterAdminDtoValidator>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])),
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Nhập JWT token theo format: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new List<string>()
                    },
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Port của React Vite
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "Admin")); // Thêm chính sách yêu cầu vai trò Admin

                options.AddPolicy("Customer", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "Customer"));
            });

            // new repository
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

<<<<<<< Updated upstream
=======
            app.UseStaticFiles(); // Enable serving static files (e.g., images)

            app.UseMiddleware<ExceptionMiddleware>();

>>>>>>> Stashed changes
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();  // 🔹 Đầu tiên, xác thực người dùng

            app.UseAuthorization();   // 🔹 Sau đó, kiểm tra quyền hạn

            app.MapControllers();

            app.Run();
        }
    }
}
