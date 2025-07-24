using NUnit.Framework.Internal;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New LevelType", menuName = "Level")] 
public class LevelType : ScriptableObject
{
    //LabubuSpawner
    public int labubuMax = 5;
    public int labubuDelay = 10;
    public int labubuSpawnTime = 2;
    //5050
    public LabubuType[] labubuTypes;

    //Gamemanager
    public int coffees = 200;

    //SceneManager
    //public Scene scene;
}
