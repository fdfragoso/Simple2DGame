using System.Collections.Generic;
using System.Linq;
using SimpleGame.Core.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleGame.Core
{
    /// <summary>
    /// For handling user inputs and player damages
    /// </summary>
    public class Player : Damageable
    {
        #region Variables
        [SerializeField] private MovementController _movement;
        [SerializeField] private AttackController _attack;
        [SerializeField] private Animator _animator;

        private List<IAbility> _abilities;
        #endregion

        #region Monobehaviour Callbacks
        public override void Awake()
        {
            base.Awake();

            _abilities = GetComponentsInChildren<IAbility>(true).ToList();
        }
        #endregion

        #region Overrided Methods
        public override void Damage(float damage)
        {
            if(!IsAlive)
                return;

            var ability = _abilities?.Find(x => x.GetType() == typeof(Block));
            if(ability != null && ability.IsExecuting)
                return;

            base.Damage(damage);
            _animator.SetTrigger("hit");
        }
        public override void Death()
        {
            base.Death();
            _animator.SetTrigger("death");
            enabled = false;
        }
        #endregion

        #region Input System Callbacks
        private void OnMove(InputValue inputValue)
        {
            if(!IsAlive)
                return;

            var input = inputValue.Get<Vector2>();
            _movement?.Move(input);
        }

        private void OnAttack1(InputValue inputValue)
        {
            if(!IsAlive)
                return;

            if(_attack && _attack.IsEnabled)
            {
                var point = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _movement?.Look(point);
                _attack.MeleeAttack(point);
            }
        }

        private void OnAttack2(InputValue inputValue)
        {
            if(_attack && _attack.IsEnabled)
            {
                var point = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _movement?.Look(point);
                _attack.RangeAttack(point);
            }
        }

        private void OnDash(InputValue inputValue)
        {
            if(!IsAlive)
                return;

            var ability = _abilities?.Find(x => x.GetType() == typeof(Dash));
            if(ability != null && ability.IsActive)
                ability.Execute();
        }

        private void OnBlock(InputValue inputValue)
        {
            if(!IsAlive)
                return;

            var ability = _abilities?.Find(x => x.GetType() == typeof(Block));
            if(ability != null && ability.IsActive)
                ability.Execute();
        }
        #endregion
    }
}