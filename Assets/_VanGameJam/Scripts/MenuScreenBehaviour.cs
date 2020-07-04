using DG.Tweening;
using UnityEngine;

public class MenuScreenBehaviour : BaseSimplePanel
{
    public void Initialize()
    {
        ReplayButton.transform.DOScale(Vector3.one * 1.3f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
    
    public void HidePanel()
    {
        ReplayButton.transform.DOKill();
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBack);
    }

    public void Reset()
    {
        ReplayButton.transform.DOKill();
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
}
