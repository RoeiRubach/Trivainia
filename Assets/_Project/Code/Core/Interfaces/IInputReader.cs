using System;
using UnityEngine;

namespace Trivainia
{
    public interface IInputReader
    {
        public void EnableActions();
        public Vector3 Direction { get; }
        public void RegisterSkillAction(int index, Action onButtonClicked);
    }
}