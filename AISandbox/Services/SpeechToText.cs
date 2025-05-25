using AISandbox.Utils;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Configuration;

namespace AISandbox.Services
{
    public static class SpeechToText
    {
        public static async Task Run(IConfiguration config)
        {
            string? speechKey = config[AppSettings.SpeechKey];
            string? speechRegion = config[AppSettings.SpeechRegion];

            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            speechConfig.SpeechRecognitionLanguage = "en-US";

            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();

            var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            Console.WriteLine("Speech to Text\n");
            Console.WriteLine("Speak into your microphone.");

            var result = await speechRecognizer.RecognizeOnceAsync();

            switch (result.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    Console.WriteLine($"Recognized: {result.Text}");
                    break;
                case ResultReason.NoMatch:
                    Console.WriteLine("No speech could be recognized.");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"Speech Recognition canceled: {cancellation.Reason}");
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
