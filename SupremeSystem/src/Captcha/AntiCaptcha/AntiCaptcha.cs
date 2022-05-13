namespace SupremeSystem.Captcha.AntiCaptcha;

public static class AntiCaptcha
{
    private const string BaseUrl = "https://api.anti-captcha.com";

    private const string CreateTask = $"{BaseUrl}/createTask";

    private const string GetResult = $"{BaseUrl}/getTaskResult";

    private const string TaskJson =
        $"{{ \"clientKey\": \"{CaptchaToken}\", \"task\": {{ \"type\": \"HCaptchaTaskProxyless\", \"websiteURL\": \"https://discord.com/register\", \"websiteKey\": \"{WebsiteToken}\", \"userAgent\": \"{UserAgent}\" }} }}";

    
    private static CreateTaskResult? Deserialize(string json) =>
        JsonSerializer.Deserialize<CreateTaskResult>(json);

    private static async Task<CreateTaskResult?> CreateTaskAsync() =>
        Deserialize(await (await PostAsync(CreateTask, HttpMethod.Post, TaskJson)).Content.ReadAsStringAsync());

    private static async Task<GetTaskResultResponse?> GetTaskResult(CreateTaskResult createTaskResult)
    {
        var json = JsonSerializer.Serialize(new GetTaskResult() { TaskId = createTaskResult.TaskId, clientKei = CaptchaToken});

        //Console.WriteLine(json);
        
        GetTaskResultResponse? taskResultResponse;

        do
        {
            var response = await PostAsync(GetResult, HttpMethod.Post, json).Result.Content.ReadAsStringAsync();
            taskResultResponse = JsonSerializer.Deserialize<GetTaskResultResponse>(response);   

            if (taskResultResponse?.CurrentStatus == "ready")
                break;

            Thread.Sleep(2000);
        } while (true);
        
        return taskResultResponse;
    }

    public static async Task<string?> GetSolution()
    {
        var taskResult = await CreateTaskAsync();
        if (taskResult!.ErrorId == 1)
            throw new Exception($"{taskResult.ErrorCode} : {taskResult.ErrorDescription}");

        var taskResultResponse = await GetTaskResult(taskResult);
        if(taskResultResponse!.ErrorId == 1)
            throw new Exception($"{taskResultResponse.ErrorCode} : {taskResultResponse.ErrorDescription}");
        
        //Console.WriteLine(taskResultResponse.Solution.CaptchaSolution);
        
        return taskResultResponse.Solution?.CaptchaSolution;
    }
    
}