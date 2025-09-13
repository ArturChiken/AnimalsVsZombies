using UnityEngine;

public class LeaderboardButtonsManager : MonoBehaviour
{
    [SerializeField] LeaderboardManager.LeaderboardContainerButtons _buttonType;

    public void ButtonClicked()
    {
        LeaderboardManager._.LeaderboardContainerButtonsClicked(_buttonType);
    }
}
