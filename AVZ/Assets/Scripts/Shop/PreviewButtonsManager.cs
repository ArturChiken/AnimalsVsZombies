using UnityEngine;

public class PreviewButtonsManager : MonoBehaviour
{
    [SerializeField] ShopManager.PreviewContainerButtons _buttonType;
    public void ButtonClicked()
    {
        ShopManager._.PreviewContainerButtonsClicked(_buttonType);
    }
}
