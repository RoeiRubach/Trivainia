using Trivainia;
using UnityEngine;

public class PlayerRotationApplier : MonoBehaviour
{
    [SerializeField] private PlayerServiceLocator _locator;
    private Transform _transform;

    private void Awake() => _transform = transform;

    private void Start() => _locator.MovementService.RotationComputed += HandleRotation;

    private void HandleRotation(Quaternion targetRotation) => _transform.rotation = targetRotation;
}