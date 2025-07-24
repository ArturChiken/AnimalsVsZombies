using NUnit.Framework.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelsType", menuName = "Level")] 
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
    //public int Scene scene;
}
