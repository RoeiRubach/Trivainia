using System;
using System.Collections.Generic;

namespace Trivainia
{
    public class AbilityModel
    {
        public event Action<List<Ability>> AbilityAdded = delegate { };
        public readonly List<Ability> Abilities = new();

        public bool TryGetAbility(int index, out Ability ability)
        {
            ability = index >= 0 && index < Abilities.Count ? Abilities[index] : null;

            return ability != null;
        }

        public void Add(Ability ability)
        {
            Abilities.Add(ability);
            AbilityAdded.Invoke(Abilities);
        }
    }
}