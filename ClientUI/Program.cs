using ClientUI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sales.Messages.Commands;

Console.Title = "ClientUI";

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

var endpointConfiguration = new EndpointConfiguration("ClientUI");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

var transport = endpointConfiguration.UseTransport(new LearningTransport());
transport.RouteToEndpoint(typeof(PlaceOrder), "Sales");

builder.UseNServiceBus(endpointConfiguration);

builder.Services.AddHostedService<InputLoopService>();

var app = builder.Build();

await app.RunAsync();