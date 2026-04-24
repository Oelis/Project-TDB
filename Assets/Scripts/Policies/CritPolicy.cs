using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Policies
{
    class LuckPolicy : ILuckPolicy
    {
        public int baseRate;
        static readonly Dictionary<float,LuckPolicy> cache = new ();
    
        private LuckPolicy(int baseRate) => this.baseRate = baseRate;
    
        public float Chance(int luck) => Mathf.Clamp(Mathf.Round(baseRate + luck),0,100);
        public bool  Roll(int luck) => Random.Range(0,100) < Chance(luck);

        public static LuckPolicy Create(int baseRate = 5)
        {
            if (!cache.TryGetValue(baseRate, out LuckPolicy policy))
            {
                policy = new LuckPolicy(baseRate);
                cache[baseRate] = policy;
            }

            return policy;
        }
    }
}




