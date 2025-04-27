using System.Collections.Generic;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "AbilitiesInputImagesSO", menuName = "Scriptable Objects/AbilitiesInputImagesSO")]
    public class AbilitiesInputImagesSO : ScriptableObject
    {
        public Sprite SkillBottom;
        public Sprite SkillLeft;
        public Sprite SkillUp;
        public Sprite SkillRight;

        public IEnumerable<Sprite> GetImagesClockwiseFromBottom()
        {
            yield return SkillBottom;
            yield return SkillLeft;
            yield return SkillUp;
            yield return SkillRight;
        }
    }
}