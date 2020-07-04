using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lean.Touch;
using UnityEngine;

public class CustomLeanSpawn : LeanSpawn
{
    public Transform _positionToSpawnFrom = null;
    public List<GameObject> _spawnedFood = null;

    [CanBeNull]
    public GameObject SpawnFromPosition()
    {
        if (Prefab != null)
        {
            var rotation = DefaultRotation == SourceType.Prefab ? Prefab.rotation : transform.rotation;
            var clone    = Instantiate(Prefab, _positionToSpawnFrom.position, rotation);
            _spawnedFood.Add(clone.gameObject);
            clone.gameObject.SetActive(true);
            return clone.gameObject;
        }

        return null;
    }

    public void Reset()
    {
        foreach (var child in _spawnedFood)
        {
            Destroy(child);
        }
    }
}
