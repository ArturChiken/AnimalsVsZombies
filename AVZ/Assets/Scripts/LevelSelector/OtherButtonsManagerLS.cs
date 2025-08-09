using UnityEngine;

public class OtherButtonsManagerLS : MonoBehaviour
{ 
    [SerializeField] LevelSelectorManager.OtherButtons _buttonType;
    public void ButtonClicked()
    {
        LevelSelectorManager._.OtherButtonsClicked(_buttonType);
    }
}

