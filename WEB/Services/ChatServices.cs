using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;
using WEB.Interfaces;

namespace WEB.Services
{
    public class ChatServices : IChatServices
    {

        private readonly ApplicationContext _chatcontext;

        public ChatServices(ApplicationContext applicationContext)
        {
            _chatcontext = applicationContext;
        }
        public IQueryable<Chat> GetChats()
        {
            return _chatcontext.chats;
        }
        public Chat GetChatId(int id)
        {
            return GetChats().Single(x => x.ChatId == id);
        }
        public IQueryable<Chat> GetChatsId(Guid id)
        {
            return GetChats().Where(x => x.ChatUserId == id);
        }
        public Chat CreateChat(Chat chat)
        {
            _chatcontext.chats.Add(chat);
            _chatcontext.SaveChanges();

            return chat;
        }
        public Chat CreateMChat(Guid id, Guid idd, string Text, Chat chat)
        {
            chat.ChatUserId = id;
            chat.ChatToUserId = idd;
            chat.ChatTextTime = DateTime.Now;
            chat.ChatText = Text;
            _chatcontext.chats.Add(chat);
            _chatcontext.SaveChanges();

            return chat;
        }
        public void GetUserId(int idd, Chat chat)
        {
            var id = GetChatId(idd);
            var contact = _chatcontext.contacts.Where(x => x.ContactId == idd);
            //chat.ChatUserId= contact.
        }
        public void Delete(int id)
        {

        }
    }
}
