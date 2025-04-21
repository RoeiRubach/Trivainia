using System;
using System.Collections;
using UnityEngine;

namespace Trivainia
{
    public class AlwaysFaceMainCamera : MonoBehaviour
    {
        private Transform _mainCamera;

        private IEnumerator Start()
        {
            _mainCamera = FindAnyObjectByType<PlayerServiceLocator>().MainCamera;

            while (true)
            {
                var direction = transform.position - _mainCamera.transform.position;
                transform.rotation = Quaternion.LookRotation(direction);

                yield return null;
            }
        }
    }
}