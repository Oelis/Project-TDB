using Units;

namespace Interfaces
{
    public interface IInstantEffect
    {
        void OnApply(UnitBrain source, UnitBrain target);
        void OnRemove();
    }
}
