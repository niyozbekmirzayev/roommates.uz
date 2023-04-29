using Roommates.Infrastructure.Enums;

namespace Roommates.Infrastructure.Base
{
    public interface IPersistentEntity
    {
        EntityState EntityState { get; set; }

        public DateTime? InactivatedDate { get; set; }
    }
}
