using UnityEngine;

public class LoseScreenButtonsManager : MonoBehaviour
{
    [SerializeField] Gamemanager.LoseScreenContainer _buttonType;

    public void ButtonClicked()
    {
        Gamemanager._.LoseButtonClicked(_buttonType);
    }
}
