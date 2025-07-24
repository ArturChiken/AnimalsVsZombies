using UnityEngine;

public class OptionsButtonManager : MonoBehaviour
{
    [SerializeField] MainMenuManager.OptionsButtons _buttonType;
    public void ButtonClicked()
    {
        MainMenuManager._.OptionsButtonClicked(_buttonType);
    }
}
