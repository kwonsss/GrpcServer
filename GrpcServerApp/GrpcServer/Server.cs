using GrpcServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Net;

namespace GrpcServer
{
    public class Server
    {
        public void Start()
        {
            // Logger
            string basedir = AppDomain.CurrentDomain.BaseDirectory;

            var serilog = new LoggerConfiguration()
                .WriteTo.Async(a => a.File(basedir + "/log/.log",
                    rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30))
                .CreateLogger();

            Log.Logger = serilog;


            var builder = WebApplication.CreateBuilder();

            // Logger
            builder.Services.AddSerilog();
            // Grpc 등록
            builder.Services.AddGrpc();

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Listen(IPAddress.Any, 50052, (listenOptions) =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            var app = builder.Build();

            app.MapGrpcService<GreeterService>();

            app.RunAsync();
        }
    }
}
