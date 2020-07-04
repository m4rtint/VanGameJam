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

    public event Action OnReleasedFood;

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

    private void UpdateFoodImage()
    {
        if (_currentFood.HasValue)
        {
            _foodImage.sprite = _currentFood.Value.FoodImage;
        }
        else
        {
            _foodImage.sprite = null;
        }
    }

    private void OnEnable()
    {
        ContainerLeanSelectable.OnSelect.RemoveAllListeners();
        ContainerLeanSelectable.OnSelect.AddListener(HandleFingerDown);
    }

    private void OnDisable()
    {
        ContainerLeanSelectable.OnSelect.RemoveAllListeners();
        FoodImage.sprite = null;
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        if (_currentFood.HasValue)
        {
            SpawnFood();
            RemoveFromContainer();
        }
    }

    private void RemoveFromContainer()
    {
        SetFood(null);
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
                _foodSpawned.OnReleasedFood += ContainerOnReleasedFood;
            }
        }
    }
    
    private void RemoveFoodSpawnDelegate()
    {
        if (_foodSpawned != null)
        {
            _foodSpawned.OnReleasedFood -= ContainerOnReleasedFood;
        }
            
        _foodSpawned = null;
    }

    private void ContainerOnReleasedFood()
    {
        if (OnReleasedFood != null)
        {
            RemoveFoodSpawnDelegate();
            OnReleasedFood();
        }
    }

}
