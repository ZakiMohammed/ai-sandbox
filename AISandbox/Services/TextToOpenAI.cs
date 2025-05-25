using AISandbox.Utils;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace AISandbox.Services
{
    public static class TextToOpenAI
    {
        public static async Task Run(IConfiguration config)
        {
            string? openAIKey = config["OpenAISettings:ApiKey"];
            string? openAIApiUrl = config["OpenAISettings:ApiUrl"];
            string? openAIModel = config["OpenAISettings:ModelName"];

            var uri = new Uri(openAIApiUrl!);
            var apiKeyCredential = new AzureKeyCredential(openAIKey!);
            var openAIClient = new AzureOpenAIClient(uri, apiKeyCredential);
            
            var chatClient = openAIClient.GetChatClient(openAIModel!);

            var conversation = string.Empty;

            while (true)
            {
                Console.WriteLine("\nEnter your message (or type 'exit' to quit):");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                var response = await GetResponseAsync(chatClient, input, conversation);
                conversation += @$"
-----------
UserQuestion:{input},
BotResponse:{response}
--------------
";
                Console.WriteLine(response);
            }
        }

        private static async Task<string> GetResponseAsync(ChatClient client, string input, string history)
        {
            string prompt = @$"
{AppSettings.PromptGuide}
        
# Question
{input}

# ChatHistory
{history}

YourAnswer:
";

            var chatMessages = new List<ChatMessage>
            {
                ChatMessage.CreateSystemMessage("You are a helpful assistant speaks only in English."),
                ChatMessage.CreateUserMessage(prompt),
            };
            var compleition = await client.CompleteChatAsync(chatMessages);
            var completionContent = compleition.Value?.Content?.FirstOrDefault();
            var response = completionContent?.Text ?? string.Empty;
            return response;
        }
    }
}
