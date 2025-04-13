using System;
using System.Collections.Generic;

namespace Trivainia
{
    public class AbilityModel
    {
        public event Action<List<Ability>> AbilityAdded = delegate { };
        public readonly List<Ability> Abilities = new();

        public void Add(Ability ability)
        {
            Abilities.Add(ability);
            AbilityAdded.Invoke(Abilities);
        }
    }
}