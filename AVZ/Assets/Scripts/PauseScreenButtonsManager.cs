using UnityEngine;

public class PauseScreenButtonsManager : MonoBehaviour
{
    [SerializeField] Gamemanager.PauseScreenContainer _buttonType;

    public void ButtonClicked()
    {
        Gamemanager._.PauseButtonClicked(_buttonType);
    }
}
