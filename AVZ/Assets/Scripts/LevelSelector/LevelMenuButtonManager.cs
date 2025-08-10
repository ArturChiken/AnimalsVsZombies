using UnityEngine;

public class LevelMenuButtonManager : MonoBehaviour
{
    [SerializeField] LevelSelectorManager.LevelContainerButtons _buttonType;
    public static int currLevel;

    public void ButtonClicked(int levelNum)
    {
        currLevel = levelNum;
        LevelSelectorManager._.LevelsMenuButtonClicked(_buttonType);
    }
}
