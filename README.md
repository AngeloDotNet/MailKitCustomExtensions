# MailKit Custom Extensions

If you like this repository, please drop a :star: on Github!


## Installation

The library is available on [NuGet](https://www.nuget.org/packages/MailKitCustomExtensions) or run the following command in the .NET CLI:

```bash
dotnet add package MailKitCustomExtensions
```


## Configuration to add to the appsettings.json file

```json
"Serilog": {
  "MinimumLevel": "Warning",
  "WriteTo": [
    {
      "Name": "Console",
      "Args": {
        "outputTemplate": "{Timestamp:HH:mm:ss}\t{Level:u3}\t{SourceContext}\t{Message}{NewLine}{Exception}"
      }
    },
    {
      "Name": "File",
      "Args": {
        "path": "Logs/log.txt",
        "rollingInterval": "Day",
        "retainedFileCountLimit": 14,
        "restrictedToMinimumLevel": "Warning",
        "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog"
      }
    }
  ]
},
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
  services.AddBusinessLayerServices();
  services.AddEmailSenderService(Configuration);
}

//OMISSIS

public void Configure(WebApplication app)
{
    app.AddApplicationServices();
}
```


## Registering services at Program

```csharp
public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.AddOptionsBuilder();

        Startup startup = new(builder.Configuration);

        //OMISSIS
    }
```


## Example of the InputMailSender class
```csharp
public class InputMailSender
{
  public string RecipientEmail { get; set; }
  public string ReplyToEmail { get; set; }
  public string Subject { get; set; }
  public string HtmlMessage { get; set; }
}
```


## Example of use in a web api controller
```csharp
[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
  private readonly ILoggerService loggerService;
  private readonly IEmailClient emailClient;

  public EmailController(ILoggerService loggerService, IEmailClient emailClient)
  {
    this.loggerService = loggerService;
    this.emailClient = emailClient;
  }
  
  [HttpPost("InvioEmail")]
  public async Task<IActionResult> InvioEmail([FromForm] InputMailSender model)
  {
      try
      {
        var result = await emailClient.SendEmailAsync(model.RecipientEmail, model.ReplyToEmail, model.Subject, model.HtmlMessage);
        
        if (!result)
        {
            return BadRequest();
        }

        return Ok();
      }
      catch
      {
         var message = "Errore durante l'invio della mail";
         var errori = loggerService.ManageError(message, 400, 0, HttpContext);
         return StatusCode(400, errori);
      }
  }
}
```


## Note:

If you are using this Nuget package, you can avoid using the SerilogCustomExtensions package, as it is already integrated in this one.


## Contributing

Contributions and/or suggestions are always welcome.
