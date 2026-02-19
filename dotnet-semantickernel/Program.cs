using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
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

    // Load .env
    DotEnv.Load();

    // Retrieve GitHub token
    var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>()
        .Build();

    string? githubToken = configuration["GITHUB_TOKEN"];

    if (string.IsNullOrEmpty(githubToken))
    {
        Console.WriteLine("❌ GitHub token not found!");
        return;
    }

    Console.WriteLine("✅ GITHUB_TOKEN loaded successfully!");

    // Build Semantic Kernel
    var kernelBuilder = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion("openai/gpt-4o", githubToken);

    // Register plugins
    kernelBuilder.Plugins.AddFromType<MathPlugin>();
    kernelBuilder.Plugins.AddFromType<StringPlugin>();
    kernelBuilder.Plugins.AddFromType<TimePlugin>();
    kernelBuilder.Plugins.AddFromType<WeatherPlugin>();

    var kernel = kernelBuilder.Build();
    Console.WriteLine("🤖 Semantic Kernel instance built successfully.");

    // =======================
    // Query loop starts here
    // =======================
    string[] testQueries =
    {
        "What time is it right now?",
        "What is 25 * 4 + 10?",
        "Reverse the string 'Hello World'",
        "What is the weather like today?"
    };

    var chatCompletion = kernel.GetRequiredService<IChatCompletionService>();

    Console.WriteLine("\n🤖 Running example queries:\n");

    foreach (var query in testQueries)
    {
        try
        {
            Console.WriteLine("────────────────────────────────────────");
            Console.WriteLine($"📝 Query: {query}");

            var chatHistory = new ChatHistory();
            chatHistory.AddSystemMessage("Please respond professionally and succinctly.");
            chatHistory.AddUserMessage(query);

            var executionSettings = new OpenAIPromptExecutionSettings
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            var response = await chatCompletion.GetChatMessageContentAsync(chatHistory, executionSettings, kernel);

            Console.WriteLine($"\n✅ Result: {response}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error processing query: " + query);
            Console.WriteLine("💡 Exception: " + ex.Message);
        }
    }

    Console.WriteLine("🎉 Agent demo complete!");
}
