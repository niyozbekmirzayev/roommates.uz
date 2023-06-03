using Roommates.Infrastructure.Enums;

namespace Roommates.Infrastructure.Base
{
    public interface IEntityPersistent
    {
        public EntityState EntityState { get; set; }

        public DateTime? InactivatedDate { get; set; }
    }
}
