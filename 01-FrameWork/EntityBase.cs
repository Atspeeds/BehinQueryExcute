using System;

namespace _01_FrameWork
{
    public abstract class EntityBase<T>
    {
        public T KeyId { get; private set; }
        public DateTime CreationDate { get; private set; }

        public EntityBase()
        {
            CreationDate = DateTime.Now;
        }
    }
}
