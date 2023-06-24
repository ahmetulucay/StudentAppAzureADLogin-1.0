
using Azure;
using Grpc.Core;
using GrpcMessage;
using LanguageExt.Common;
using Nest;
using StudentApp.Models;
using StudentApp.Services;
using System;
using AddStudentRequest = StudentApp.Models.AddStudentRequest;
using UpdateStudentRequest = StudentApp.Models.UpdateStudentRequest;

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
        var result = await _service.Get();
        var phoneList = new List<PhoneStudentRequest>();

        if (result is null)
        {
            _logger.LogDebug("Students data not retrieved from the service.");
            await responseStream.WriteAsync(new GrpcMessage.GetAllStudentsResponse());
        }
        else
        {
            for (var i = 0; i < result.Count; i++)
            {
                await responseStream.WriteAsync(new GrpcMessage.GetAllStudentsResponse 
                {
                    Id = result[i].StudentId,
                    FirstName = result[i].FirstName,
                    SecondName = result[i].SecondName,
                    LastName = result[i].LastName,
                    UserName = result[i].UserName,
                    School = result[i].School
                    //for (var j = 0; j < result.Count; j++)
                    //{
                    //phoneList.Add(new PhoneStudentRequest(result));
                    //}
                });
            }
        }
    }

    public override async Task GetStudent(GrpcMessage.GetStudentRequest request, IServerStreamWriter<GrpcMessage.GetStudentResponse> responseStream, ServerCallContext context)
    {
        var result = await _service.GetAsId((int)request.Id);
        if (result == null)
        {
            _logger.LogWarning($"Wrong student id:{request.Id}.");
        }
        else
        {
            await responseStream.WriteAsync(new GrpcMessage.GetStudentResponse
            {
                Id = result.StudentId,
                FirstName = result.FirstName,
                SecondName = result.LastName,
                LastName = result.LastName,
                UserName = result.UserName,
                School = result.School
            });
        }
    }

    public override async Task<AddStudentResponse> AddStudent(GrpcMessage.AddStudentRequest request, ServerCallContext context)
    {
        var requestDB = new AddStudentRequest();
        var phoneList = new List<PhoneStudentRequest>();
        var grpcPhones = request.PhoneNumber;

        for (var i = 0; i < grpcPhones.Count; i++)
        {
            phoneList.Add(new PhoneStudentRequest(
                new StudentPhoneNo 
                {
                    PhoneNo = grpcPhones[i].PhoneNumber,
                }));
        }

        _logger.LogInformation("1");

        var student = new AddStudentRequest
        {
            UserName = request.UserName,
            FirstName = request.FirstName, 
            SecondName = request.SecondName,
            LastName = request.LastName,
            School = request.School,
            RegistrationDate = DateTime.UtcNow,
            PhoneStudent = phoneList,
            AddressStudent = new List<AddressStudentRequest>(),
            ImageStudent   = new List<ImageStudentRequest>(),
            EmailAddressStudent = new List<EmailAddressStudentRequest>()
        };

        _logger.LogInformation("2");
        var studentDB = requestDB.ToStudent(student);
        _logger.LogInformation("3");

        var result = await _service.AddStudent(studentDB);
        _logger.LogInformation("4");
        if (result == null)
        {
            _logger.LogWarning("Wrong student data");
            return new AddStudentResponse();
        }
        else
        {
            _logger.LogInformation("Student added in the database");
            return new AddStudentResponse();
        }
    }

    public override async Task<UpdateStudentResponse> UpdateStudent(GrpcMessage.UpdateStudentRequest request, ServerCallContext context)
    {
        var requestDB = new UpdateStudentRequest();

        var student = new UpdateStudentRequest
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            SecondName = request.SecondName,
            LastName = request.LastName,
            School = request.School,
            RegistrationDate = DateTime.UtcNow,
            PhoneStudent = new List<PhoneStudentRequest>(),
            AddressStudent = new List<AddressStudentRequest>(),
            ImageStudent = new List<ImageStudentRequest>(),
            EmailAddressStudent = new List<EmailAddressStudentRequest>()
        };

        var studentDB = requestDB.ToUpdateStudent(student);

        var result = await _service.UpdateStudent((int)request.Id, studentDB);
        if (result == null)
        {
            _logger.LogWarning("Wrong student data");
            return new UpdateStudentResponse();
        }
        else
        {
            _logger.LogInformation("Student updated in the database");
            return new UpdateStudentResponse();
        }
    }

    public override async Task<DeleteStudentResponse> DeleteStudent (GrpcMessage.DeleteStudentRequest request, ServerCallContext context)
    {
        var result = await _service.DeleteStudent((int)request.Id);
        if (result == null)
        {
            _logger.LogWarning($"Wrong student id:{request.Id}.");
            return new DeleteStudentResponse();
        }
        if (result == false)
        {
            _logger.LogError($"Deleting student id:{request.Id} is NOT successful");
            return new DeleteStudentResponse();
        }
        _logger.LogInformation($"True: Deleting Id {request.Id} is successful");
        return new DeleteStudentResponse();
    }




    //Ex-Grpc Messages
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

