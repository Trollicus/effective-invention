var response = await PostAsync("https://discord.com/api/v9/auth/register", HttpMethod.Post,
    $"{{\"fingerprint\": \"{await FingerPrint()}\", \"email\": \"{await CreateMail()}\", \"password\": \"{RandomString(10)}\", \"username\": \"GorgTheBOlen\", \"global_name\":\"bolenHEBI\", \"unique_username_registration\": true, \"consent\": true, \"date_of_birth\": \"2005-03-13\", \"invite\": \"\", \"gift_code_sku_id\":null, \"promotional_email_opt_in\": false}}",
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
        new RequestHeadersEx("X-Captcha-Key", await RunCaptcha(CaptchaType.AntiCaptcha))
    }); 

Console.WriteLine(await response.Content.ReadAsStringAsync());
Console.ReadLine();
