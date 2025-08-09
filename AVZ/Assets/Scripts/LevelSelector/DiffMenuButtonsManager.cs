using UnityEngine;

public class DiffMenuButtonsManager : MonoBehaviour
{
    [SerializeField] LevelSelectorManager.DiffContainerButtons _buttonType;

    public void ButtonClicked()
    {
        LevelSelectorManager._.DiffMenuButtonClicked(_buttonType);
    }
}
