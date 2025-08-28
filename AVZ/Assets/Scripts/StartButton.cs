using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Gamemanager gameManager;
    public Transform cardSelector;
    public Animator animator;

    //public Transform pauseButton;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
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

            animator.Play("LabubuPreAni");
        }
        else
        {
            Debug.Log("Need one or more animals!");
        }
    }
}
