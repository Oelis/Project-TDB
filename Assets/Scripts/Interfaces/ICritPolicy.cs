namespace Interfaces
{
    public interface ICritPolicy
    {
        float Chance(float luck, float mult);
        bool Roll(float luck, float mult);
    }
    
}