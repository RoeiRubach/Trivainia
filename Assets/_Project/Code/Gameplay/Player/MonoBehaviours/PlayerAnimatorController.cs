using System.Collections.Generic;
using Trivainia.Utilities;
using UnityEngine;

namespace Trivainia
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        private const int DEFAULT_LAYER = 0;
        private const float TRANSITION_RATIO = 0.25f;
        private const float DEFAULT_FADE_DURATION = 0.25f;

        [SerializeField] private Animator _animator;

        private readonly HashSet<int> _validHashes = new();
        private readonly Dictionary<int, float> _clipLengths = new();

        private void Awake() => CacheClipLengths();

        public void RegisterCommand(IAnimationCommand command) => command.AnimationHashRaised += OnAnimationHashRaised;

        private void OnAnimationHashRaised(int animationHash)
        {
            if (!IsValidAnimationHash(animationHash))
                return;

            var fadeDuration = GetFadeDuration(animationHash);
            _animator.CrossFadeInFixedTime(animationHash, fadeDuration, DEFAULT_LAYER);
        }

        private bool IsValidAnimationHash(int animationHash)
        {
            if (_validHashes.Contains(animationHash))
                return true;

            if (!_animator.HasState(DEFAULT_LAYER, animationHash))
            {
                ConsoleLogger.PrintWarning($"Animator does not have state with hash {animationHash}");

                return false;
            }

            _validHashes.Add(animationHash);

            return true;
        }

        private float GetFadeDuration(int animationHash)
        {
            if (_clipLengths.TryGetValue(animationHash, out var clipLength))
                return clipLength * TRANSITION_RATIO;

            ConsoleLogger.PrintWarning($"Missing clip length for hash {animationHash}. Defaulting to 0.25s.");

            return DEFAULT_FADE_DURATION;
        }

        private void CacheClipLengths()
        {
            foreach (var clip in _animator.runtimeAnimatorController.animationClips)
            {
                var hash = Animator.StringToHash(clip.name);
                _clipLengths[hash] = clip.length;
            }
        }
    }
}