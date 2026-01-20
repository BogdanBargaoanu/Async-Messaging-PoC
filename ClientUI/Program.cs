using Microsoft.Extensions.Hosting;

Console.Title = "ClientUI";

var builder = Host.CreateApplicationBuilder(args);

var endpointConfiguration = new EndpointConfiguration("ClientUI");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

// Create .learningtransport directory in project root
var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName
                  ?? Directory.GetCurrentDirectory();
var storageDirectory = Path.Combine(projectRoot, ".learningtransport");
if (!Directory.Exists(storageDirectory))
{
    Directory.CreateDirectory(storageDirectory);
}
;

var transport = endpointConfiguration.UseTransport(new LearningTransport());

builder.UseNServiceBus(endpointConfiguration);

var app = builder.Build();

await app.RunAsync();