using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // UI Mesh
using System.Data.SqlTypes;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Linq;

// Defining enums
// The different battle states
public enum BattleState { START, PLAYERTURN, PLAYERTURN1, PLAYERTURN2, PLAYER3, ENEMYTURN, WON, LOST, WAITING, FLEE }
public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;

    public GameObject enemyPrefab;

    // Only need the coordinates and not the whole game object hence the transform
    public Transform playerBattleStation1;
    public Transform playerBattleStation2;
    public Transform playerBattleStation3;

    public Transform enemyBattleStation;

    Unit playerUnit1;
    Unit playerUnit2;
    Unit playerUnit3;

    Unit enemyUnit;

    // UI Texts
    public TextMeshProUGUI dialogueText;

    public BattleHud playerHUD1;
    public BattleHud playerHUD2;
    public BattleHud playerHUD3;


    public BattleHud enemyHUD;

    // UI Skills menu
    //public Image battleMenu1;
    //public Image battleMenu2;
    //public Image battleMenu3;

    //public Image skillMenu;
    public GameObject skillMenu;

    public BattleState state;

    // Block -> Flytta till Unit klassen???
    private Boolean blockingPlayer1 = false;
    private Boolean blockingPlayer2 = false;
    private Boolean blockingPlayer3 = false;

    // Show battlemenu
    private Boolean player1BattleMenu = false;
    private Boolean player2BattleMenu = false;
    private Boolean player3BattleMenu = false;

    public GameObject battleMenu1;
    public GameObject battleMenu2;
    public GameObject battleMenu3;

    //Array for turn order
    public Unit[] allUnit;
    public List<Unit> sortedUnit;
    public int turnIndex = 0; 

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START; // Start of the battle

        StartCoroutine(setUpBattle()); // Calling set up battle function
    }

    public void getState(string unitType)
    {
        Debug.Log(sortedUnit[turnIndex].unitName + "'s turn");

        if (unitType == "player")
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
            Debug.Log(state);
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            Debug.Log(state);
        }
    }

    public void setTurnIndex()
    {
        Debug.Log(sortedUnit.Count);
        if (turnIndex >=  sortedUnit.Count)
        {
            turnIndex = 0;
        }
        else
        {
            turnIndex += 1; // When turn is over increase the turn with one
        }

        Debug.Log("TurnIndex: " + turnIndex);
    }

    IEnumerator setUpBattle()
    {
        GameObject playerGO1 = Instantiate(playerPrefab1, playerBattleStation1); // Spawn player on player battle station
        playerUnit1 = playerGO1.GetComponent<Unit>(); // Access the UI units

        GameObject playerGO2 = Instantiate(playerPrefab2, playerBattleStation2); // Spawn player on player battle station
        playerUnit2 = playerGO2.GetComponent<Unit>(); // Access the UI units

        GameObject playerGO3 = Instantiate(playerPrefab3, playerBattleStation3); // Spawn player on player battle station
        playerUnit3 = playerGO3.GetComponent<Unit>(); // Access the UI units


        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation); // Spawn enemy on enemy battle station
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD1.SetHUD(playerUnit1);
        playerHUD2.SetHUD(playerUnit2);
        playerHUD3.SetHUD(playerUnit3);
        enemyHUD.SetHUD(enemyUnit);

        // Assign the turn order
        allUnit = new Unit[] { playerUnit1, playerUnit2, playerUnit3, enemyUnit };
        sortedUnit = allUnit.OrderByDescending(turnOrder => turnOrder.speed).ToList(); // Sort by speed

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        state = BattleState.PLAYERTURN; // After setting up battle enter player turn
        PlayerTurn();

        //getState(sortedUnit[turnIndex].unitType);

    }

    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        //enemyUnit.TakeDamage(playerUnit.damage
        //Debug.Log(sortedUnit[turnIndex].name + " is attacking");
        bool isDead = enemyUnit.TakeDamage(playerUnit1.damage);

        enemyHUD.SetHP(enemyUnit.currentHP); // Change later depending on if more then one enemy unit
        state = BattleState.WAITING; // Prevent button spamming

        dialogueText.text = "The attack is succesfull!";

        yield return new WaitForSeconds(2f);

        // Check if enemy is dead 
        if (isDead)
        {
            // End the battle
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            // Enemys turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            //setTurnIndex();
            //getState(sortedUnit[turnIndex].unitType);
        }
        // Change state based on what has happened

    }

    IEnumerator EnemyTurn()
    {
        // Add AI here if wanted
        dialogueText.text = enemyUnit.unitName + " attacks";

        yield return new WaitForSeconds(1f);

        bool isDead;

        if (blockingPlayer1)
        {
            isDead = playerUnit1.BlockDamage(enemyUnit.damage, playerUnit1.deffense);
            blockingPlayer1 = false;
            Debug.Log(blockingPlayer1);
        }
        else
        {
            isDead = playerUnit1.TakeDamage(enemyUnit.damage); // Damage the player
        }
        
        playerHUD1.SetHP(playerUnit1.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            //setTurnIndex();
            // getState(sortedUnit[turnIndex].unitType);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    // Enable/Diable battle menu
    private void enableBattleMenu(Unit playerUnit)
    {
        /*if (playerUnit.showBattleMenu == true)
        {
            if(playerUnit.unitNr == 1) {
                battleMenu1.SetActive(false);
            }
            else if(playerUnit.unitNr == 2)
            {
                battleMenu2.SetActive(false);
            }
            else //unit nr3.
            {
                battleMenu3.SetActive(false);
            }

            playerUnit.showBattleMenu = false;
        }
        else
        {
            if (playerUnit.unitNr == 1)
            {
                battleMenu1.SetActive(true);
            }
            else if (playerUnit.unitNr == 2)
            {
                battleMenu2.SetActive(true);
            }
            else //unit nr3.
            {
                battleMenu3.SetActive(true);
            }

            setTurnIndex();
            playerUnit.showBattleMenu = true;
        }*/

        if(player1BattleMenu == false)
        {
            player1BattleMenu = true;
            battleMenu1.SetActive(true);
        }
        else
        {
            player1BattleMenu = false;
            battleMenu1.SetActive(false);
        }

    }
    

    // On battle end
    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }else if(state == BattleState.LOST)
        {
            dialogueText.text = "You lost the battle.";
        }else if(state==BattleState.FLEE)
        {
            dialogueText.text = "You fled the battle";
        }
    }

    // On players turn
    void PlayerTurn()
    {

        dialogueText.text = "Choose an action";
        enableBattleMenu(sortedUnit[turnIndex]);
    }


    IEnumerator PlayerHeal()
    {
        Unit currentUnit = sortedUnit[turnIndex];

        currentUnit.Heal(5);

        currentUnit.battleHud.SetHP(currentUnit.currentHP);

        state = BattleState.WAITING; // Prevent button spamming

        dialogueText.text = "You feel renewed!";

        yield return new WaitForSeconds(2f);

        //setTurnIndex();
        //getState(sortedUnit[turnIndex].unitType);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerBlock()
    {
        blockingPlayer1 = true;
        Debug.Log(blockingPlayer1);
        //dialogueText.text = playerUnit.unitName + " is blocking.";

        state = BattleState.WAITING; // Prevent button spamming

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        //setTurnIndex();
        //getState(sortedUnit[turnIndex].unitType);
    }

    IEnumerator PlayerFlee() {

        state = BattleState.WAITING;
        state = BattleState.FLEE;

        EndBattle();
        yield return new WaitForSeconds(2f);
    }

    // When the attack button is pressed
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) // Borde göras på ett bättre sätt
        {
            return;
        }
        enableBattleMenu(sortedUnit[turnIndex]);
        Debug.Log("Attack button is pressed");
        StartCoroutine(PlayerAttack());
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        enableBattleMenu(sortedUnit[turnIndex]);
        StartCoroutine(PlayerHeal());
    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        enableBattleMenu(sortedUnit[turnIndex]);
        StartCoroutine(PlayerBlock());
    }

    public void OnFleeButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //
        enableBattleMenu(sortedUnit[turnIndex]);
        StartCoroutine(PlayerFlee());
    }

    public void OnSkillButton()
    {
        skillMenu.SetActive(true);
    }

}

