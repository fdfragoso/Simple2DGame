using System.Collections;
using UnityEngine;

namespace SimpleGame.Core.Abilities
{
    /// <summary>
    /// Block Ability
    /// </summary>
    public class Block : Ability
    {
        #region Variables
        [SerializeField] private AttackController _attackController;
        [SerializeField] private SpriteRenderer _visual;
        #endregion

        #region Overrided Methods
        public override void Execute()
        {
            base.Execute();
            _attackController.IsEnabled = false;
            StartCoroutine(Blocking());
        }

        public override void StopExecution()
        {
            base.StopExecution();
            _attackController.IsEnabled = true;
        }
        #endregion

        #region Coroutines
        IEnumerator Blocking()
        {
            OnDeActivated?.Invoke();
            var wait = new WaitForEndOfFrame();
            var remainingTime = Duration;
            while(remainingTime>0) 
            {
                remainingTime -= Time.deltaTime;
                var color = _visual.color;
                color.a = remainingTime / Duration;
                _visual.color = color;
                yield return wait;
            }
            StopExecution();
            yield return new WaitForSeconds(Cooldown - Duration);
            IsActive = true;
            OnActivated?.Invoke();
        }
        #endregion
    }
}