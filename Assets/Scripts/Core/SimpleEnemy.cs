using System.Collections.Generic;
using System.Linq;
using SimpleGame.Core.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleGame.Core
{
    /// <summary>
    /// This Very Simple Enemy Is Only For Testing
    /// </summary>
    public class SimpleEnemy : Damageable
    {
        #region Variables
        [SerializeField] private MovementController _movement;
        [SerializeField] private AttackController _attack;
        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;

        #endregion

        #region Monobehaviour Callbacks
        public override void Awake()
        {
            base.Awake();
        }

        private void FixedUpdate()
        {
            if(!IsAlive)
                return;

            if(Vector3.Distance(transform.position, _player.transform.position) < .2f)
            {
                _attack?.MeleeAttack(_player.transform.position);
                _movement?.Move(Vector2.zero);
            }
            else
            {
                _attack?.RangeAttack(_player.transform.position);
                _movement?.Move((_player.transform.position - transform.position).normalized);
            }

        }
        #endregion

        #region Overrided Methods
        public override void Damage(float damage)
        {
            if(!IsAlive)
                return; 

            base.Damage(damage);
            _animator.SetTrigger("hit");
        }
        public override void Death()
        {
            base.Death();
            _animator.SetTrigger("death");
            enabled = false;
            _movement?.Move(Vector2.zero);
        }
        #endregion
    }
}