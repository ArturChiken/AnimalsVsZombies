using UnityEngine;

public class CreditsButtonManager : MonoBehaviour
{
    [SerializeField] MainMenuManager.CreditsButtons _buttonType;
    public void ButtonClicked()
    {
        MainMenuManager._.CreditsButtonClicked(_buttonType);
    }
}
