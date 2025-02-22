using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.API_ApplicazioneCalendario>("api");





builder.Build().Run();
