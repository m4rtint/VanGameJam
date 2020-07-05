using TMPro;
using UnityEngine;

public class ScaleWeightBehaviour : MonoBehaviour
{
    //Animation
    private const float RollingSpeed = 0.05f;
    private const float GoUpBy = 50;
    private const string Suffix = " g";

    private float _rollingDuration = 0;
    private float _weight = 0;
    private float _destinationWeight = 0;
    private TMP_Text _weightText;
    

    private TMP_Text WeightText
    {
        get
        {
            if (_weightText == null)
            {
                _weightText = GetComponentInChildren<TMP_Text>();
            }

            return _weightText;
        }
    }

    public void Initialize()
    {
        Reset();
    }

    public void AddWeight(float weight)
    {
        _destinationWeight += weight;
    }

    public void Reset()
    {
        _destinationWeight = 0;
        _weight = 0;
        SetWeightText();
    }

    private void Update()
    {
        if (_destinationWeight > _weight)
        {
            _rollingDuration -= Time.deltaTime;
            if (_rollingDuration < 0)
            {
                _rollingDuration = RollingSpeed;
                if (_destinationWeight - _weight > GoUpBy)
                {
                    _weight += GoUpBy;
                }
                else
                {
                    _weight = _destinationWeight;
                }
            
                SetWeightText();
            }
        }
    }

    private void SetWeightText()
    {
        WeightText.text = $"{_weight}{Suffix}";
    }
}
