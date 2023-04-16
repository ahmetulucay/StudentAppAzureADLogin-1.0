using Microsoft.AspNetCore.Http;

namespace StudentApp.AzureStorage;

public interface IStorageService 
{ 
    void Upload(IFormFile formFile); 
};