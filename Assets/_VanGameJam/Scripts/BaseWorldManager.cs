using UnityEngine;

public class BaseWorldManager : MonoBehaviour
{
    [SerializeField] private FoodGeneratorBehaviour _foodGenerator = null;
    [SerializeField] private UIManager _uiManager = null;
    [SerializeField] private DeathBoxBehaviour _deathBox = null;
    
    private void OnEnable()
    {
        _uiManager.Initialize();
        _foodGenerator.Initialize();
        AddDelegate();
    }

    private void OnDisable()
    {
        if (_uiManager != null)
        {
            _uiManager?.Reset();
        }

        if (_foodGenerator != null)
        {
            _foodGenerator.Reset();
        }
        
        RemoveDelegate();
    }

    private void AddDelegate()
    {
        _foodGenerator.OnFoodWeightCounted += CountWeight;
        _deathBox.OnEnterDeathBox += OnFoodDeath;
        _uiManager.OnPlayGame += OnEnable;
    }

    private void RemoveDelegate()
    {
        _foodGenerator.OnFoodWeightCounted -= CountWeight;
        _deathBox.OnEnterDeathBox -= OnFoodDeath;
    }

    private void CountWeight(float weight)
    {
        _uiManager.UpdateWeight(weight);
    }

    private void OnFoodDeath()
    {
        _uiManager.LoseGame();
    }
}
