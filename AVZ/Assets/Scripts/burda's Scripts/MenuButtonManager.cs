using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] MainMenuManager.MenuButtons _buttonType;
    public void ButtonClicked()
    {
        MainMenuManager._.MenuButtonClicked(_buttonType);
    }
}
