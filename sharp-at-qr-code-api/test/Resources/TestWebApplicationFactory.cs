using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace PChouse.AT.QRCode.API.Test;

public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : Program
{

    public event Action<object, IServiceCollection>? OnStartConfigureServices = null;


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {


        builder.ConfigureServices(services =>
        {
            OnStartConfigureServices?.Invoke(this, services);
        });

        base.ConfigureWebHost(builder);
        builder.CaptureStartupErrors(true);
    }
}