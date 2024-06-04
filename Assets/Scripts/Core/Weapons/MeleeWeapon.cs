using Unity.VisualScripting;
using UnityEngine;

namespace SimpleGame.Core.Weapons
{
    /// <summary>
    /// For inheriting in any type of MeleeWeapon classes
    /// </summary>
    public class MeleeWeapon : Weapon
    {
        #region Variables
        [SerializeField] private float _angle;
        [SerializeField] private Animator _animator;
        #endregion

        #region Overrided Methods
        public override bool Attack(Vector3 point)
        {
            if (!base.Attack(point))
                return false;

            _animator.SetTrigger("meleeAttack");

            Vector2 distance = point - transform.position;
            var overlapResult = Physics2D.OverlapCircleAll(transform.position, Range, TargetMask);
            CheckAndApplyDamage(overlapResult, distance.normalized);

            return true;
        }

        private void CheckAndApplyDamage(Collider2D[] overlapHits,Vector2 direction)
        {
            foreach(var hit in overlapHits)
            {
                Vector2 distanceTarget = hit.transform.position - transform.position;

                if(Vector2.Angle(distanceTarget.normalized, direction) < _angle / 2)
                {
                    IDamageable target;
                    if(hit.TryGetComponent(out target))
                        target.Damage(4);
                }
            }
        }
        #endregion
    }
}