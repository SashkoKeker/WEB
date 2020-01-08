using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;
namespace WEB.Interfaces
{
    public interface IProfileService
    {
        Profile GetProfileById(Guid id);
        Profile CreateProfile(Profile profile);
        Profile GetProfileByUserId(Guid id);
        Profile UpdateProfile(Profile profile);
        void Delete(Guid id);
        Profile GetMyInfo();
    }
}
