using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Models;
using WEB.Interfaces;
namespace WEB.Services
{
    public class ProfileService : IProfileService
    {
        private readonly CurrentUserInfo _info;
        private readonly ApplicationContext _profilecontext;

        public ProfileService(ApplicationContext profilecontext, CurrentUserInfo info)
        {
            _profilecontext = profilecontext;
            _info = info;
        }

        public IQueryable<Profile> GetAll()
        {
            return _profilecontext.Profiles;
        }

        public Profile GetProfileById(Guid id)
        {
            var profile = GetAll().Single(x => x.Id == id);
            return profile;
        }

        public Profile GetMyInfo()
        {
            var id = _info.Id;
            var profile = _profilecontext.Profiles.First(x => x.UserId == id);
            return profile;
        }

        public Profile GetProfileByUserId(Guid id)
        {
            var profile = GetAll().Single(x => x.UserId == id);
            return profile;
        }

        public Profile CreateProfile(Profile profile)
        {
            _profilecontext.Profiles.Add(profile);
            _profilecontext.SaveChanges();

            return profile;
        }

        public Profile UpdateProfile(Profile profile)
        {
            var id = _profilecontext.Profiles.First(x => x.UserId == _info.Id).Id;
            profile.UserId = _info.Id;
            profile.Id = id;
            _profilecontext.ChangeTracker.Entries().ElementAt(0).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _profilecontext.SaveChanges();
            _profilecontext.Attach(profile);
            _profilecontext.Entry(profile).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _profilecontext.SaveChanges();

            return profile;
        }

        public void Delete(Guid Id)
        {
            _profilecontext.Remove(GetProfileById(Id));
            _profilecontext.SaveChanges();
        }


    }
}
