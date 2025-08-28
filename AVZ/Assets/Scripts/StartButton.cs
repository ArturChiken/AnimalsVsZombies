using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Gamemanager gameManager;
    private Animator animator;
    public Transform cardSelector;
    //public Transform pauseButton;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void StartGame()
    {
        if (gameManager.cardAmount > 0)
        {
            gameManager.isGameStarted = true;

            cardSelector.gameObject.SetActive(false);
            gameObject.SetActive(false);
            //pauseButton.gameObject.SetActive(True);

            animator.SetTrigger("StartGame");
        }
        else
        {
            Debug.Log("Need one or more animals!");
        }
    }
}
