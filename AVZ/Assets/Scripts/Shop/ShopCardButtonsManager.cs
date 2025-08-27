using UnityEngine;

public class ShopCardButtonsManager : MonoBehaviour
{ 
    [SerializeField] PreviewScriptableObject _thisPreview;
    public void ButtonClicked()
    {
        ShopManager._.ChangePreviewSO(_thisPreview);
    }
}

