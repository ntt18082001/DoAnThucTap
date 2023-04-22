using ChatServer.Api.Services.Interfaces;

namespace ChatServer.Api.Services
{
	public class FileStorageService : IFileStorageService
	{
		private readonly IWebHostEnvironment env;

		public FileStorageService(IWebHostEnvironment _env)
		{
			env = _env;
		}
		public Task DeleteFile(string filePath, string containerName)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				return Task.CompletedTask;
			}

			var fileName = Path.GetFileName(filePath);
			var fileDirectory = Path.Combine(env.WebRootPath, containerName, fileName);

			if (File.Exists(fileDirectory))
			{
				File.Delete(fileDirectory);
			}

			return Task.CompletedTask;
		}

		public async Task<string> EditFile(string filePath, IFormFile file, string fileRoute)
		{
			await DeleteFile(fileRoute, filePath);
			return await SaveFile(filePath, file);
		}

		public async Task<string> SaveFile(string filePath, IFormFile file)
		{
			var extension = Path.GetExtension(file.FileName);
			var fileName = $"{Guid.NewGuid()}{extension}";
			string folder = Path.Combine(env.WebRootPath, filePath);

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			string route = Path.Combine(folder, fileName);
			using (var ms = new MemoryStream())
			{
				await file.CopyToAsync(ms);
				var content = ms.ToArray();
				await File.WriteAllBytesAsync(route, content);
			}

			return fileName;
		}
	}
}
