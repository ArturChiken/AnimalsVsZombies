using UnityEngine;

public class DiffMenuButtonsManager : MonoBehaviour
{
    [SerializeField] LevelSelectorManager.DiffContainerButtons _buttonType;
    public static int currDiff;

    public void ButtonClicked(int diffNum)
    {
        currDiff = diffNum;
        LevelSelectorManager._.DiffMenuButtonClicked(_buttonType);
    }
}
