## Discord Account Generator
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0) ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) [![Telegram](https://img.shields.io/badge/Telegram-2CA5E0?style=for-the-badge&logo=telegram&logoColor=white)](https://t.me/trollicus)

High quality Discord account Generator

Built with Educational Purposes.

## Demonstration

## Getting Started

Instructions about setting up the project.

### Requirements

* .NET 7
* Anti-Captcha account with funds


### Build & Restore

Restore the nugget packages

```
dotnet restore
```

Via opening CMD in the project directory simply type

```
dotnet build
```


### Set-Up

Before usage you should:

In `Settings.cs`:

```csharp 
 public const string CaptchaToken = ""; 
```

* Set your Anti-Captcha/2Captcha(W.I.P) Token

In `Program.cs`:

* Chose between `CaptchaType.AntiCaptcha` and `CaptchaType.TwoCaptcha`
