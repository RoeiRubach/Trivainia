using System;
using UnityEngine;

namespace Trivainia
{
    public interface IInputReader
    {
        public void EnableActions();
        public Vector3 Direction { get; }
        public event Action<InputDeviceType> InputDeviceChanged;
        public void RegisterSkillAction(int index, Action onButtonClicked);
    }
}