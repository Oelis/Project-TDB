using System;
using Units;

namespace Interfaces
{
    public interface IEffect<TTarget>
    {
        void Apply(Unit source, TTarget target);
        void Cleanup();
        
    }
}