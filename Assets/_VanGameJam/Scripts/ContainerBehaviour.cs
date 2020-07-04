using System;
using System.ComponentModel;
using Lean.Touch;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ContainerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image _foodImage = null;
    private Food? _currentFood = null;
    
    // Lean Dependencies
    private LeanSelectable _containerLeanSelectable = null;

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
    public void SetFood(Food food)
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
            GetComponent<LeanSpawn>().Prefab = _currentFood.Value.FoodObject.transform;
            GetComponent<LeanSpawn>().Spawn();
        }
    }
}
