using NUnit.Framework.Internal.Filters;
using UnityEngine;
using UnityEngine.Rendering;

public class Tile : MonoBehaviour
{
    public bool hasAnimal = false;
    public int[] actNums;
    public int[] lvlNums;

    private void Start()
    {
        gameObject.SetActive(true);

        foreach (int act in actNums)
        {
            if (act == ActMenuButtonsManager.currAct)
            {
                foreach(int lvl in lvlNums)
                {
                    if (lvl == LevelMenuButtonManager.currLevel)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
