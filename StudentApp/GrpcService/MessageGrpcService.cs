
using Azure;
using Grpc.Core;
using StudentApp.Models;
using StudentApp.Services;

namespace StudentApp.GrpcService;

public class MessageGrpcService : GrpcMessage.Message.MessageBase
{
    private readonly IService _service;
    private readonly ILogger<MessageGrpcService> _logger;
    public MessageGrpcService(IService service, ILogger<MessageGrpcService> logger)
    {
        _service = service;
        _logger = logger;
    }

    public override async Task GetAllStudents(GrpcMessage.GetAllStudentsRequest request, IServerStreamWriter<GrpcMessage.GetAllStudentsResponse> responseStream, ServerCallContext context)
    {
        var students = new List<StudentResponse>();
        var result = await _service.Get();
        if (result is null)
        {
            _logger.LogDebug("Students data not retrieved from the service.");
            await responseStream.WriteAsync(new GrpcMessage.GetAllStudentsResponse());
        }
        else
        {
            for (var i = 0; i < result.Count; i++)
            {
                await responseStream.WriteAsync(new GrpcMessage.GetAllStudentsResponse { 
                    Id = result[i].StudentId,
                    FirstName = result[i].FirstName,
                    SecondName = result[i].LastName,
                    LastName = result[i].LastName,
                    UserName = result[i].UserName,
                    School  = result[i].School
                });
            }

        }
    }

    public override async Task GetAllMessages(GrpcMessage.GetAllMessagesRequest request, IServerStreamWriter<GrpcMessage.GetAllMessagesResponse> responseStream, ServerCallContext context)
    {
        for (var i = 0; i < 10; i++)
        {
            var response = new GrpcMessage.GetAllMessagesResponse
            {
                Id = i + 1,
                From = "A",
                To = "O",
                Message = "Napiyon Kanka",
            };
            await responseStream.WriteAsync(response);
        }
    }

    public override async Task<GrpcMessage.GetMessageResponse> GetMessage(GrpcMessage.GetMessageRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Requested message id {request.Id}");
        var response = new GrpcMessage.GetMessageResponse
        {
            Response = new GrpcMessage.GetAllMessagesResponse
            {
                Id = request.Id,
                From = "A",
                To = "O",
                Message = "Napiyon Kanka",
            }
        };
        return await Task.FromResult(response);
    }

    public override async Task<GrpcMessage.SendMessageResponse> SendMessage(GrpcMessage.SendMessageRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Sending message id {request.Id}, from {request.From}, to {request.To}, message {request.Message}");
        return await Task.FromResult(new GrpcMessage.SendMessageResponse { Id = -1 });
    }

}

