using UnityEngine;

public class BaseWorldManager : MonoBehaviour
{
    [SerializeField] private FoodGeneratorBehaviour _foodGenerator = null;
    [SerializeField] private UIManager _uiManager = null;
    
    private void OnEnable()
    {
        _uiManager.Initialize();
        _foodGenerator.Initialize();
        AddDelegate();
    }

    private void OnDisable()
    {
        _uiManager.Reset();
        _foodGenerator.Reset();
        RemoveDelegate();
    }

    private void AddDelegate()
    {
        _foodGenerator.OnFoodWeightCounted += CountWeight;
    }

    private void RemoveDelegate()
    {
        _foodGenerator.OnFoodWeightCounted -= CountWeight;
    }

    private void CountWeight(float weight)
    {
        _uiManager.UpdateWeight(weight);
    }
}
