using UnityEngine;

public class ActManager : MonoBehaviour
{
    public int actNum;

    private bool isActive = false;

    public LevelManager lvlManager;

    private void Start()
    {
        if (actNum == lvlManager.actOut)
        {
            isActive = true;
        }

        gameObject.SetActive(isActive);
    }
}
