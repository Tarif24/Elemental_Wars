using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, PlayerChoice, Busy};

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    BattleUnitPlayer playerUnit;
    [SerializeField]
    BattleUnitMonster enemyUnit;
    [SerializeField]
    BattleHudPlayer playerHUD;
    [SerializeField]
    BattleHudMonster enemyHUD;
    [SerializeField]
    BattleDialogBox dialogBox;

    BattleState state;

    int currentAction;
    int currentAbility;

    private void Start()
    {
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHUD.SetData(playerUnit.player);
        enemyHUD.SetData(enemyUnit.monster);

        dialogBox.SetMoveNames(playerUnit.player.Abilities);

        if (playerUnit.player.HP <= 0)
        {
            yield return StartCoroutine(dialogBox.TypeDialog("Your health is at a critical level please go to the shop and buy a health potion"));

            if (enemyUnit.isBoss)
            {
                SceneManager.UnloadSceneAsync(4);
            }
            else
            {
                SceneManager.UnloadSceneAsync(2);
            }
        }

        if (enemyUnit.isBoss)
        {
            yield return StartCoroutine(dialogBox.TypeDialog($"A BOSS BATTLE HAS BEGUN WITH {enemyUnit.monster.main.Name} !"));
        }
        else
        {
            yield return StartCoroutine(dialogBox.TypeDialog($"A wild {enemyUnit.monster.main.Name} has appeared."));
        }
        
        yield return new WaitForSeconds(2f);

        PlayerAction();
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;

        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableAbilitySelector(true);


    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose to either flee or fight"));
        dialogBox.EnableActionSelector(true);

    }

    void PlayerChoice()
    {
        state = BattleState.PlayerChoice;

        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableAbilitySelector(true);

        dialogBox.SetMoveNames(enemyUnit.monster.Abilities);
    }

    IEnumerator PreformEnemyMove()
    {
        state = BattleState.EnemyMove;

        AbilitiesBase ability = enemyUnit.monster.GetComputerMove();

        yield return dialogBox.TypeDialog($"{enemyUnit.monster.main.Name} used {ability.Name}");

        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.player.TakeDamage(ability, enemyUnit.monster);
        yield return playerHUD.SetHP();

        if (isDead)
        {
            playerUnit.player.isInEncounter = false;
          
            yield return dialogBox.TypeDialog("YOU HAVE BEEN SLAYIN! YOU WILL NOW BE RETURNED TO HEAL");
            SceneManager.UnloadSceneAsync(2);
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator PreformPlayerAbility()
    {
        state = BattleState.Busy;

        AbilitiesBase ability = playerUnit.player.Abilities[currentAbility];

        yield return dialogBox.TypeDialog($"{playerUnit.player.Name} used {ability.Name}");

        yield return new WaitForSeconds(2f);

        bool isDead = enemyUnit.monster.TakeDamage(ability, playerUnit.player.GetComponent<PlayerController>());
        yield return enemyHUD.SetHP();

        if (isDead)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.monster.main.Name} has been slayin");
            yield return new WaitForSeconds(2f);

            yield return dialogBox.TypeDialog($"Now take an ability from {enemyUnit.monster.main.name}");
            yield return new WaitForSeconds(2f);

            PlayerChoice();
        }
        else
        {
            StartCoroutine(PreformEnemyMove());
        }
    }

    IEnumerator PreformPlayerChoice()
    {
        state = BattleState.Busy;

        AbilitiesBase ability = enemyUnit.monster.Abilities[currentAbility];

        playerUnit.player.Abilities.Insert(0, ability);

        yield return dialogBox.TypeDialog($"You have taken the {ability.name} ability from {enemyUnit.monster.main.Name}");
        yield return new WaitForSeconds(3f);

        playerUnit.player.isInEncounter = false;

        if (enemyUnit.isBoss)
        {
            playerUnit.player.bossTier++;
            int XPTemp = 100 * (playerUnit.player.currentTierLocation + 1);
            playerUnit.player.XP += XPTemp;
            int coinsTemp = 250 * (playerUnit.player.currentTierLocation + 1);
            playerUnit.player.elementalCoins += coinsTemp;

            yield return dialogBox.TypeDialog($"You have recived {XPTemp} XP and {coinsTemp} elemental coins");
            yield return new WaitForSeconds(3f);

            SceneManager.UnloadSceneAsync(4);
        }
        else
        {
            int XPTemp = 25 * (playerUnit.player.currentTierLocation + 1);
            playerUnit.player.XP += XPTemp;
            int coinsTemp = 50 * (playerUnit.player.currentTierLocation + 1);
            playerUnit.player.elementalCoins += coinsTemp;

            yield return dialogBox.TypeDialog($"You have recived {XPTemp} XP and {coinsTemp} elemental coins");
            yield return new WaitForSeconds(3f);

            SceneManager.UnloadSceneAsync(2);
        }

    }

    private void Update()
   {
       if (state == BattleState.PlayerAction)
       {
           HandleActionSelection();
       }
       else if (state == BattleState.PlayerMove)
       {
           HandleAbilitySelection();
       }
       else if (state == BattleState.PlayerChoice)
        {
            HandleAbilityChoiceSelection();
        }
   }

    void HandleAbilitySelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentAbility < playerUnit.player.Abilities.Count - 1)
            {
                ++currentAbility;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAbility > 0)
            {
                --currentAbility;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAbility < playerUnit.player.Abilities.Count - 2)
            {
                currentAbility += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAbility > 1)
            {
                currentAbility -= 2;
            }
        }

        dialogBox.UpdateAbilitySelection(currentAbility, playerUnit.player.Abilities[currentAbility]);

        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.EnableAbilitySelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PreformPlayerAbility());
        }
    }

    void HandleAbilityChoiceSelection()
    {
        currentAbility = 0;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentAbility < enemyUnit.monster.Abilities.Count - 1)
            {
                ++currentAbility;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAbility > 0)
            {
                --currentAbility;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAbility < enemyUnit.monster.Abilities.Count - 2)
            {
                currentAbility += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAbility > 1)
            {
                currentAbility -= 2;
            }
        }

        dialogBox.UpdateAbilitySelection(currentAbility, enemyUnit.monster.Abilities[currentAbility]);

        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.EnableAbilitySelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PreformPlayerChoice());
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (currentAction > 0)
            {
                --currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (currentAction < 1)
            {
                ++currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentAction == 0)
            {
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                playerUnit.player.isInEncounter = false;
             
                if (enemyUnit.isBoss)
                {
                    SceneManager.UnloadSceneAsync(4);
                }
                else
                {
                    SceneManager.UnloadSceneAsync(2);
                }
            }
        }
    }

}
