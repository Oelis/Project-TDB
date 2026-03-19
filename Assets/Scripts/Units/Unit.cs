using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities;
using Abilities.AbilityEffects;
using Attributes;
using Enums;
using Interfaces;
using Sirenix.OdinInspector;
using Stats;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour 
    {
        public void Kill()
        {
            Destroy(gameObject);
        }
    }
    
    public class Unit<TBrain> : Unit where TBrain : UnitBrain
    {
        public TBrain _unitBrain;
    }
    
    

    
}
