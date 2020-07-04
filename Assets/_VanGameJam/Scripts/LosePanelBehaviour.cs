using DG.Tweening;
using UnityEngine;

public class LosePanelBehaviour : BaseSimplePanel
{
    public void Initialize()
    {
        HidePanel();
    }

    public void HidePanel()
    {
        transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack);
    }

    public void ShowPanel()
    {
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
    }
}
