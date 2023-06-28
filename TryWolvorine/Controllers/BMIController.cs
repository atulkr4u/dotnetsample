using Microsoft.AspNetCore.Mvc;
using MediatR;
using Wolverine;
namespace TryWolvorine.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BMIController : ControllerBase
{
    private readonly ILogger<BMIController> _logger;
    private readonly IMessageBus _messageBus;

    public BMIController(ILogger<BMIController> logger,IMessageBus messageBus)
    {
        _logger = logger;
        _messageBus = messageBus;
    }

    [HttpPost]
    public async Task<BMIResponse> CalculateBMI(BMIRequest request)
    {
        var response = await _messageBus.InvokeAsync<BMIResponse>(request);
        return response;
    }
}

