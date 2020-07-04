using System;
using TMPro;
using UnityEngine;

public class ScaleWeightBehaviour : MonoBehaviour
{
    private const string Suffix = " g";
    private float _weight = 0;
    private TMP_Text _weightText;

    public float Weight => _weight;
    
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
        _weight += weight;
        SetWeightText();
    }

    private void SetWeight(float weight)
    {
        _weight = weight;
        SetWeightText();
    }

    public void Reset()
    {
        SetWeight(0);
    }

    private void SetWeightText()
    {
        WeightText.text = $"{Weight}{Suffix}";
    }
}
