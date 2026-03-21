using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abilities;
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

        private void Awake()
        {
            Registry<Unit>.TryAdd(this);
        }
        
        public void Kill()
        {
            Registry<Unit>.Remove(this);
            Destroy(gameObject);
        }
        
        public void SetBrain(UnitBrain brain)
        {
            _Brain = brain;
        }
        
        public UnitBrain GetBrain()
        {
            return _Brain;
        }
    }
    

    
}
