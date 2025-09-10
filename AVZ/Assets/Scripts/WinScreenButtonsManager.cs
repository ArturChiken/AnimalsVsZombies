using UnityEngine;

public class WinScreenButtonsManager : MonoBehaviour
{
    [SerializeField] Gamemanager.WinScreenContainer _buttonType;

    public void ButtonClicked()
    {
        Gamemanager._.WinButtonClicked(_buttonType);
    }
}
