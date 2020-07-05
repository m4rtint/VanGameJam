using UnityEngine;

public class BaseWorldManager : MonoBehaviour
{
    [SerializeField] private FoodGeneratorBehaviour _foodGenerator = null;
    [SerializeField] private UIManager _uiManager = null;
    [SerializeField] private DeathBoxBehaviour _deathBox = null;
    private bool _isGameInSession = false;
    
    private void OnEnable()
    {
        _uiManager.Initialize();
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
        _uiManager.OnPlayGame += StartGame;
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
        if (_isGameInSession)
        {
            _isGameInSession = false;
            AudioManager.Instance.PlayGameLose();
            _uiManager.LoseGame();
            _foodGenerator.LoseGame();
        }
    }

    private void StartGame()
    {
        _uiManager.Initialize();
        _foodGenerator.Initialize();
        _isGameInSession = true;
    }
}
