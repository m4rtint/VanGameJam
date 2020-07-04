using DG.Tweening;
using UnityEngine;

public class MenuScreenBehaviour : BaseSimplePanel
{
    public void HidePanel()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBack);
    }
}
