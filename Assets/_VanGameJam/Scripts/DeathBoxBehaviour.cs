using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DeathBoxBehaviour : MonoBehaviour
{
    public event Action OnEnterDeathBox;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OnEnterDeathBox != null)
        {
            OnEnterDeathBox();
        }
    }
}
