using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] int gameSceneIndex = 1;

    public void AdvPlayButton()
    {
        SceneSwitch(gameSceneIndex);
    }

    public void InfPlayButton()
    {
        SceneSwitch(gameSceneIndex);
    }

    public void SceneSwitch(int index)
    {
        SceneManager.LoadScene(index);
    }
}
