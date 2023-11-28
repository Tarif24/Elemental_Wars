using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    public SO_PlayerSaveData SO_SaveData;
    public bool isSafe;

    [SerializeField]
    private float SafeTime = 10f;
    private float SafeTimer = 10f;

    [SerializeField]
    List<AbilitiesBase> abilities;

    [SerializeField]
    string name;
    [SerializeField]
    int maxHP;
    [SerializeField]
    int Hp;
    [SerializeField]
    int attack;
    [SerializeField]
    int defense;
    [SerializeField]
    int level;

    bool isMoving = false;

    private AudioSource playerAudio;
    public AudioClip soundToPlay;

    public AudioClip[] walkingSounds;
    public string[] typesOfGroundTags;

    public bool isInEncounter = false;

    public int bossTier = 0;

    public int XP;
    public int elementalCoins = 0;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinsText;
    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHP;

        if (SO_SaveData.isSaveGame)
        {
            transform.position = SO_SaveData.saveLocation;
        }

        XP = level * 100;

        playerAudio = GetComponent<AudioSource>();

        nameText.text = "Player: " + name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInEncounter)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput == 0 && verticalInput == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            Vector2 normalizedinput = inputVector.normalized;

            transform.position += new Vector3(normalizedinput.x * moveSpeed, normalizedinput.y * moveSpeed, 0) * Time.deltaTime;
        }
        else
        {
            isMoving = false;
        }

        if (SafeTimer <= 0f && isSafe && !isInEncounter)
        {
            SafeTimer = SafeTime;
            isSafe = false;
        }
        else if (isSafe && !isInEncounter)
        {
            SafeTimer -= Time.deltaTime;
        }

        if (isMoving)
        {
            
            if (!playerAudio.isPlaying)
            {
                playerAudio.PlayOneShot(soundToPlay, 2.0f);
            }
            
        }
        else if (playerAudio.isPlaying)
        {
            playerAudio.Stop();
        }

        level = XP / 100;
        UpdateHUD();
    }

    public List<AbilitiesBase> Abilities
    {
        get { return abilities; }
    }

    public string Name
    {
        get { return name; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public int HP
    {
        get { return Hp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int Level
    {
        get { return level; }
    }

    public bool TakeDamage(AbilitiesBase ability, ElementalMonster monster)
    {
        Hp -= ability.Damage;

        if (Hp <= 0)
        {
            Hp = 0;

            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveLocation()
    {
        SO_SaveData.saveLocation = transform.position;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < typesOfGroundTags.Length; i++)
        {
            if (collision.gameObject.CompareTag(typesOfGroundTags[i]))
            {
                soundToPlay = walkingSounds[i];
                playerAudio.Stop();
            }
        }
    }

    void UpdateHUD()
    {
        float hpNormalized = (float) Hp / MaxHP;
        healthBar.transform.localScale = new Vector3(hpNormalized, 1f);

        levelText.text = "Level: " + level;
        coinsText.text = "Elemental Coins: " + elementalCoins;
    }
}
