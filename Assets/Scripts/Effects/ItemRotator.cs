using UnityEngine;

namespace SimpleGame.Effects
{
    public class ItemRotator : MonoBehaviour
    {
        [SerializeField]
        public Vector3 _rotationAmount;
        [SerializeField]
        public float _rotationSpeed;

        void Update()
        {
            transform.Rotate(_rotationAmount, _rotationSpeed * Time.deltaTime);
        }
    }
}