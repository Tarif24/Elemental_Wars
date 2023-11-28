using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy};

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

            playerUnit.player.isInEncounter = false;

            if (enemyUnit.isBoss)
            {
                playerUnit.player.bossTier++;
                SceneManager.UnloadSceneAsync(4);
            }
            else 
            {
                SceneManager.UnloadSceneAsync(2);
            }
        }
        else
        {
            StartCoroutine(PreformEnemyMove());
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
