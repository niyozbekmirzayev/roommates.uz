
using Roommates.Domain.Enums;

namespace Roommates.Domain.Base
{
    public interface IPersistentEntity
    {
        EntityState EntityState { get; set; }

        public DateTime? InactivatedDate { get; set; }
    }
}
