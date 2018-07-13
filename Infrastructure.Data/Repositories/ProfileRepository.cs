using Application.Management.Adapters;
using Application.Management.Data.Repositories;
using Infrastructure.Data.EF;
using Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace Infrastructure.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private ClonesContextEF _db;

        public ProfileRepository(ClonesContextEF context)
        {
            _db = context;
        }

        public IEnumerable<IProfileAdapter> GetAll()
        {
            return _db.Profiles.ToList();
        }

        public IProfileAdapter Get(int id)
        {
            return _db.Profiles.Find(id);
        }

        public void Create(IProfileAdapter item)
        {
            Profile profile = new Profile(item);
            _db.Profiles.Add(profile);
        }

        public void Update(IProfileAdapter item)
        {
            Profile profile = new Profile(item);
            _db.Entry(profile).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Profile profile = _db.Profiles.Find(id);
            if (profile != null)
                _db.Profiles.Remove(profile);
        }
    }
}
