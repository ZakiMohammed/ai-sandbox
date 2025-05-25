using AISandbox.Services;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();

// For Speech to Text
// await SpeechToText.Run(config);

// For Text to Speech
//await TextToSpeech.Run(config);

// For Speech to Speech Translation
//await SpeechToSpeech.Run(config);

// For Text to OpenAI
//await TextToOpenAI.Run(config);

// For OpenAI to SQL
await OpenAIToSql.Run(config);