using AutoMapper;
using ChatServer.Api.Common.Mailer;
using ChatServer.Api.Services;
using ChatServer.Api.Services.Interfaces;
using ChatServer.Api.ViewModels.User;
using ChatServer.Data.Entities;
using ChatServer.Data.Services;
using ChatServer.Shared.Consts;
using ChatServer.Shared.DTOs;
using ChatServer.Shared.DTOs.User;
using ChatServer.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using RazorEngine;
using RazorEngine.Templating;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace ChatServer.Api.Controllers
{
	[Route("[controller]")]
	[Authorize]
	public class AccountController : AppControllerBase
	{
		private readonly UserService _userService;
		private readonly IFileStorageService _fileStorageService;
		private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
		private readonly AppMailConfiguration _mailConfig;
		public AccountController(
			IMapper mapper,
			UserService userService,
			IFileStorageService fileStorageService,
			Microsoft.AspNetCore.Hosting.IHostingEnvironment env,
			AppMailConfiguration mailConfig) : base(mapper)
		{
			_userService = userService;
			_fileStorageService = fileStorageService;
			_env = env;
			_mailConfig = mailConfig;
		}
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
		{
			if (!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
											.SelectMany(x => x.Errors)
											.Select(x => x.ErrorMessage)
											.ToList();
				return BadRequest(new
				{
					Error = errorMessages
				});
			}
			if (await _userService.CheckUserExist(x => x.Email == registerDTO.Email && x.DeletedDate == null))
			{
				return BadRequest(new
				{
					Error = new
					{
						Email = "Email đã tồn tại!"
					}
				});
			}
			try
			{
				var hashResult = PwdHashHelper.HashHMACSHA512(registerDTO.Password);
				var user = _mapper.Map<AppUser>(registerDTO);
				user.PasswordHash = hashResult.Value;
				user.PasswordSalt = hashResult.Key;
				user.Username = registerDTO.Email;
				user.AppRoleId = RoleConst.UserRole.ID;
				user.Avatar = DEFAULT_FOLDER_AVATAR + "/" + DEFAULT_AVATAR;
				user.MessageKey = StringHasher.CreateSalt();
				var isSuccess = await _userService.Register(user);
				if(isSuccess)
				{
					SendMail(user.Username, user.FullName, "emailSuccess.html");
				}
				return Ok(isSuccess);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("updateuser")]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
		{
			var user = await _userService.GetUser(updateUserDTO.Id);
			if(user == null)
			{
				return BadRequest("Không tìm thấy người dùng!");
			}
			try
			{
				user.FullName = updateUserDTO.FullName;
				user.PhoneNumber1 = updateUserDTO.PhoneNumber;
				user.Address = updateUserDTO.Address;
				var isSuccess = await _userService.UpdateProfileUser(user);
				return Ok(new
				{
					Id = user.Id,
					FullName = user.FullName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber1,
					Avatar = user.Avatar,
					Role = user.AppRole.Name,
					Address = user.Address
				});
			} catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPost("updateavatar")]
		public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarVM model)
		{
			var user = await _userService.GetUser(model.Id);
			if (user == null)
			{
				return BadRequest("Không tìm thấy người dùng!");
			}
			try
			{
				if (model.File != null)
				{
					var fileName = await _fileStorageService.SaveFile(DEFAULT_FOLDER_AVATAR, model.File);
					user.Avatar = DEFAULT_FOLDER_AVATAR + "/" + fileName;
				}
				var isSuccess = await _userService.UpdateProfileUser(user);
				if (isSuccess)
				{
					return Ok(new
					{
						Avatar = user.Avatar
					});
				}
				else
				{
					return BadRequest(ERROR_NAME);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPost("changepassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
		{
			if (!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
											.SelectMany(x => x.Errors)
											.Select(x => x.ErrorMessage)
											.ToList();
				return BadRequest(new
				{
					Error = errorMessages
				});
			}
			var user = await _userService.GetUser(model.Id);
			if (user == null)
			{
				return BadRequest(ERROR_NAME);
			}
			try
			{
				var encryptPwd = PwdHashHelper.HashHMACSHA512WithKey(model.Password, user.PasswordSalt);
				if (!encryptPwd.SequenceEqual(user.PasswordHash))
				{
					return BadRequest(new
					{
						Error = new
						{
							Password = "Mật khẩu cũ không chính xác!"
						}
					});
				}
				var hashResult = PwdHashHelper.HashHMACSHA512(model.NewPassword);
				user.PasswordHash = hashResult.Value;
				user.PasswordSalt = hashResult.Key;
				var isSuccess = await _userService.UpdateUser(user);
				return Ok(isSuccess);
			} catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpGet("GetListFriend")]
		public async Task<IActionResult> GetListFriend()
		{
			try
			{
				var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
				var result = await _userService.GetListFriend(Convert.ToInt32(id));
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[AllowAnonymous]
		[HttpPost("forgotpassword")]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
		{
			if (!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
											.SelectMany(x => x.Errors)
											.Select(x => x.ErrorMessage)
											.ToList();
				return BadRequest(new
				{
					Error = errorMessages
				});
			}
			var user = await _userService.GetUser(model.Email);
			if (user == null)
			{
				return BadRequest(new
				{
					Error = new
					{
						Email = "Email không tồn tại!"
					}
				});
			}
			try
			{
				var code = VerifyCodeHelper.CreateCode();
				var verifyCode = new AppVerifyCode()
				{
					TokenString = code,
					CreatedDate = DateTime.Now,
					Expired = DateTime.Now.AddMinutes(EXPIRED_VERIFY_CODE),
					UserId = user.Id
				};
				await _userService.AddVerifyCode(verifyCode);
				SendMail(user.Email, user.FullName, "emailTemplate.html", code);
				return Ok(new
				{
					Email = model.Email
				});
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[AllowAnonymous]
		[HttpPost("checkcode")]
		public async Task<IActionResult> CheckVerifyCode([FromBody] VerifyCodeVM model)
		{
			try
			{
				var code = await _userService.GetVerifyCode(model.Code);
				if (code == null)
				{
					return BadRequest(new
					{
						Error = new
						{
							Code = "Code không tồn tại!"
						}
					});
				}
				if (VerifyCodeHelper.IsCodeExpired(code.Expired))
				{
					return BadRequest(new
					{
						Error = new
						{
							Code = "Code đã hết hạn!"
						}
					});
				}
				return Ok(new
				{
					Code = code.TokenString
				});
			}
			catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[AllowAnonymous]
		[HttpPost("createnewpwd")]
		public async Task<IActionResult> CreateNewPwd([FromBody] NewPasswordVM model)
		{
			if (!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
											.SelectMany(x => x.Errors)
											.Select(x => x.ErrorMessage)
											.ToList();
				return BadRequest(new
				{
					Error = errorMessages
				});
			}
			var code = await _userService.GetVerifyCode(model.Code);
			var user = await _userService.GetUser(code.UserId ?? 0);
			if(user == null)
			{
				return BadRequest(ERROR_NAME);
			}
			try
			{
				var hashResult = PwdHashHelper.HashHMACSHA512(model.Password);
				user.PasswordHash = hashResult.Value;
				user.PasswordSalt = hashResult.Key;
				var isSuccess = await _userService.UpdateUser(user);
				return Ok(isSuccess);
			}
			catch(Exception ex)
			{
				return Ok(false);
			}
		}

		private void SendMail(string email, string fullName, string template, string code = "")
		{
			try
			{
				var pathToFile = $"{_env.WebRootPath}" +
					$"{Path.DirectorySeparatorChar}" +
					$"mailTemplate{Path.DirectorySeparatorChar}{template}";

				var appMailSender = new AppMailSender()
				{
					Name = "TS-Chat",
					Subject = "Cảm ơn bạn đã đăng ký sử dụng TS-Chat"
				};

				using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
				{
					appMailSender.Content = SourceReader.ReadToEnd();
				};

				var appMailReciver = new AppMailReceiver()
				{
					Email = email,
					Name = fullName
				};

				var contentMessage = Engine.Razor
					.RunCompile(appMailSender.Content, "@",
					null,
					new
					{
						Name = appMailReciver.Name,
						Signature = _mailConfig.Signature,
						Code = code
					});
				appMailSender.Content = contentMessage;

				AppMailer _emailMap = new AppMailer(_mailConfig);
				_emailMap.Sender = appMailSender;
				_emailMap.Reciver = appMailReciver;
				_emailMap.Send();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}
