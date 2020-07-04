using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MenuTitleBehaviour : MonoBehaviour
{
    [SerializeField] private float AlphaEndValue = 0.3f;
    [SerializeField] private float AnimationDurationRate = 2f;
    
    private CanvasGroup _titleCanvasGroup = null;
    private CanvasGroup TitleCanvasGroup
    {
        get
        {
            if (_titleCanvasGroup == null)
            {
                _titleCanvasGroup = GetComponent<CanvasGroup>();
            }

            return _titleCanvasGroup;
        }
    }
    public void AlphaOut()
    {
        TitleCanvasGroup.DOFade(AlphaEndValue, AnimationDurationRate);
    }
}
