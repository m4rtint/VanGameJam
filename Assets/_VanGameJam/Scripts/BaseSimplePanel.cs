using UnityEngine;
using UnityEngine.UI;

public class BaseSimplePanel : MonoBehaviour
{
    [SerializeField]
    private Button _replayButton = null;

    public Button ReplayButton
    {
        get
        {
            if (_replayButton == null)
            {
                //There's only one button in this panel right now.
                _replayButton = GetComponentInChildren<Button>();
            }

            return _replayButton;
        }
    }
}
