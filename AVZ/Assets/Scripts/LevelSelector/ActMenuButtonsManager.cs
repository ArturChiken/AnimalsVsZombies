using UnityEngine;

public class ActMenuButtonsManager : MonoBehaviour
{
    [SerializeField] LevelSelectorManager.ActContainerButtons _buttonType;
    
    public void ButtonClicked()
    {
        LevelSelectorManager._.ActMenuButtonClicked(_buttonType);
    }
}
