using UnityEngine.Events;
using UnityEngine;

namespace SimpleGame.Core.Abilities
{
    /// <summary>
    /// For inheriting in any type of abilities
    /// </summary>
    public class Ability : MonoBehaviour, IAbility
    {
        #region Variables & Events
        [SerializeField] private float _duration;
        [SerializeField] private float _cooldown;
        [SerializeField] private UnityEvent _onActivated;
        [SerializeField] private UnityEvent _onDeActivated;
        [SerializeField] private UnityEvent _onExecuted;
        [SerializeField] private UnityEvent _onStoped;
        public float Duration { get => _duration; set => Duration = value; }
        public float Cooldown { get => _cooldown; set => _cooldown = value; }

        public UnityEvent OnExecuted => _onExecuted;
        public UnityEvent OnStoped => _onStoped;
        public UnityEvent OnActivated => _onActivated;
        public UnityEvent OnDeActivated => _onDeActivated;

        public bool IsActive { get; set; }
        public bool IsExecuting { get; set; }
        #endregion

        #region Monobehaviour Callbacks
        public virtual void Awake()
        {
            IsActive = true;
        }
        #endregion

        #region Virtual Methods
        public virtual void Execute()
        {
            if(!gameObject.activeInHierarchy)
                return;

            IsActive = false;
            IsExecuting = true;
            OnExecuted?.Invoke();
        }

        public virtual void StopExecution()
        {
            IsExecuting = false;
            OnStoped?.Invoke();
        }
        #endregion
    }

    #region Interfaces
    public interface IAbility
    {
        public float Duration { get; set; }
        public float Cooldown { get; set; }
        public bool IsActive { get; set; }
        public bool IsExecuting { get; set; }
        public UnityEvent OnExecuted { get; }
        public UnityEvent OnStoped { get; }
        public UnityEvent OnActivated { get; }
        public UnityEvent OnDeActivated { get; }

        public void Execute();
        public void StopExecution();
    }
    #endregion
}