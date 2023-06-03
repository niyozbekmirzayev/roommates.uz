using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Infrastructure.Base
{
    public interface IEntityAuthorship : IEntityPersistent
    {
        public Guid CreatedById { get; set; }

        public Guid? InactivatedById { get; set; }

        public void Create(Guid createdById);

        public void Inactivate(Guid InactivatedById);
    }
}
