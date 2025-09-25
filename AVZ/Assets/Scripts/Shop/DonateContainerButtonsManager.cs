using UnityEngine;

public class DonateContainerButtonsManager : MonoBehaviour
{
    [SerializeField] ShopManager.DonateContainerButtons _buttonType;
    public void ButtonClicked()
    {
        ShopManager._.DonateContainerButtonsClicked(_buttonType);
    }
}
