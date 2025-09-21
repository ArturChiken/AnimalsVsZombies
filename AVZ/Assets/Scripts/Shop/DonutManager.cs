using UnityEngine;
using YG;

public class DonutManager : MonoBehaviour
{
    // В гайде так было
    private void OnEnable()
    {
        YG2.onPurchaseSuccess += SuccessPurchased;
        YG2.onPurchaseFailed += FailedPurchased;
    }

    private void OnDisable()
    {
        YG2.onPurchaseSuccess -= SuccessPurchased;
        YG2.onPurchaseFailed -= FailedPurchased;
    }
    // честно хз(

    private void SuccessPurchased(string id)
    {
        switch(id)
        {
            case "100":
                YG2.saves.playerCoins += 100;
                break;
            case "250":
                YG2.saves.playerCoins += 250;
                break;
            case "500":
                YG2.saves.playerCoins += 500;
                break;
            case "1000":
                YG2.saves.playerCoins += 1000;
                break;
        }
        YG2.SaveProgress();
    }

    private void FailedPurchased(string id)
    {

    }
}
