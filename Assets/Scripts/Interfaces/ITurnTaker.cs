using System;

namespace Interfaces
{
    public interface ITurnTaker
    {
        public float SpeedBarValue {get; set;}
        
        public event Action OnTurnStart;
        public event Action OnTurnEnd;
    }
}