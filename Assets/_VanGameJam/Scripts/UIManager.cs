using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScaleWeightBehaviour _scaleWeightBehaviourManager = null;
    [SerializeField] private LosePanelBehaviour _losePanel = null;
    [SerializeField] private MenuScreenBehaviour _menuPanel = null;

    [Header("Components To Animate")] public MenuTitleBehaviour _title = null;
    public event Action OnPlayGame;
    
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
        _losePanel.Reset();
        AddPlayButtonListener();
    }
    
    public void Reset()
    {
        ScaleWeightBehaviourManager.Reset();
        _losePanel.Reset();
        _menuPanel.Reset();
    }

    public void UpdateWeight(float weight)
    {
        ScaleWeightBehaviourManager.AddWeight(weight);
    }

    public void LoseGame()
    {    
        _losePanel.gameObject.SetActive(true);
    }

    private void AddPlayButtonListener()
    {
        _menuPanel.ReplayButton.onClick.RemoveAllListeners();
        _menuPanel.ReplayButton.onClick.AddListener(OnClickPlayButton);

        _losePanel.ReplayButton.onClick.RemoveAllListeners();
        _losePanel.ReplayButton.onClick.AddListener(OnClickReplayButton);
    }

    private void OnClickPlayButton()
    {
        AnimateOnPlay();
        OnClickReplayButton();
    }

    private void AnimateOnPlay()
    {
        _menuPanel.HidePanel();
        _title.AlphaOut();
    }

    private void OnClickReplayButton()
    {
        if (OnPlayGame != null)
        {
            OnPlayGame();
        }
    }
}
