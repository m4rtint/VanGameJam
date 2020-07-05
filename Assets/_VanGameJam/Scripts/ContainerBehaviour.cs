using System;
using DG.Tweening;
using JetBrains.Annotations;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

public class ContainerBehaviour : MonoBehaviour
{
    private const float AnimationTime = 0.5f;
    
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
        PlaySetFoodSFX();
    }

    public void Reset()
    {
        _currentFood = null;
        _foodSpawned = null;
        FoodImage.transform.localScale = Vector3.zero;
        RemoveFoodSpawnDelegate();
        RemoveImageFromContainer();
    }

    public bool IsFoodEmpty()
    {
        return _foodImage.sprite == null && _foodSpawned.HasMoved();
    }

    public void Initialize()
    {
        ContainerCustomLeanSpawn.Reset();
        TurnTouch(true);
        ContainerLeanSelectable.OnSelect.RemoveAllListeners();
        ContainerLeanSelectable.OnSelect.AddListener(HandleFingerDown);
    }

    public void OnGameLost()
    {
        TurnTouch(false);
    }

    private void PlaySetFoodSFX()
    {
        AudioManager.Instance.PlayFillContainerWithFood();
    }
    
    private void UpdateFoodImage()
    {
        if (_currentFood.HasValue)
        {
            FoodImage.sprite = _currentFood.Value.FoodImage;
            AnimateFillFood();
        }
        else
        {
            RemoveImageFromContainer();
        }
    }

    private void AnimateFillFood()
    {
        TurnTouch(true);
        FoodImage.transform.DOScale(Vector3.one, AnimationTime).SetEase(Ease.OutBack);
    }

    private void AnimateEmptyFood()
    {
        FoodImage.transform.DOScale(Vector3.zero, AnimationTime).SetEase(Ease.InBack).OnComplete(() =>
        {
            FoodImage.sprite = null;
        });
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        if (FoodImage.sprite != null)
        {
            PlayTouchContainerSFX();
            SpawnFood();
            RemoveImageFromContainer();
            TurnTouch(false);
        }
    }

    private void TurnTouch(bool on)
    {
        ContainerLeanSelectable.enabled = on;
    }

    private void PlayTouchContainerSFX()
    {
        AudioManager.Instance.PlayContainerClicked();
    }

    private void RemoveImageFromContainer()
    {
        AnimateEmptyFood();
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
            RemoveFoodSpawnDelegate();
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
    }

    private void ContainerOnFoodStoppedMoving()
    {
        if (_currentFood.HasValue && OnFoodStoppedMoving != null)
        {
            RemoveFoodSpawnDelegate();
            OnFoodStoppedMoving(_currentFood.Value.Weight);
        }
    }

}
