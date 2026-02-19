using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables; // REQUIRED
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using dotenv.net;
using SemanticKernelPlugin;

await RunAgentAsync();

async Task RunAgentAsync()
{
    Console.WriteLine("Welcome to the Semantic Kernel AI Agent!");

    DotEnv.Load();

    var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>()
        .Build();


    string? githubToken = configuration["GITHUB_TOKEN"];

    if (string.IsNullOrEmpty(githubToken))
    {
        Console.WriteLine("❌ Error: GITHUB_TOKEN not found.");
        return;
    }

    Console.WriteLine("✅ GITHUB_TOKEN loaded successfully!");

    // Build kernel
    var kernelBuilder = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(
            "openai/gpt-4o",  // modelId
            githubToken       // apiKey
        );

    kernelBuilder.Plugins.AddFromType<MathPlugin>();
    kernelBuilder.Plugins.AddFromType<StringPlugin>();
    kernelBuilder.Plugins.AddFromType<TimePlugin>();
    kernelBuilder.Plugins.AddFromType<WeatherPlugin>();

    var kernel = kernelBuilder.Build();

    Console.WriteLine("🤖 Semantic Kernel instance built successfully.");

    // … rest of the code remains unchanged …
}
