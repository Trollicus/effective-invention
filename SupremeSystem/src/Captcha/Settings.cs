namespace SupremeSystem.Captcha;

public static class Settings
{
    public const string CaptchaToken = ""; //Your Anti-Captcha or 2Captcha token. 

    public const string WebsiteToken = "4c672d35-0701-42b2-88c3-78380b0db560"; 

    public const string UserAgent =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
    
    public enum CaptchaType // Yet to be added more, feel free to contribute.
    {
        AntiCaptcha,
        TwoCaptcha
    }

    public static async Task<string?> RunCaptcha(CaptchaType type) 
    {
        if (type == CaptchaType.AntiCaptcha)
            return GetSolution().Result;
        return null;
    }
}