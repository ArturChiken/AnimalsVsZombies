using UnityEngine;

public class OtherButtonsManager : MonoBehaviour
{ 
    [SerializeField] MainMenuManager.OtherButtons _buttonType;
    public void ButtonClicked()
    {
        MainMenuManager._.OtherButtonsClicked(_buttonType);
    }
}

