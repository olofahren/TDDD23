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
using UnityEngine.EventSystems;


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

    public Unit playerUnit1;
    public Unit playerUnit2;
    public Unit playerUnit3;

    public Unit enemyUnit;

    // UI Texts
    public TextMeshProUGUI dialogueText;

    public BattleHud playerHUD1;
    public BattleHud playerHUD2;
    public BattleHud playerHUD3;


    public BattleHud enemyHUD;

    //public Image skillMenu;
    public GameObject skillMenu;

    public BattleState state;

    // Block -> Flytta till Unit klassen???
    public Boolean blockingPlayer1 = false;
    public Boolean blockingPlayer2 = false;
    public Boolean blockingPlayer3 = false;

    // Show battlemenu
    public Boolean player1BattleMenu = false;
    public Boolean player2BattleMenu = false;
    public Boolean player3BattleMenu = false;

    public GameObject battleMenu1;
    public GameObject battleMenu2;
    public GameObject battleMenu3;

    //Array for turn order
    public Unit[] allUnit;
    public int turnIndex = 0;

    //Timer
    private float delay = 5.0f;
    private float timer = 0.0f;

    // Other functions in other scripts 
    public GameObject battleScript;
    BattleFunctions battleFunctions;

    private List<int> completedBattles;
    private int battleNumber;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START; // Start of the battle

        completedBattles = PlayerPrefsExtra.GetList<int>("completedBattles");

        //battleScript = GameObject.Find("BattleFunctions");
        battleFunctions = battleScript.GetComponent<BattleFunctions>();

        StartCoroutine(setUpBattle()); // Calling set up battle function
    }

    // Checkking if its enemy or player turn
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
            PlayerPrefs.SetInt("behaviorTreeDoOnce", 1); // To make the behavior tree do once
            StartCoroutine(EnemyTurn());
        }
    }

    // Sets who's turn it is in the index
    public void setTurnIndex()
    {
        turnIndex += 1;
        // If the turnIndex is larger than tot amount of units set turnIndex back to 0
        // to restart the turn order
        if (turnIndex >=  allUnit.Length)
        {
            turnIndex = 0;
        }
    }

    // Sets up the battle 
    IEnumerator setUpBattle()
    {
        // Initiate player to show up on the right battle staion
        GameObject playerGO1 = Instantiate(playerPrefab1, playerBattleStation1); // Spawn player on player battle station
        playerUnit1 = playerGO1.GetComponent<Unit>(); // Access the UI units

        GameObject playerGO2 = Instantiate(playerPrefab2, playerBattleStation2); // Spawn player on player battle station
        playerUnit2 = playerGO2.GetComponent<Unit>(); // Access the UI units

        GameObject playerGO3 = Instantiate(playerPrefab3, playerBattleStation3); // Spawn player on player battle station
        playerUnit3 = playerGO3.GetComponent<Unit>(); // Access the UI units

        // Set player units from the global variables
        playerUnit1.setUnit(PlayerPrefs.GetInt("Chicken1Lvl"), PlayerPrefs.GetInt("Chicken1dmg"), PlayerPrefs.GetInt("Chicken1maxHP"),
            PlayerPrefs.GetInt("Chicken1cHP"), PlayerPrefs.GetInt("Chicken1def"), PlayerPrefs.GetInt("Chicken1speed"),
            PlayerPrefs.GetInt("Chicken1special1"), PlayerPrefs.GetInt("Chicken1special2"), PlayerPrefs.GetInt("Chicken1special3"));

        playerUnit2.setUnit(PlayerPrefs.GetInt("Chicken2Lvl"), PlayerPrefs.GetInt("Chicken2dmg"), PlayerPrefs.GetInt("Chicken2maxHP"),
             PlayerPrefs.GetInt("Chicken2cHP"), PlayerPrefs.GetInt("Chicken2def"), PlayerPrefs.GetInt("Chicken2speed"),
            PlayerPrefs.GetInt("Chicken2special1"), PlayerPrefs.GetInt("Chicken2special2"), PlayerPrefs.GetInt("Chicken2special3"));

        playerUnit3.setUnit(PlayerPrefs.GetInt("Chicken3Lvl"), PlayerPrefs.GetInt("Chicken3dmg"), PlayerPrefs.GetInt("Chicken3maxHP"),
             PlayerPrefs.GetInt("Chicken3cHP"), PlayerPrefs.GetInt("Chicken3def"), PlayerPrefs.GetInt("Chicken3speed"),
            PlayerPrefs.GetInt("Chicken3special1"), PlayerPrefs.GetInt("Chicken3special2"), PlayerPrefs.GetInt("Chicken3special3"));

        //Debug.Log("Chicken2 HP: " + playerUnit2.currentHP.ToString());

        // Initiate enemy 
        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation); // Spawn enemy on enemy battle station
        enemyUnit = enemyGo.GetComponent<Unit>();

        // Battle dialogue
        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        // Set UI 
        playerHUD1.SetHUD(playerUnit1);
        playerHUD2.SetHUD(playerUnit2);
        playerHUD3.SetHUD(playerUnit3);
        enemyHUD.SetHUD(enemyUnit);

        // Assign the turn order
        allUnit = new Unit[] { playerUnit1, playerUnit2, playerUnit3, enemyUnit };
        allUnit = allUnit.OrderByDescending(turnOrder => turnOrder.speed).ToArray(); // Sort by speed

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        // Get who's first turn it is
        getState(allUnit[turnIndex].unitType);
    }

    // Enable/Disable battle menu
    void EnableBattleMenu(int playerNr)
    {
        //Debug.Log("Unit Player Nr: " + playerNr);
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
                //Debug.Log("tured off battle menu");
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
                //Debug.Log("tured off battle menu");
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
                //Debug.Log("tured off battle menu");
                player3BattleMenu = false;
                battleMenu3.SetActive(false);
                playerBattleStation3.transform.position -= temp;
                setTurnIndex();
            }
        }
    }
    

    // On battle end
    public void EndBattle()
    {
        if(state == BattleState.WON)
        {
            battleNumber = PlayerPrefs.GetInt("currentBattle");

            completedBattles[battleNumber] = 1;
            PlayerPrefsExtra.SetList("completedBattles", completedBattles);

            dialogueText.text = "You won the battle!";
        }else if(state == BattleState.LOST)
        {
            dialogueText.text = "You lost the battle.";
        }else if(state==BattleState.FLEE)
        {
            dialogueText.text = "You fled the battle";
        }

        // Re-assign global variables for the player units
        // Used in other battles
        battleFunctions.assignStats(playerUnit1.unitNr, playerUnit1.unitLevel,
                    playerUnit1.damage, playerUnit1.maxHP, playerUnit1.currentHP, playerUnit1.defense, playerUnit1.speed,
                    playerUnit1.specialSill1, playerUnit1.specialSill2, playerUnit1.specialSill3);

        battleFunctions.assignStats(playerUnit2.unitNr, playerUnit2.unitLevel,
                    playerUnit2.damage, playerUnit2.maxHP, playerUnit2.currentHP, playerUnit2.defense, playerUnit2.speed,
                    playerUnit2.specialSill1, playerUnit2.specialSill2, playerUnit2.specialSill3);

        battleFunctions.assignStats(playerUnit3.unitNr, playerUnit3.unitLevel,
                    playerUnit3.damage, playerUnit3.maxHP, playerUnit3.currentHP, playerUnit3.defense, playerUnit3.speed,
                    playerUnit3.specialSill1, playerUnit3.specialSill2, playerUnit3.specialSill3);

        //Debug.Log("CHicken2HP: " + PlayerPrefs.GetInt("Chicken2HP"));

        // Move back to the overworld
        SceneManager.LoadScene(PlayerPrefs.GetString("currentWorld"));
    }

    // On players turn
    void PlayerTurn()
    {
        dialogueText.text = "Choose an action for " + allUnit[turnIndex].unitName;
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }

    IEnumerator EnemyTurn()
    {
        // Add AI here if wanted
        dialogueText.text = enemyUnit.unitName + " attacks";

        yield return new WaitForSeconds(1f);

        bool isDead = false;

        // Get the behavior tree for the enemy to do stuff

        if (playerUnit1.currentHP < 0) // Re-write later to make it check all chickens
        {
            isDead = true;
        }


        /* if (blockingPlayer1)
         {
             isDead = playerUnit1.BlockDamage(enemyUnit.damage, playerUnit1.defense);
             blockingPlayer1 = false;
             //Debug.Log(blockingPlayer1);
         }
         else if(blockingPlayer2)
         {
             isDead = playerUnit2.BlockDamage(enemyUnit.damage, playerUnit2.defense);
             blockingPlayer2 = false;
         }
         else if(blockingPlayer3)
         {
             isDead = playerUnit3.BlockDamage(enemyUnit.damage, playerUnit3.defense);
             blockingPlayer2 = false;
         }
         else
         {
             isDead = playerUnit2.TakeDamage(enemyUnit.damage); // Damage the player
         }*/

        // Update the current HP since the units are copies and not the actual prefab
        playerUnit1.currentHP = PlayerPrefs.GetInt("Chicken1cHP");

        playerHUD1.SetHP(playerUnit1.currentHP);

        Debug.Log("Chicken1 currentHP: " + playerUnit1.currentHP);

        yield return new WaitForSeconds(2f);

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

    /* ---------------------------- Button functions ---------------------------- */

    // When the attack button is pressed
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) // Borde göras på ett bättre sätt
        {
            return;
        }
        //EnableBattleMenu();
        StartCoroutine(battleFunctions.PlayerAttack());
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //EnableBattleMenu();
        StartCoroutine(battleFunctions.PlayerHeal());
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //EnableBattleMenu();
        StartCoroutine(battleFunctions.PlayerBlock());
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }

    public void OnFleeButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        //EnableBattleMenu();
        StartCoroutine(battleFunctions.PlayerFlee());
        EnableBattleMenu(allUnit[turnIndex].unitNr);
    }

}

