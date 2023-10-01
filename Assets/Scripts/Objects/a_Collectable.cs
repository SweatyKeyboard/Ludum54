using System;
using UnityEngine;

namespace Objects
{
    public abstract class a_Collectable : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        
        public event Action Collected;
        public abstract Sprite Sprite { get; }

        public void Collect()
        {
            Collected?.Invoke();
            Destroy(gameObject);
        }
    }
}