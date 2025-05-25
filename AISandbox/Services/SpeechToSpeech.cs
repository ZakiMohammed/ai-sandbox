using AISandbox.Utils;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;
using Microsoft.CognitiveServices.Speech.Translation;

namespace AISandbox.Services
{
    public static class SpeechToSpeech
    {
        public static async Task Run(IConfiguration config)
        {
            string? speechKey = config[AppSettings.SpeechKey];
            string? speechRegion = config[AppSettings.SpeechRegion];

            var translationConfig = SpeechTranslationConfig.FromSubscription(speechKey, speechRegion);
            translationConfig.SpeechRecognitionLanguage = AppSettings.TranslationSourceLanguage;
            translationConfig.AddTargetLanguage(AppSettings.TranslationTargetLanguage); // Urdu

            var translationAudioConfig = AudioConfig.FromDefaultMicrophoneInput();

            var translationRecognizer = new TranslationRecognizer(translationConfig, translationAudioConfig);

            Console.WriteLine("Speech to Speech Translation\n");
            Console.WriteLine($"Speak into your microphone. The translation will be spoken back in [{AppSettings.TranslationTargetLanguage}].");

            var translationResult = await translationRecognizer.RecognizeOnceAsync();

            switch (translationResult.Reason)
            {
                case ResultReason.TranslatedSpeech:
                    var inputText = translationResult.Text;
                    var outputText = translationResult.Translations[AppSettings.TranslationTargetLanguage];

                    Console.WriteLine($"Recognized: {inputText}");
                    Console.WriteLine($"Translated: {outputText}");

                    var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
                    speechConfig.SpeechSynthesisVoiceName = AppSettings.TranslationTargetVoiceName;

                    var speechAudioConfig = AudioConfig.FromDefaultSpeakerOutput();

                    var speechSynthesizer = new SpeechSynthesizer(speechConfig, speechAudioConfig);

                    var speechResult = await speechSynthesizer.SpeakTextAsync(outputText);

                    switch (speechResult.Reason)
                    {
                        case ResultReason.SynthesizingAudioCompleted:
                            Console.WriteLine("Speech synthesis completed successfully.");
                            break;
                        case ResultReason.Canceled:
                            var speechSynthesisCancellation = SpeechSynthesisCancellationDetails.FromResult(speechResult);
                            Console.WriteLine($"Speech synthesis canceled: {speechSynthesisCancellation.Reason}");
                            if (speechSynthesisCancellation.Reason == CancellationReason.Error)
                            {
                                Console.WriteLine($"Error details: {speechSynthesisCancellation.ErrorDetails}");
                            }
                            break;
                        default:
                            Console.WriteLine("Not able to perform speech synthesis operation.");
                            break;
                    }
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(translationResult);
                    Console.WriteLine($"Translation canceled: {cancellation.Reason}");
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"Error details: {cancellation.ErrorDetails}");
                    }
                    return;
                default:
                    Console.WriteLine("Not able to perform any operation");
                    break;
            }
        }
    }
}
