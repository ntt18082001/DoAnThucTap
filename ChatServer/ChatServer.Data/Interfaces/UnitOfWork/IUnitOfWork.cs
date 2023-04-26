using ChatServer.Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ILoginRepository LoginRepository { get; }
        IUserRepository UserRepository { get; }
        IFriendRequestRepository FriendRequestRepository { get; }
        IMessageRepository MessageRepository { get; }
		Task SaveAsync();
    }
}
