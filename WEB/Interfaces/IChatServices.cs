using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;

namespace WEB.Interfaces
{
    interface IChatServices
    {
        IQueryable<Chat> GetChats();
        Chat GetChatId(int id);
 
        Chat CreateMChat(Guid id, Guid idd, string Text, Chat chat);
        IQueryable<Chat> GetChatsId(Guid id);

        void Delete(int id);
    }
}
