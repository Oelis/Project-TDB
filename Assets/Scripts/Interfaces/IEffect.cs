using System;
using Units;

namespace Interfaces
{
    public interface IEffect
    {
        void Apply(UnitBrain source, UnitBrain target);
        void Cleanup();
        
    }
}