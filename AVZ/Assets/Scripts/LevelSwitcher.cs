using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    public int actNum;
    public int lvlNum;

    private bool isActive = false;

    private void Start()
    {
        if (actNum == ActMenuButtonsManager.currAct)
        {
            if (lvlNum == LevelMenuButtonManager.currLevel)
            {
                isActive = true;
            }
        }
        gameObject.SetActive(isActive);
    }
}