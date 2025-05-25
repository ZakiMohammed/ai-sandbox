# AISandbox

Exploring AI Service in sandbox console app built using C#.

Covering below Azure services:

1. Azure AI Speech
1. Azure AI Translate
1. Azure OpenAI

Covering below operations:

1. Speech-to-Text
1. Text-to-Speech
1. Speech-to-Speech
1. Text-to-OpenAI

For Speech operations, we will use Azure AI Speech SDK for C#. The NuGet Packages:
- Microsoft.CognitiveServices.Speech
- Microsoft.CognitiveServices.Speech.Audio;
- Microsoft.CognitiveServices.Speech.Translation;

For OpenAI operations, we will use Azure OpenAI SDK for C#. The NuGet Packages:
- Azure.AI.OpenAI

## 1. Speech-to-Text

This operation converts spoken language into written text using Azure AI Speech service.

Algoirthm:

```cs
// 1. Create SpeechConfig
var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
speechConfig.SpeechRecognitionLanguage = "en-US";

// 2. Create AudioConfig
var audioConfig = AudioConfig.FromDefaultMicrophoneInput();

// 3. Create SpeechRecognizer
var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

Console.WriteLine("Speak into your microphone.");

// 4. Start Recognition
var result = await speechRecognizer.RecognizeOnceAsync();
```

## 2. Text-to-Speech

This operation converts written text into spoken language using Azure AI Speech service.

Algoirthm:
```cs
// 1. Create SpeechConfig
var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
speechConfig.SpeechSynthesisVoiceName = "en-US-AvaMultilingualNeural";

// 2. Create AudioConfig
var audioConfig = AudioConfig.FromDefaultSpeakerOutput();

// 3. Create SpeechSynthesizer
var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioConfig);

Console.WriteLine("Enter text to synthesize:");
var text = Console.ReadLine();

// 4. Start Synthesis
var result = await speechSynthesizer.SpeakTextAsync(text);
```

## 3. Speech-to-Speech

This operation converts spoken language into another spoken language using Azure AI Speech service.

Algoirthm:
```cs
// 1. Create SpeechConfig for Translation
var translationConfig = SpeechTranslationConfig.FromSubscription(speechKey, speechRegion);
translationConfig.SpeechRecognitionLanguage = AppSettings.TranslationSourceLanguage;
translationConfig.AddTargetLanguage(AppSettings.TranslationTargetLanguage); // Urdu

// 2. Create AudioConfig for Translation
var translationAudioConfig = AudioConfig.FromDefaultMicrophoneInput();

// 3. Create TranslationRecognizer
var translationRecognizer = new TranslationRecognizer(translationConfig, translationAudioConfig);

Console.WriteLine("Speak into your microphone.");

// 4. Start Translation
var translationResult = await translationRecognizer.RecognizeOnceAsync();

// If translationResult.Reason == ResultReason.TranslatedSpeech then go to next step

// 5. Create SpeechConfig for Synthesis
var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
speechConfig.SpeechSynthesisVoiceName = AppSettings.TranslationTargetVoiceName;

// 6. Create AudioConfig for Synthesis
var audioConfig = AudioConfig.FromDefaultSpeakerOutput();

// 7. Create SpeechSynthesizer
var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioConfig);

// 8. Start Synthesis
var speechResult = await speechSynthesizer.SpeakTextAsync(outputText);
```