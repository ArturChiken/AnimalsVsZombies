using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] Button _nextLvlButton;
    [SerializeField] GameObject _woodFrame;

    private LevelSwitcher _levelSwitcherInstance;

    private void Awake()
    {
        if (LevelMenuButtonManager.currLevel % 10 == 0)
        {
            _nextLvlButton.interactable = false;
            _woodFrame.SetActive(true);
        }
        else
        {
            _nextLvlButton.interactable = true;
            _woodFrame.SetActive(false);
        }
    }
}
