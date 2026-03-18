using System.Collections.Generic;
using UnityEngine;

namespace Policies
{
    public class CritPolicy
    {
        public float baseRate;
        private static readonly Dictionary<float, CritPolicy> cache = new();
    
        private CritPolicy(float baseRate) => this.baseRate = baseRate;

        public float Chance(float luck) => Mathf.Clamp01(baseRate * luck);
    
        public bool Roll(float luck)=>UnityEngine.Random.value < Chance(luck);

        public static CritPolicy GetOrCreate(float baseRate = 0.05f)
        {
            if (!cache.TryGetValue(baseRate, out CritPolicy policy))
            {
                policy = new CritPolicy(baseRate);
                cache[baseRate] = policy;
            }
            return policy;
        }

    }
}


