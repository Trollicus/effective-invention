namespace SupremeSystem.Features;

public static class Friends
{
    public static async Task AddFriend(string username, int discriminator, string authorization) //await AddFriend(name, desc, token);
    {
        var request = await PostAsync("https://discord.com/api/v9/users/@me/relationships", HttpMethod.Post,
            $"{{\"username\": \"{username}\", \"discriminator\": \"{discriminator}\"}}", new[]
            {
                new RequestHeadersEx("User-Agent", UserAgent),
                new RequestHeadersEx("X-Super-Properties",
                    Base64Encode(
                        XSuperProperties)),
                new RequestHeadersEx("Authorization", authorization),
                new RequestHeadersEx("Alt-Used", "discord.com"),
                new RequestHeadersEx("Referer", "https://discord.com/channels/@me"),
                new RequestHeadersEx("X-Context-Properties",
                    Base64Encode("{ \"location\": \"Add Friend\" }"))
            });

        var taskResultResponse = JsonSerializer.Deserialize<JError>(await request.Content.ReadAsStringAsync());
        
        if (request.StatusCode != HttpStatusCode.NoContent)
        {
            throw new Exception($"{taskResultResponse?.Code} : {taskResultResponse?.Message}");
        }
    }
}