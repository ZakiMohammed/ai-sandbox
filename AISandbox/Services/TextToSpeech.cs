using AISandbox.Utils;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;

namespace AISandbox.Services
{
    public static class TextToSpeech
    {
        public static async Task Run(IConfiguration config)
        {
            string? speechKey = config[AppSettings.SpeechKey];
            string? speechRegion = config[AppSettings.SpeechRegion];

            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            speechConfig.SpeechSynthesisVoiceName = AppSettings.SpeechSynthesisVoiceName;

            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();

            var speechSynthesis = new SpeechSynthesizer(speechConfig, audioConfig);

            Console.WriteLine("Text to Speech\n");
            Console.Write("Enter some text you want app to speak: ");
            var text = Console.ReadLine();

            var result = await speechSynthesis.SpeakTextAsync(text);

            switch (result.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    Console.WriteLine($"Speech synthesis for text [{text}]");
                    break;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                    Console.WriteLine($"Speech synthesis canceled: {cancellation.Reason}");
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"Error details: {cancellation.ErrorDetails}");
                    }
                    break;
                default:
                    Console.WriteLine("Not able to perform any oprtation");
                    break;
            }
        }
    }
}