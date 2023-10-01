using System;
using Objects;
using UnityEngine;

namespace Player
{
    public sealed class PlayerCollector : MonoBehaviour
    {
        public event Func<a_Collectable, bool> TriedToCollectMana;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out a_Collectable collectable))
                return;

            TryCollect(collectable);
        }

        private void TryCollect(a_Collectable collectable)
        {
            if (collectable is ManaCollectable)
            {
                if (TriedToCollectMana.Invoke(collectable))
                {
                    collectable.Collect();
                }

                return;
            }

            if (collectable is BonusCollectable)
            {
                collectable.Collect();
            }
        }
    }
}