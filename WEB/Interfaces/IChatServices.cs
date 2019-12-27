using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;

namespace WEB.Interfaces
{
    interface IChatServices
    {
        IQueryable<Chat> GetChat();
        Chat GetChatId(int id);
        IQueryable<Chat> GetChatUsersId(Guid id);        
        Chat CreateMChat(Chat chat);
        void Delete(int id);
    }
}
