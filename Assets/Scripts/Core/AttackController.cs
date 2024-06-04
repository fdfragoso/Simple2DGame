using SimpleGame.Core.Weapons;
using UnityEngine;

namespace SimpleGame.Core
{
    /// <summary>
    /// For calling melee and range attacks
    /// </summary>
    public class AttackController : MonoBehaviour
    {
        #region Variables
        [HideInInspector] public bool IsEnabled = true;

        [SerializeField] private MeleeWeapon _melee;
        [SerializeField] private RangeWeapon _range;
        #endregion

        #region Methods
        public bool MeleeAttack(Vector3 point) => _melee.Attack(point);

        public bool RangeAttack(Vector3 point) => _range.Attack(point);

        #endregion
    }
}