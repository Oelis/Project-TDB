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
using Unity.VisualScripting;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour 
    {
        private UnitBrain _Brain;
        
        public void Kill()
        {
            Destroy(gameObject);
        }

        public UnitBrain GetBrain()
        {
            return _Brain;
        }
        
        public void SetBrain(UnitBrain brain)
        {
            _Brain = brain;
        }
    }
    

    
}
