using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScaleWeightBehaviour _scaleWeightBehaviourManager = null;
    
    public ScaleWeightBehaviour ScaleWeightBehaviourManager
    {
        get
        {
            if (_scaleWeightBehaviourManager == null)
            {
                _scaleWeightBehaviourManager = GetComponentInChildren<ScaleWeightBehaviour>();
            }

            return _scaleWeightBehaviourManager;
        }
    }

    public void Initialize()
    {
        ScaleWeightBehaviourManager.Initialize();
    }
    
    public void Reset()
    {
        ScaleWeightBehaviourManager.Reset();
    }

    public void UpdateWeight(float weight)
    {
        ScaleWeightBehaviourManager.AddWeight(weight);
    }
}
