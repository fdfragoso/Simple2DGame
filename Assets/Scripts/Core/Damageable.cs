using UnityEngine;
using UnityEngine.Events;

namespace SimpleGame.Core
{
   /// <summary>
   /// Damageable Base Class For Inheriting In Any Object That's Damageable
   /// </summary>
    public class Damageable : MonoBehaviour, IDamageable
    {
        #region Variables & Events
        private float _remainingHP;
        [SerializeField] private float _health;
        [SerializeField] private UnityEvent<float> _onDamaged;
        [SerializeField] private UnityEvent _onDeath;

        public float Health { get => _health; set => _health = value; }
        public UnityEvent<float> OnDamaged => _onDamaged; 
        public UnityEvent OnDeath  => _onDeath;

        public bool IsAlive => _remainingHP>0;
        #endregion

        #region Monobehaviour Callbacks
        public virtual void Awake()
        {
            _remainingHP = Health;
        }
        #endregion

        #region Virtual Methods
        public virtual void Damage(float damage)
        {
            _remainingHP -= damage;
            OnDamaged?.Invoke(_remainingHP);
            if(_remainingHP <= 0)
                Death();
        }

        public virtual void Death()
        {
            OnDeath?.Invoke();
        }
        #endregion
    }

    #region Interfaces
    public interface IDamageable
    {
        public float Health { get; set; }
        public bool IsAlive { get; }
        public void Damage(float damage);
        public UnityEvent<float> OnDamaged { get; }
        public UnityEvent OnDeath { get; }
    }
    #endregion 
}