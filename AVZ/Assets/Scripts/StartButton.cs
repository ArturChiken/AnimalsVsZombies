using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    AudioManager audioManager;
    private Gamemanager gameManager;
    public Transform cardSelector;
    public Animator animator, _StartButtonAnim, _SpeechbubbleAnim, _CardSelectorAnim;
    public Button ButtonStart;

    [SerializeField] CanvasGroup Canvas;

    //public Transform pauseButton;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
        gameManager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            StartCoroutine(GameStartAnimation());
            audioManager.PlaySFX(audioManager.buttonClicked);
            audioManager.SwitchMusic(audioManager.inGameMusic);
        }
    }

    public IEnumerator GameStartAnimation()
    {
        Canvas.interactable = false;
        Canvas.blocksRaycasts = false;
        gameManager._speechBubbleCS.SetActive(false);
        gameManager.blurFrameInAnimalCardSelectorGO.SetActive(false);
        _StartButtonAnim.Play("StartButtonRight");
        _CardSelectorAnim.Play("AnimalSLeft");
        animator.Play("LabubuPreAni");
        yield return new WaitForSeconds(_CardSelectorAnim.GetCurrentAnimatorStateInfo(0).length);
        cardSelector.gameObject.SetActive(false);
        gameObject.SetActive(false);
        gameManager._pauseButton.SetActive(true);
        Canvas.interactable = true;
        Canvas.blocksRaycasts = true;
    }
}
