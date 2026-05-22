namespace BlogPessoal.Config;

public class OpenAIOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = "gpt-4.1";
    public string BaseUrl { get; set; } = "https://api.openai.com/v1/responses";
}
