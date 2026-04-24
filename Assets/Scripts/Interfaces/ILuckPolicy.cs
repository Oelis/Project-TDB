using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Interfaces
{
    public interface ILuckPolicy
    {
        float Chance(int luck);
        bool Roll(int luck);
    }
    
}

