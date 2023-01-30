namespace SupremeSystem.Captcha;

public static class Urls
{
    private const string BaseUrl = "https://api.anti-captcha.com";

    public const string CreateTask = $"{BaseUrl}/createTask";

    public const string GetResult = $"{BaseUrl}/getTaskResult";
}