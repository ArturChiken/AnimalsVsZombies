using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int lvlNum;

    private bool isActive = false;

    public LevelManager lvlManager;

    private void Start()
    {
        if (lvlNum == lvlManager.lvlOut)
        {
            isActive = true;
        }

        gameObject.SetActive(isActive);
    }
}