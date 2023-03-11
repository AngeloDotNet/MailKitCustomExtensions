# MailKit Custom Extensions

If you like this repository, please drop a :star: on Github!


## Installation

The library is available on [NuGet](https://www.nuget.org/packages/MailKitCustomExtensions) or run the following command in the .NET CLI:

```bash
dotnet add package MailKitCustomExtensions
```


## Configuration to add to the appsettings.json file

```json
"Smtp": {
  "Host": "example.org",
  "Port": 25,
  "Security": "StartTls",
  "Username": "Username SMTP",
  "Password": "Password SMTP",
  "Sender": "MyName <noreply@example.org>"
}
```


## Registering services at Startup

```csharp
public Startup(IConfiguration configuration)
{
  Configuration = configuration;
}

public IConfiguration Configuration { get; }
	
public void ConfigureServices(IServiceCollection services)
{
    services.AddEmailSenderService(Configuration);
}
```


## Usage example

```csharp
var result = await emailClient.SendEmailAsync(string recipientEmail, string replyToEmail, string subject, string htmlMessage);

if (!result)
{
    return false;
}

return true;
```


## Contributing

Contributions and/or suggestions are always welcome.
