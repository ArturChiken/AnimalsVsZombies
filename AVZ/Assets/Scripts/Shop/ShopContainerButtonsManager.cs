using UnityEngine;

public class ShopContainerButtonsManager : MonoBehaviour
{ 
    [SerializeField] ShopManager.ShopContainerButtons _buttonType;
    public void ButtonClicked()
    {
        ShopManager._.ShopContainerButtonsClicked(_buttonType);
    }
}

