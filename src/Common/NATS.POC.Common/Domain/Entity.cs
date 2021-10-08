using System;

namespace NATS.POC.Common.Domain
{
    public abstract class Entity
    {
        public virtual Guid Id
        {
            get;

            protected set;
        }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
