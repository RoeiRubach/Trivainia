using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Trivainia
{
    [CreateAssetMenu(fileName = "AbilitiesInputImagesSO", menuName = "Scriptable Objects/AbilitiesInputImagesSO")]
    public class AbilitiesInputImagesSO : ScriptableObject
    {
        [Title("Clockwise Input Images (Starting from Bottom)")]
    
        [PreviewField(70), LabelText("Bottom")]
        [PropertySpace(SpaceBefore = 5, SpaceAfter = 5)]
        public Sprite SkillBottom;

        [PreviewField(70), LabelText("Left")]
        [PropertySpace(SpaceBefore = 5, SpaceAfter = 5)]
        public Sprite SkillLeft;

        [PreviewField(70), LabelText("Up")]
        [PropertySpace(SpaceBefore = 5, SpaceAfter = 5)]
        public Sprite SkillUp;

        [PreviewField(70), LabelText("Right")]
        [PropertySpace(SpaceBefore = 5, SpaceAfter = 10)]
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
