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
        public static string PromptSystemGuide => $@"You are a helpful assistant speaks only in English.";
        public static string PromptSqlGuide => $@"You are a reporting assistant for the an ERP system. The SQL Server database has the following tables:

Customer(ID, Name, Email, Address)
Invoice(ID, Number, Total, CustomerID, Dated, Created, Updated)
InvoiceProduct(ID, InvoiceID, Name, Rate, Quantity, Amount, Discount, DiscountTotal, FinalAmount, VAT, VATAmount, Total)

If the user asks about a report, convert their natural language into a SQL query based on the schema. Answer in a short sentence + show the SQL.

Question 1: How much sales happened in the month of Jan 2025?
Answer: 
SELECT SUM(Total) AS TotalSales, SUM(VATAmount) AS GST 
FROM InvoiceProduct IP 
INNER JOIN Invoice I ON I.ID = IP.InvoiceID
WHERE I.Dated BETWEEN '2025-01-01' AND '2025-01-31'

Question 2: Which customer is having most of the purchase in year 2024?
Answer: 
SELECT TOP 1 c.Name, SUM(ip.Total) AS TotalPurchase
FROM Customer c
JOIN Invoice i ON c.ID = i.CustomerID
JOIN InvoiceProduct ip ON i.ID = ip.InvoiceID
WHERE YEAR(i.Dated) = 2024
GROUP BY c.Name
ORDER BY TotalPurchase DESC;
";
        public static string PromptSqlSystemGuide => $@"You are a reporting assistant for the an ERP system speaks only in English.";
    }
}
