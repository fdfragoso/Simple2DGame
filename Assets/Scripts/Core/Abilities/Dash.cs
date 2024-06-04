using System.Collections;
using UnityEngine;

namespace SimpleGame.Core.Abilities
{
    /// <summary>
    /// Dash Ability
    /// </summary>
    public class Dash : Ability
    {
        #region Variables
        [SerializeField] private float _dashSpeed;
        [SerializeField] private MovementController _movement;
        [SerializeField] private TrailRenderer _trail;
        #endregion

        #region Monobehaviour Callbacks
        public override void Awake()
        {
            base.Awake();

            _trail.emitting = false;
        }
        #endregion

        #region Overrided Methods
        public override void Execute()
        {
            base.Execute();

            StartCoroutine(Dashing());
        }
        #endregion

        #region Coroutines
        IEnumerator Dashing()
        {
            _trail.emitting = true;
            OnDeActivated?.Invoke();
            var baseSpeed = _movement.Speed;
            _movement.SetSpeed(baseSpeed * _dashSpeed);
            yield return new WaitForSeconds(Duration);
            _movement.SetSpeed(baseSpeed);
            StopExecution();
            _trail.emitting = false;
            yield return new WaitForSeconds(Cooldown - Duration);
            IsActive = true;
            OnActivated?.Invoke();
        }
        #endregion
    }
}