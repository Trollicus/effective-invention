var response = await PostAsync("https://discord.com/api/v9/auth/register", HttpMethod.Post,
    $"{{\"fingerprint\": \"{await FingerPrint()}\", \"email\": \"{await CreateMail()}\", \"password\": \"{RandomString(13)}\", \"username\": \"UnrealContent\", \"consent\": true, \"date_of_birth\": \"1999-12-07\", \"captcha_key\": \"{await RunCaptcha(CaptchaType.AntiCaptcha)}\", \"invite\": \"pXUUXxZk\", \"gift_code_sku_id\":null, \"promotional_email_opt_in\": false}}",
    new[]
    {
        new RequestHeadersEx("X-Fingerprint", await FingerPrint()),
        new RequestHeadersEx("X-Super-Properties",
            Base64Encode(XSuperProperties)),
        new RequestHeadersEx("Referer", "https://discord.com/register"),
        new RequestHeadersEx("Origin", "https://discord.com"),
        new RequestHeadersEx("User-Agent", UserAgent),
        new RequestHeadersEx("Host", "discord.com"),
        new RequestHeadersEx("X-Discord-Locale", "en-US"),
    }); 

Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.ReadLine();
