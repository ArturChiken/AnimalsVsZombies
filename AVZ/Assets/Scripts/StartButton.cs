using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Gamemanager gameManager;
    public Transform cardSelector;
    public Animator animator;
    public Button ButtonStart;

    //public Transform pauseButton;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void Update()
    {
        if (gameManager.cardAmount > 0)
        {
            gameManager._speechBubbleCS.SetActive(false);
            ButtonStart.interactable = true;
        }
        else
        {
            gameManager._speechBubbleCS.SetActive(true);
            ButtonStart.interactable = false;
        }
    }

    private void StartGame()
    {
        if (gameManager.cardAmount > 0)
        {
            gameManager.isGameStarted = true;
            gameManager._speechBubbleCS.SetActive(false);
            cardSelector.gameObject.SetActive(false);
            gameObject.SetActive(false);
            gameManager._pauseButton.SetActive(true);
            gameManager.blurFrameInAnimalCardSelectorGO.SetActive(false);
            animator.Play("LabubuPreAni");
        }
    }
}
