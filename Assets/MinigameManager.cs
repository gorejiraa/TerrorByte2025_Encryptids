using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public bool gameStarted = true;
    public bool canShoot = false;
    private bool opponentShot = false;
    private bool playerShot = false;
    private float shootTimer = 0f;
    private float shootTimeLimit;
    public float opponentShootTime;
    public int winsRequired = 3;
    private int playerWins = 0;
    private int opponentWins = 0;
    public AudioSource shootPromptSFX;
    public Image shootPromptUI;
    public TextMeshProUGUI playerWinText;
    public TextMeshProUGUI opponentWinText;
    public GameObject crosshair;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootTimeLimit = Random.Range(5f, 10f);
        UpdateWinUI();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > shootTimeLimit && !canShoot) 
        {
            TimeLimitReached();
        }
        else if (shootTimer > shootTimeLimit && canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                if (crosshair.GetComponent<CrosshairMovement>().CrosshairIsOverOpponent()) 
                {
                    opponentShot = true;
                    playerWins++;
                    UpdateWinUI();
                    shootPromptSFX.Play();
                    ResetMinigame();
                }
            }
        }

        float realOpponentTimer = opponentShootTime * Mathf.Min(1 + (opponentWins * 0.15f), 1.5f);

        if (shootTimer > shootTimeLimit + realOpponentTimer && !opponentShot) 
        {
            canShoot = false;
            playerShot = true;
            opponentWins++;
            UpdateWinUI();
            shootPromptSFX.Play();
            ResetMinigame();
        }
    }

    void UpdateWinUI() 
    {
        playerWinText.text = $"You: {playerWins}";
        opponentWinText.text = $"Opp: {opponentWins}";
    }

    void TimeLimitReached() 
    {
        shootPromptSFX.Play();
        shootPromptUI.gameObject.SetActive(true);
        canShoot = true;
    }

    void ResetMinigame() 
    {
        shootTimer = 0f;
        shootTimeLimit = Random.Range(5f, 10f);
        canShoot = false;
        opponentShot = false;
        playerShot = false;
        shootPromptUI.gameObject.SetActive(false);
    }
}
