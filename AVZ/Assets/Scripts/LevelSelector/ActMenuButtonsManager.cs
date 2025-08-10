using UnityEngine;

public class ActMenuButtonsManager : MonoBehaviour
{
    [SerializeField] LevelSelectorManager.ActContainerButtons _buttonType;
    public static int currAct;

    public void ButtonClicked(int actNum)
    {
        currAct = actNum;
        LevelSelectorManager._.ActMenuButtonClicked(_buttonType);
    }
}
