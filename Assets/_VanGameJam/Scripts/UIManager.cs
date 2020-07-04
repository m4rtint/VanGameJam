using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScaleWeightBehaviour _scaleWeightBehaviourManager = null;
    [SerializeField] private LosePanelBehaviour _losePanel = null;
    [SerializeField] private MenuScreenBehaviour _menuPanel = null;

    [Header("Components To Animate")] public MenuTitleBehaviour _title = null;
    public event Action OnPlayGame;

    private ScaleWeightBehaviour ScaleWeightBehaviourManager
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
        _losePanel.Initialize();
        AddPlayButtonListener();
    }
    
    public void Reset()
    {
        ScaleWeightBehaviourManager.Reset();
        _menuPanel.Reset();
    }

    public void UpdateWeight(float weight)
    {
        ScaleWeightBehaviourManager.AddWeight(weight);
    }

    public void LoseGame()
    {    
        _losePanel.ShowPanel();
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
        _losePanel.HidePanel();
        if (OnPlayGame != null)
        {
            OnPlayGame();
        }
    }
}
