using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lean.Touch;
using UnityEngine;

public class CustomLeanSpawn : LeanSpawn
{
    public Transform _positionToSpawnFrom;

    [CanBeNull]
    public GameObject SpawnFromPosition()
    {
        if (Prefab != null)
        {
            var rotation = DefaultRotation == SourceType.Prefab ? Prefab.rotation : transform.rotation;
            var clone    = Instantiate(Prefab, _positionToSpawnFrom.position, rotation);

            clone.gameObject.SetActive(true);
            return clone.gameObject;
        }

        return null;
    }
}
