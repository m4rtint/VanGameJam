using UnityEngine;

public class MenuScreenBehaviour : BaseSimplePanel
{
    public void HidePanel()
    {
        ReplayButton.transform.localScale = Vector3.zero;
    }
}
