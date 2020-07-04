using UnityEngine;
using UnityEngine.UI;

public class ContainerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image _foodImage = null;
    private Food? _currentFood = null;
    
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
    
    private void OnDisable()
    {
        FoodImage.sprite = null;
    }
}
