using System.Collections.Generic;
using Application.Adapters;

namespace Infrastructure.Data.Entities
{
    public class Profile : IProfileAdapter
    {
        public Profile()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public IEnumerable<IAccountAdapter> AccountAdapters { get => Accounts as IEnumerable<IAccountAdapter>; }

        public Profile(IProfileAdapter adapter) : this()
        {
            Id = adapter.Id;
            Name = adapter.Name;
            Accounts = new List<Account>();
            
            if (adapter.AccountAdapters != null)
                foreach (var item in adapter.AccountAdapters)
                    Accounts.Add(new Account(item));
        }
    }
}
