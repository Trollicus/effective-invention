using System.Text.RegularExpressions;

namespace SupremeSystem.Discord.Features;

public static class BuildNumber
{
    public static async Task<string> ReturnBuildNumber()
    {
        var request = await PostAsync("https://discord.com/app", HttpMethod.Get);
        var assets = new List<Match>(Regex.Matches(await request.Content.ReadAsStringAsync(), "/assets/.{20}.js"));
        assets.Reverse();

        foreach (var asset in assets)
        {
            var content = await GetStringAsync("https://discord.com" + asset);
            if (!content.Contains("build_number:\"")) continue;
            var m = Regex.Match(content, "build_number:\"(.*?)\"", RegexOptions.IgnoreCase);
            return m.Groups[1].Value;
        }

        return "";
    }
}