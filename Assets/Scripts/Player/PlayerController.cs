using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    public SO_PlayerSaveData saveData;
    public bool isSafe;

    [SerializeField]
    private float SafeTime = 10f;
    private float SafeTimer = 10f;

    [SerializeField]
    List<AbilitiesBase> abilities;

    [SerializeField]
    List<AbilitiesBase> allAbilities = new List<AbilitiesBase>();

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
    public bool isBusy = false;

    public int bossTier = 0;

    public int XP;
    public int elementalCoins = 0;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinsText;
    public GameObject healthBar;

    public int currentTierLocation = 0;

    public int healthPotions = 0;

    public bool inShopRange = false;
    public bool inHomeRange = false;

    string saveFileName = "PlayerSave.txt";


    public ElementalMonsterType type = ElementalMonsterType.None;
    public ElementalMonsterType strength = ElementalMonsterType.None;
    public ElementalMonsterType weakness = ElementalMonsterType.None;

    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHP;

        allAbilities.AddRange(Resources.LoadAll<AbilitiesBase>("ScriptableMoves/"));

        if (saveData.isSaveGame)
        {
            LoadSave();
        }

        XP = level * 100;

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInEncounter && !isBusy)
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
        set { name = value; }
    }

    public int MaxHP
    {
        get { return maxHP + (Level - 1) * 10; }
    }

    public int HP
    {
        get { return Hp; }
        set {  Hp = value; }
    }

    public int Attack
    {
        get { return attack + (Level - 1) * 10; } 
    }

    public int Defense
    {
        get { return defense + (Level - 1) * 10; }
    }

    public int Level
    {
        get { return level; }
    }

    public bool TakeDamage(AbilitiesBase ability, ElementalMonster monster)
    {
        float damage = ability.Damage;

        if (Defense > monster.Attack)
        {
            damage *= 0.7f;
        }
        else
        {
            damage *= 0.9f;
        }

        damage += (monster.Attack * 0.15f);

        if (ability.Type == monster.main.Type)
        {
            damage *= 1.25f;
        }

        if (ability.Type == strength)
        {
            damage *= 0.75f;
        }
        else if (ability.Type == weakness)
        {
            damage *= 1.25f;
        }


        HP -= (int)damage;

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

    public void Save()
    {
        StreamWriter sw = new StreamWriter(saveFileName);

        sw.WriteLine(name);
        sw.WriteLine(level);
        sw.WriteLine(elementalCoins);
        sw.WriteLine(bossTier);
        sw.WriteLine(Hp);
        sw.WriteLine(currentTierLocation);
        sw.WriteLine(transform.position.x);
        sw.WriteLine(transform.position.y);

        foreach (AbilitiesBase a in abilities)
        {
            sw.WriteLine(a.Name);
        }

        sw.Close();
    }

    public void LoadSave()
    {
        abilities.Clear();

        StreamReader sr = new StreamReader(saveFileName);

        name = sr.ReadLine();
        level = int.Parse(sr.ReadLine());
        elementalCoins = int.Parse(sr.ReadLine());
        bossTier = int.Parse(sr.ReadLine());
        Hp = int.Parse(sr.ReadLine());
        currentTierLocation = int.Parse(sr.ReadLine());
        float x = float.Parse(sr.ReadLine());
        float y = float.Parse(sr.ReadLine());
        transform.position = new Vector3(x, y, transform.position.z);

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();

            foreach (AbilitiesBase a in allAbilities)
            {
                if (a.Name == line)
                {
                    abilities.Add(a);
                }
            }
        }

        sr.Close();
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

        nameText.text = "Player: " + name;
    }

    public void ChangeType(ElementalMonsterType elementType)
    {
        type = elementType;

        switch (elementType) 
        {
            case ElementalMonsterType.Fire:
                strength = ElementalMonsterType.Air;
                weakness = ElementalMonsterType.Water;
                break;
            case ElementalMonsterType.Air:
                strength = ElementalMonsterType.Earth;
                weakness = ElementalMonsterType.Fire;
                break;
            case ElementalMonsterType.Water:
                strength = ElementalMonsterType.Fire;
                weakness = ElementalMonsterType.Electric;
                break;
            case ElementalMonsterType.Earth:
                strength = ElementalMonsterType.Electric;
                weakness = ElementalMonsterType.Air;
                break;
            case ElementalMonsterType.Electric:
                strength = ElementalMonsterType.Water;
                weakness = ElementalMonsterType.Earth;
                break;
            default:
                strength = ElementalMonsterType.None;
                weakness = ElementalMonsterType.None;
                break;

        }
    }
}
