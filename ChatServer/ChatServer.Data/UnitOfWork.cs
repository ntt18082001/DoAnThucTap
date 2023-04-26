using ChatServer.Data.Interfaces.Repositories;
using ChatServer.Data.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppChatDbContext _context;
		public ILoginRepository LoginRepository { get; private set; }
		public IUserRepository UserRepository { get; private set; }
		public IFriendRequestRepository FriendRequestRepository { get; private set; }
		public IMessageRepository MessageRepository { get; private set; }

		public UnitOfWork(
			AppChatDbContext context,
			ILoginRepository loginRepository,
			IUserRepository userRepository,
			IFriendRequestRepository friendRequestRepository,
			IMessageRepository messageRepository)
		{
			_context = context;
			LoginRepository = loginRepository;
			UserRepository = userRepository;
			FriendRequestRepository = friendRequestRepository;
			MessageRepository = messageRepository;
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
