using UnityEngine;
using YG;

public class AdvInLB : MonoBehaviour
{
    private float _advChance;
    void Start()
    {
        _advChance = Random.Range(0f, 1f);
        if (_advChance <= .35f)
        {
            YG2.InterstitialAdvShow();
        }
    }
}
