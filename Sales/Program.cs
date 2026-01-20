using Microsoft.Extensions.Hosting;

Console.Title = "Sales";

var builder = Host.CreateApplicationBuilder(args);

#region Create .learningtransport directory in solution root
var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory())?
    .Parent?
    .Parent?
    .Parent?
    .FullName ?? Directory.GetCurrentDirectory();
var storageDirectory = Path.Combine(projectRoot, ".learningtransport");
Directory.CreateDirectory(storageDirectory);
#endregion

var endpointConfiguration = new EndpointConfiguration("Sales");

endpointConfiguration.UseSerialization<SystemJsonSerializer>();

endpointConfiguration.UseTransport(new LearningTransport());

builder.UseNServiceBus(endpointConfiguration);

var app = builder.Build();

await app.RunAsync();