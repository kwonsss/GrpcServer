using GrpcServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace GrpcServer
{
    public class Server
    {
        public void Start()
        {
            var builder = WebApplication.CreateBuilder();

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
