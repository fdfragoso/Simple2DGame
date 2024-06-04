using UnityEngine;

namespace SimpleGame.Core.Weapons
{
    /// <summary>
    /// For using as bullet and inheriting in any kind of bullet classes.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        #region Variables
        [HideInInspector] public IWeapon Owner;
        [SerializeField] private Rigidbody2D _rigidBody;
        private Vector2 _direction;
        private float _speed;
        private Vector2 _startPoint;
        #endregion

        #region Monobehaviour Callbacks
        private void FixedUpdate()
        {
            BulletUpdate();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(Owner != null)
            {
                Damageable target;
                if(collision.gameObject.TryGetComponent(out target))
                {
                    target.Damage(Owner.Damage);
                    Owner.OnHit?.Invoke();
                }
            }
            Destroy(gameObject);
        }
        #endregion

        #region Methods
        public void Init(IWeapon owner, Vector2 direction, float speed)
        {
            Owner = owner;
            _direction = direction;
            _speed = speed;
            _startPoint = transform.position;
        }
        private void BulletUpdate()
        {
            _rigidBody.velocity = _direction * _speed * Time.deltaTime;
            if(Vector2.Distance(_startPoint, transform.position) >= Owner.Range)
                Destroy(gameObject);
        }
        #endregion
    }
}