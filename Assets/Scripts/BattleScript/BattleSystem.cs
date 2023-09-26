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
using UnityEngine.SceneManagement;

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
    public int turnIndex = 0;

    //Timer
    private float delay = 5.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START; // Start of the battle

        StartCoroutine(setUpBattle()); // Calling set up battle function
    }

    public void getState(string unitType)
    {

        if (unitType == "player")
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void setTurnIndex()
    {
        turnIndex += 1;
        if (turnIndex >=  allUnit.Length)
        {
            turnIndex = 0;
        }
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
        allUnit = allUnit.OrderByDescending(turnOrder => turnOrder.speed).ToArray(); // Sort by speed

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        getState(allUnit[turnIndex].unitType);

    }

    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        //bool isDead = enemyUnit.TakeDamage(playerUnit1.damage);
        bool isDead = false;

        if (allUnit[turnIndex].unitNr == 1)
        {
            isDead = enemyUnit.TakeDamage(playerUnit1.damage);

        }
        else if (allUnit[turnIndex].unitNr == 2)
        {
             isDead = enemyUnit.TakeDamage(playerUnit2.damage);
        }
        else // Unit 3
        {
            isDead = enemyUnit.TakeDamage(playerUnit3.damage);
        }

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
            getState(allUnit[turnIndex].unitType);
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
            //Debug.Log(blockingPlayer1);
        }
        else if(blockingPlayer2)
        {
            isDead = playerUnit2.BlockDamage(enemyUnit.damage, playerUnit2.deffense);
            blockingPlayer2 = false;
        }
        else if(blockingPlayer3)
        {
            isDead = playerUnit3.BlockDamage(enemyUnit.damage, playerUnit3.deffense);
            blockingPlayer2 = false;
        }
        else
        {
            isDead = playerUnit2.TakeDamage(enemyUnit.damage); // Damage the player
        }
        
        playerHUD2.SetHP(playerUnit2.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            setTurnIndex();
            getState(allUnit[turnIndex].unitType);
        }

    }

    // Enable/Diable battle menu
    private void EnableBattleMenu() // Remove function
    {

        if(player1BattleMenu == false)
        {
            player1BattleMenu = true;
            battleMenu1.SetActive(true);
        }
        else
        {
            player1BattleMenu = false;
            battleMenu1.SetActive(false);
            setTurnIndex();
        }


    }

    void EnableBattleMenu(int playerNr)
    {
        Debug.Log("Unit Player Nr: " + playerNr);
        Vector3 temp = new(-1.5f, 0, 0);
        if (playerNr == 1)
        {
            if (player1BattleMenu == false)
            {
                player1BattleMenu = true;
                battleMenu1.SetActive(true);
                playerBattleStation1.transform.position += temp;
            }
            else
            {
                Debug.Log("tured off battle menu");
                player1BattleMenu = false;
                battleMenu1.SetActive(false);
                playerBattleStation1.transform.position -= temp;
                setTurnIndex();
            }

        }
        else if(playerNr == 2)
        {
            if (player2BattleMenu == false)
            {
                player2BattleMenu = true;
                battleMenu2.SetActive(true);
                playerBattleStation2.transform.position +=temp;
            }
            else
            {
                Debug.Log("tured off battle menu");
                player2BattleMenu = false;
                battleMenu2.SetActive(false);
                playerBattleStation2.transform.position -= temp;
                setTurnIndex();
            }
        }
        else
        {
            if (player3BattleMenu == false)
            {
                player3BattleMenu = true;
                battleMenu3.SetActive(true);
                playerBattleStation3.transform.position += temp;
            }
            else
            {
                Debug.Log("tured off battle menu");
                player3BattleMenu = false;
                battleMenu3.SetActive(false);
                playerBattleStation3.transform.position -= temp;
                setTurnIndex();
            }
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



            SceneManager.LoadScene("World1");
    }

    // On players turn
    void PlayerTurn()
    {
        dialogueText.text = "Choose an action for " + allUnit[turnIndex].unitName;
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }


    IEnumerator PlayerHeal()
    {
        Unit currentUnit = allUnit[turnIndex];

        currentUnit.Heal(5);

        if (currentUnit.unitNr == 1)
        {
            playerHUD1.SetHP(currentUnit.currentHP);

        }
        else if (allUnit[turnIndex].unitNr == 2)
        {
            playerHUD2.SetHP(currentUnit.currentHP);
        }
        else // Unit 3
        {
            playerHUD2.SetHP(currentUnit.currentHP);
        }

        //currentUnit.battleHud.SetHP(currentUnit.currentHP);

        state = BattleState.WAITING; // Prevent button spamming

        dialogueText.text = allUnit[turnIndex].unitName + " feel renewed!";

        yield return new WaitForSeconds(2f);

        //state = BattleState.ENEMYTURN;
        //StartCoroutine(EnemyTurn());
        getState(allUnit[turnIndex].unitType);
    }

    IEnumerator PlayerBlock()
    {

        if (allUnit[turnIndex].unitNr == 1)
        {
            blockingPlayer1 = true;

        }
        else if (allUnit[turnIndex].unitNr == 2)
        {
            blockingPlayer2 = true;
        }
        else // Unit 3
        {
            blockingPlayer3 = true;
        }
            
            
        state = BattleState.WAITING; // Prevent button spamming

        yield return new WaitForSeconds(2f);

        //state = BattleState.ENEMYTURN;
        //StartCoroutine(EnemyTurn());
        getState(allUnit[turnIndex].unitType);

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
        //EnableBattleMenu();
        StartCoroutine(PlayerAttack());
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //EnableBattleMenu();
        StartCoroutine(PlayerHeal());
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //EnableBattleMenu();
        StartCoroutine(PlayerBlock());
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }

    public void OnFleeButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        //EnableBattleMenu();
        StartCoroutine(PlayerFlee());
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }

    public void OnSkillButton()
    {
        skillMenu.SetActive(true);
    }

}

