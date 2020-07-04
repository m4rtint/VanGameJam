using System;
using JetBrains.Annotations;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

public class ContainerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image _foodImage = null;
    private Food? _currentFood = null;
    [CanBeNull] private FoodBehaviour _foodSpawned = null;
    
    // Lean Dependencies
    private LeanSelectable _containerLeanSelectable = null;
    private CustomLeanSpawn _containerCustomLeanSpawn = null;
    
    public event Action<float> OnFoodStoppedMoving;

    private CustomLeanSpawn ContainerCustomLeanSpawn
    {
        get
        {
            if (_containerCustomLeanSpawn == null)
            {
                _containerCustomLeanSpawn = GetComponent<CustomLeanSpawn>();
            }

            return _containerCustomLeanSpawn;
        }
    }

    private LeanSelectable ContainerLeanSelectable
    {
        get
        {
            if (_containerLeanSelectable == null)
            {
                _containerLeanSelectable = GetComponent<LeanSelectable>();
            }

            return _containerLeanSelectable;
        }
    }
    
    private Image FoodImage
    {
        get
        {
            if (_foodImage == null)
            {
                _foodImage = GetComponentInChildren<Image>();
            }

            return _foodImage;
        }
    }

    /// <summary>
    /// Puts the Food for this container
    /// </summary>
    /// <param name="food"></param>
    public void SetFood(Food? food)
    {
        _currentFood = food;
        UpdateFoodImage();
    }

    public void Reset()
    {
        _currentFood = null;
        _foodSpawned = null;
        RemoveFoodSpawnDelegate();
        RemoveImageFromContainer();
    }

    private void UpdateFoodImage()
    {
        if (_currentFood.HasValue)
        {
            FoodImage.sprite = _currentFood.Value.FoodImage;
        }
        else
        {
            FoodImage.sprite = null;
        }
    }

    public void Initialize()
    {
        ContainerCustomLeanSpawn.Reset();
        ContainerLeanSelectable.OnSelect.RemoveAllListeners();
        ContainerLeanSelectable.OnSelect.AddListener(HandleFingerDown);
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        if (FoodImage.sprite != null)
        {
            SpawnFood();
            RemoveImageFromContainer();
        }
    }

    private void RemoveImageFromContainer()
    {
        FoodImage.sprite = null;
    }

    private void SpawnFood()
    {
        ContainerCustomLeanSpawn.Prefab = _currentFood.Value.FoodObject.transform;
        var foodSpawned = ContainerCustomLeanSpawn.SpawnFromPosition();
        AddDelegate(foodSpawned);
    }

    private void AddDelegate(GameObject food)
    {
        if (food != null)
        {
            _foodSpawned = food.GetComponent<FoodBehaviour>();
            if (_foodSpawned != null)
            {
                _foodSpawned.OnFoodStoppedMoving += ContainerOnFoodStoppedMoving;
            }
        }
    }
    
    private void RemoveFoodSpawnDelegate()
    {
        if (_foodSpawned != null)
        {
            _foodSpawned.OnFoodStoppedMoving -= ContainerOnFoodStoppedMoving;
        }
            
        _foodSpawned = null;//
    }

    private void ContainerOnFoodStoppedMoving()
    {
        if (_currentFood.HasValue && OnFoodStoppedMoving != null)
        {
            OnFoodStoppedMoving(_currentFood.Value.Weight);
            RemoveFoodSpawnDelegate();
        }
    }

}
