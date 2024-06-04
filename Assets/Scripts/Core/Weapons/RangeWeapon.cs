using UnityEngine;

namespace SimpleGame.Core.Weapons
{
    /// <summary>
    ///  For inheriting in any type of RangeWeapon classes.
    /// </summary>
    public class RangeWeapon : Weapon
    {
        #region Variables
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private Transform _shootOrigin;
        #endregion

        #region Overrided Methods
        public override bool Attack(Vector3 point)
        {
            if(!base.Attack(point))
                return false;

            Vector2 distance = point - transform.position;
            var bullet = Instantiate(_bulletPrefab, _shootOrigin.position, Quaternion.identity);
            bullet.Init(this, distance.normalized, _bulletSpeed);

            return true;
        }
        #endregion
    }
}