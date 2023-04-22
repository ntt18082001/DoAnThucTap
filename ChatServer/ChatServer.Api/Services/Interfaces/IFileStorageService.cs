namespace ChatServer.Api.Services.Interfaces
{
	public interface IFileStorageService
	{
		Task<string> SaveFile(string filePath, IFormFile file);
		Task<string> EditFile(string filePath, IFormFile file, string fileRoute);
		Task DeleteFile(string filePath, string containerName);
	}
}
