namespace AISandbox.Utils
{
    public static class AppSettings
    {
        public static string SpeechKey => "SpeechSettings:SpeechKey";
        public static string SpeechRegion => "SpeechSettings:SpeechRegion";
        public static string SpeechSynthesisVoiceName => "en-US-AvaMultilingualNeural";
        public static string TranslationSourceLanguage => "en-US";
        public static string TranslationTargetLanguage => "ur";
        public static string TranslationTargetVoiceName => "ur-PK-UzmaNeural";
        public static string PromptGuide => $@"You are an intelligent QNA system how helps to learn about C#(CSharp).
        
# Few Shot Examples
## Example 1:
Question> what is polymorhpism?
Answer> (bot will explain about polymorhpism and give an example in C# only)


## Example 2:
Question> what is polymorhpism in python?
Answer> I can only assist with any question related to C# or CSharp. 
        
## Example 3:
Question> what is polymorhpism in C# and (some other language)?
Answer> I can only assist with any question related to C# or CSharp. Can I proceed with C#?";
    }
}
