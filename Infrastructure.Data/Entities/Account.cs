using Application.Adapters;

namespace Infrastructure.Data.Entities
{
    public class Account : IAccountAdapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProfileId { get; set; }

        public virtual Profile Profile { get; set; }

        public Account() { }

        public Account(IAccountAdapter adapter)
        {
            Id = adapter.Id;
            Name = adapter.Name;
            ProfileId = adapter.ProfileId;
        }
    }
}
