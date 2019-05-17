using Core.Common.CoreFrame;
using Core.Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using NLog.Web;
using Ocelot.JwtAuthorize;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lobster.Service.Demo
{
    /// <summary>
    /// 启动入口 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Api版本提者信息
        /// </summary>
        private IApiVersionDescriptionProvider provider;
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigHelper.t = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //注入APIHelper
            services.AddTransient<IApiHelper, ApiHelper>();
            services.AddSession();
            //注入JWT 登录
            services.AddTokenJwtAuthorize();
            //注入JWT 验证
            services.AddApiJwtAuthorize((context) =>
            {
                return ValidatePermission(context);
            });
            //API版本管理
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;//当设置为 true 时, API 将返回响应标头中支持的版本信息
                options.AssumeDefaultVersionWhenUnspecified = true; //此选项将用于不提供版本的请求。默认情况下, 假定的 API 版本为1.0
                options.DefaultApiVersion = new ApiVersion(1, 0);
            }).AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
            this.provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            //Swagger API管理
            services.AddSwaggerGen(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName,
                        new Info()
                        {
                            Title = "Lobster Demo服务 v" + description.ApiVersion,
                            Version = description.ApiVersion.ToString(),
                            Contact = new Contact { Email = "343588387@qq.com", Name = "Lobster.Service.Demo", Url = "http://0.0.0.0" },
                            Description = "切换版本请点右上角版本切换"
                        }
                    );
                }
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Lobster.Service.Demo.xml");
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "请输入带有Bearer的Token", Name = "Authorization", Type = "apiKey" });
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    {
                        "Bearer",
                        Enumerable.Empty<string>()
                    }
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.IgnoreObsoleteActions();
                //排除第三方controller
                //options.DocInclusionPredicate((docName, apiDesc) =>
                //{
                //    var assemblyName = ((ControllerActionDescriptor)apiDesc.ActionDescriptor).ControllerTypeInfo.Assembly.GetName().Name;
                //    var currentAssemblyName = GetType().Assembly.GetName().Name;
                //    return currentAssemblyName == assemblyName;
                //});
                options.DocumentFilter<HiddenApiFilter>();
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //将Redis分布式缓存服务添加到服务中
            //services.AddDistributedRedisCache(options =>
            //{
            //    //用于连接Redis的配置  
            //    options.Configuration = Configuration.GetConnectionString("RedisConnection");//读取配置信息的串
            //    //Redis实例名RedisDistributedCache
            //    options.InstanceName = "SSORedisCache";
            //});
            //支持跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问          
                    //.WithOrigins("http://localhost:8080") ////允许http://localhost:8080的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();//指定处理cookie
                });
            });
            services.AddSession();
            //解决大文件上传问题
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });
            services.AddMvc(options =>
            {
                options.Filters.Add<ActionCoreFrame>();
                options.Filters.Add<ExceptionCoreFrame>();
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //使用NLog作为日志记录工具
            loggerFactory.AddNLog();
            //引入Nlog配置文件
            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMvc();
            app.UseCors("AllowSameDomain");
            app.UseSession();
            app.UseMvcWithDefaultRoute();
            app.UseSwagger().UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        /// <summary>
        /// JWT验证身份
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        bool ValidatePermission(HttpContext httpContext)
        {
            return true;
        }
    }
}
