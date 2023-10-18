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
using static UnityEngine.UI.CanvasScaler;


// Defining enums
// The different battle states
public enum BattleState { START, PLAYERTURN, PLAYERTURN1, PLAYERTURN2, PLAYER3, ENEMYTURN, WON, LOST, WAITING, FLEE }
public class BattleSystem : MonoBehaviour
{
    public GameObject[] allEnemies;

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
    public GameObject spaceBarIcon;

    public BattleHud playerHUD1;
    public BattleHud playerHUD2;
    public BattleHud playerHUD3;

    public BattleHud enemyHUD;

    public BattleState state;

    // Show battlemenu
    private Boolean player1BattleMenu = false;
    private Boolean player2BattleMenu = false;
    private Boolean player3BattleMenu = false;

    public GameObject battleMenu1;
    public GameObject battleMenu2;
    public GameObject battleMenu3;

    public Button battleMenu1FirstButton;
    public Button battleMenu2FirstButton;
    public Button battleMenu3FirstButton;

    EventSystem m_EventSystem;

    //Array for turn order
    public Unit[] allUnit;
    public int turnIndex = 0;

    //Timer
    //private float delay = 5.0f;
    //private float timer = 0.0f;

    // Other functions in other scripts 
    public GameObject battleScript;
    BattleFunctions battleFunctions;

    private List<int> completedBattles;
    private int battleNumber;

    public Boolean player1Dead = false;
    public Boolean player2Dead = false;
    public Boolean player3Dead = false;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START; // Start of the battle
        completedBattles = PlayerPrefsExtra.GetList<int>("completedBattles");

        m_EventSystem = EventSystem.current;

        //battleScript = GameObject.Find("BattleFunctions");
        battleFunctions = battleScript.GetComponent<BattleFunctions>();

        StartCoroutine(SetUpBattle()); // Calling set up battle function
    }

    public void GetState(string unitType)
    {
        int currentUnitNr = allUnit[turnIndex].unitNr;

        if (unitType == "player")
        {
            // Checks the unit nr and if that player is dead
            // If dead skip that players turn
            if(currentUnitNr == 1 && !player1Dead)
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else if (currentUnitNr == 2 && !player2Dead)
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else if (currentUnitNr == 3 && !player3Dead)
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else // If the player is dead
            {
                // Writing text if it cannot perform its turn
                StartCoroutine(WriteDialogueText());
            }
        }
        else
        {
            state = BattleState.ENEMYTURN;
            PlayerPrefs.SetInt("behaviorTreeDoOnce", 1); // To make the behavior tree do once
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator WriteDialogueText()
    {
        dialogueText.text = allUnit[turnIndex].unitName + " has fainted.";
        yield return new WaitForSeconds(2f);
        // Next turn
        SetTurnIndex();
        GetState(allUnit[turnIndex].unitType);
    }

    public void SetTurnIndex()
    {
        turnIndex += 1;
        if (turnIndex >= allUnit.Length)
        {
            turnIndex = 0;
        }
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerGO1 = Instantiate(playerPrefab1, playerBattleStation1); // Spawn player on player battle station
        playerUnit1 = playerGO1.GetComponent<Unit>(); // Access the UI units

        GameObject playerGO2 = Instantiate(playerPrefab2, playerBattleStation2); // Spawn player on player battle station
        playerUnit2 = playerGO2.GetComponent<Unit>(); // Access the UI units

        GameObject playerGO3 = Instantiate(playerPrefab3, playerBattleStation3); // Spawn player on player battle station
        playerUnit3 = playerGO3.GetComponent<Unit>(); // Access the UI units


        playerUnit1.SetUnit(PlayerPrefs.GetInt("Chicken1Lvl"), PlayerPrefs.GetInt("Chicken1dmg"), PlayerPrefs.GetInt("Chicken1maxHP"),
            PlayerPrefs.GetInt("Chicken1cHP"), PlayerPrefs.GetInt("Chicken1def"), PlayerPrefs.GetInt("Chicken1speed"),
            PlayerPrefs.GetInt("Chicken1special1"), PlayerPrefs.GetInt("Chicken1special2"), PlayerPrefs.GetInt("Chicken1special3"), 
            PlayerPrefs.GetInt("Chicken1maxEXP"), PlayerPrefs.GetFloat("Chicken1cEXP"), PlayerPrefs.GetInt("Chicken1nrSpA"), PlayerPrefs.GetInt("Chicken1nrHeal"),
            PlayerPrefs.GetInt("Chicken1maxNrSpA"), PlayerPrefs.GetInt("Chicken1maxNrHeal"));

        playerUnit2.SetUnit(PlayerPrefs.GetInt("Chicken2Lvl"), PlayerPrefs.GetInt("Chicken2dmg"), PlayerPrefs.GetInt("Chicken2maxHP"),
            PlayerPrefs.GetInt("Chicken2cHP"), PlayerPrefs.GetInt("Chicken2def"), PlayerPrefs.GetInt("Chicken2speed"),
            PlayerPrefs.GetInt("Chicken2special1"), PlayerPrefs.GetInt("Chicken2special2"), PlayerPrefs.GetInt("Chicken2special3"), 
            PlayerPrefs.GetInt("Chicken2maxEXP"), PlayerPrefs.GetFloat("Chicken2cEXP"), PlayerPrefs.GetInt("Chicken2nrSpA"), PlayerPrefs.GetInt("Chicken2nrHeal"),
            PlayerPrefs.GetInt("Chicken2maxNrSpA"), PlayerPrefs.GetInt("Chicken2maxNrHeal"));

        playerUnit3.SetUnit(PlayerPrefs.GetInt("Chicken3Lvl"), PlayerPrefs.GetInt("Chicken3dmg"), PlayerPrefs.GetInt("Chicken3maxHP"),
            PlayerPrefs.GetInt("Chicken3cHP"), PlayerPrefs.GetInt("Chicken3def"), PlayerPrefs.GetInt("Chicken3speed"),
            PlayerPrefs.GetInt("Chicken3special1"), PlayerPrefs.GetInt("Chicken3special2"), PlayerPrefs.GetInt("Chicken3special3"), 
            PlayerPrefs.GetInt("Chicken3maxEXP"), PlayerPrefs.GetFloat("Chicken3cEXP"), PlayerPrefs.GetInt("Chicken3nrSpA"), PlayerPrefs.GetInt("Chicken3nrHeal"),
            PlayerPrefs.GetInt("Chicken3maxNrSpA"), PlayerPrefs.GetInt("Chicken3maxNrHeal"));

        //Debug.Log("Chicken1 HP: " + playerUnit1.currentHP.ToString());

        // Check if chickens are dead
        player1Dead = playerUnit1.CheckIfDead();
        player2Dead = playerUnit2.CheckIfDead();
        player3Dead = playerUnit3.CheckIfDead();

        // Finding the correct enemy from the array of prefab enemies 
        String tempEnemyType = PlayerPrefs.GetString("EnemyUnitType");
        GameObject tempEnemy = allEnemies[0];
        for (int i = 0; i < allEnemies.Length; i++)
        {
            GameObject tempEnemy2 = allEnemies[i];
            Unit tempEnemyUnit = tempEnemy2.GetComponent<Unit>();

            if(tempEnemyUnit.enemyUnit == tempEnemyType)
            {
                tempEnemy = allEnemies[i];
            }
        }

        GameObject enemyGo = Instantiate(tempEnemy, enemyBattleStation); // Spawn enemy on enemy battle station
        enemyUnit = enemyGo.GetComponent<Unit>();

        // Resets enemy HP to max at start of battle
        PlayerPrefs.SetInt("EnemycHP", enemyUnit.maxHP);
        PlayerPrefs.SetFloat("EnemycEXP", enemyUnit.currentExp);

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";


        playerHUD1.SetHUD(playerUnit1);
        playerHUD2.SetHUD(playerUnit2);
        playerHUD3.SetHUD(playerUnit3);
        enemyHUD.SetHUD(enemyUnit);

        // Assign the turn order
        allUnit = new Unit[] { playerUnit1, playerUnit2, playerUnit3, enemyUnit };
        allUnit = allUnit.OrderByDescending(turnOrder => turnOrder.speed).ToArray(); // Sort by speed

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        GetState(allUnit[turnIndex].unitType);

    }

    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        //bool isDead = enemyUnit.TakeDamage(playerUnit1.damage);
        bool isDead = false;

        // Check which chicken is attacking
        // Return bool if enemy is dead
        if (allUnit[turnIndex].unitNr == 1)
        {
            isDead = enemyUnit.TakeDamage(playerUnit1.damage);
            PlayerPrefs.SetInt("EnemycHP", enemyUnit.currentHP);

        }
        else if (allUnit[turnIndex].unitNr == 2)
        {
            isDead = enemyUnit.TakeDamage(playerUnit2.damage);
            PlayerPrefs.SetInt("EnemycHP", enemyUnit.currentHP);
        }
        else // Unit 3
        {
            isDead = enemyUnit.TakeDamage(playerUnit3.damage);
            PlayerPrefs.SetInt("EnemycHP", enemyUnit.currentHP);
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
            StartCoroutine(GetEXP()); // Set exp when the battle is done
            spaceBarIcon.SetActive(true);
            yield return new WaitForKey(KeyCode.Space);
            spaceBarIcon.SetActive(false);
            StartCoroutine(EndBattle());
        }
        else
        {
            GetState(allUnit[turnIndex].unitType);
        }
        // Change state based on what has happened

    }

    IEnumerator EnemyTurn()
    {
        // Add AI here if wanted
        dialogueText.text = enemyUnit.unitName + " turn";

        yield return new WaitForSeconds(1f);

        // Get the behavior tree for the enemy to do stuff

        // Update the current HP since the units are copies and not the actual prefab
        // UI also gets updated
        playerUnit1.currentHP = PlayerPrefs.GetInt("Chicken1cHP");
        playerHUD1.SetHP(playerUnit1.currentHP);

        playerUnit2.currentHP = PlayerPrefs.GetInt("Chicken2cHP");
        playerHUD2.SetHP(playerUnit2.currentHP);

        playerUnit3.currentHP = PlayerPrefs.GetInt("Chicken3cHP");
        playerHUD3.SetHP(playerUnit3.currentHP);

        enemyUnit.currentHP = PlayerPrefs.GetInt("EnemycHP");
        enemyHUD.SetHP(enemyUnit.currentHP);

        dialogueText.text = enemyUnit.unitName + " attacks the chickens!";

        //Debug.Log("Chicken1 currentHP: " + playerUnit1.currentHP);

        // Checks the current HP of all chickens if all are below 0
        if (playerUnit1.CheckIfDead())
        {
            playerUnit1.currentHP = 0;
            player1Dead = true;
            Debug.Log("Chicken 1 is dead");

        }

        if (playerUnit2.CheckIfDead())
        {
            playerUnit2.currentHP = 0;
            player2Dead = true;
            Debug.Log("Chicken 2 is dead");
        }

        if (playerUnit3.CheckIfDead())
        {
            playerUnit3.currentHP = 0;
            player3Dead = true;
            Debug.Log("Chicken 3 is dead");
        }

        yield return new WaitForSeconds(2f);

        // If all chickens are dead the battle ends
        if ((player1Dead && player2Dead && player3Dead))
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            SetTurnIndex();
            GetState(allUnit[turnIndex].unitType);
        }

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
                battleMenu1FirstButton.Select();
                playerBattleStation1.transform.position += temp;
            }
            else
            {
                //Debug.Log("tured off battle menu");
                player1BattleMenu = false;
                battleMenu1.SetActive(false);
                playerBattleStation1.transform.position -= temp;
                SetTurnIndex();
            }

        }
        else if (playerNr == 2)
        {
            if (player2BattleMenu == false)
            {
                player2BattleMenu = true;
                battleMenu2.SetActive(true);
                battleMenu2FirstButton.Select();
                playerBattleStation2.transform.position += temp;
            }
            else
            {
                //Debug.Log("tured off battle menu");
                player2BattleMenu = false;
                battleMenu2.SetActive(false);
                playerBattleStation2.transform.position -= temp;
                SetTurnIndex();
            }
        }
        else
        {
            if (player3BattleMenu == false)
            {
                player3BattleMenu = true;
                battleMenu3.SetActive(true);
                battleMenu3FirstButton.Select();
                playerBattleStation3.transform.position += temp;
            }
            else
            {
                //Debug.Log("tured off battle menu");
                player3BattleMenu = false;
                battleMenu3.SetActive(false);
                playerBattleStation3.transform.position -= temp;
                SetTurnIndex();
            }
        }
    }

    IEnumerator GetEXP()
    {
        dialogueText.text = "You won the battle!";

        Debug.Log("-BattleSystem- says: getEXP function called");

        if (player1Dead == false)
        {
            playerUnit1.SetEXP(PlayerPrefs.GetFloat("EnemycEXP"));
            playerHUD1.SetEXP(PlayerPrefs.GetFloat("Chicken1cEXP"));
            playerHUD1.SetLVL(PlayerPrefs.GetInt("Chicken1Lvl"));
            Debug.Log("-BattleSystem- says: " + playerUnit1.unitName + " gained EXP");
        }
        if (player2Dead == false)
        {
            playerUnit2.SetEXP(PlayerPrefs.GetFloat("EnemycEXP"));
            playerHUD2.SetEXP(PlayerPrefs.GetFloat("Chicken2cEXP"));
            playerHUD2.SetLVL(PlayerPrefs.GetInt("Chicken2Lvl"));
            Debug.Log("-BattleSystem- says: " + playerUnit2.unitName + " gained EXP");
        }
        if (player3Dead == false)
        {
            playerUnit3.SetEXP(PlayerPrefs.GetFloat("EnemycEXP"));
            playerHUD3.SetEXP(PlayerPrefs.GetFloat("Chicken3cEXP"));
            playerHUD3.SetLVL(PlayerPrefs.GetInt("Chicken3Lvl"));
            Debug.Log("-BattleSystem- says: " + playerUnit3.unitName + " gained EXP");
        }

        // Wait for the space bar to be pressed
        spaceBarIcon.SetActive(true);
        yield return new WaitForKey(KeyCode.Space);
        spaceBarIcon.SetActive(false);

        dialogueText.text = "The chickens gained " + enemyUnit.currentExp + " EXP.";
    }


    // On battle end
    IEnumerator EndBattle()
    {
        // Re-sets the PlayerPrefs variables for the chickens
        battleFunctions.AssignStats(playerUnit1.unitNr, playerUnit1.unitLevel, playerUnit1.damage, playerUnit1.maxHP, playerUnit1.currentHP, 
            playerUnit1.defense, playerUnit1.speed, playerUnit1.specialSkill1, playerUnit1.specialSkill2, playerUnit1.specialSkill3, 
            playerUnit1.maxExp, playerUnit1.currentExp, playerUnit1.noOfSpecialAttacks, playerUnit1.noOfHeals, playerUnit1.maxOfSpecialAttacks, playerUnit1.maxOfHeals);

        battleFunctions.AssignStats(playerUnit2.unitNr, playerUnit2.unitLevel, playerUnit2.damage, playerUnit2.maxHP, playerUnit2.currentHP, 
            playerUnit2.defense, playerUnit2.speed, playerUnit2.specialSkill1, playerUnit2.specialSkill2, playerUnit2.specialSkill3, 
            playerUnit2.maxExp, playerUnit2.currentExp, playerUnit2.noOfSpecialAttacks, playerUnit2.noOfHeals, playerUnit2.maxOfSpecialAttacks, playerUnit2.maxOfHeals);

        battleFunctions.AssignStats(playerUnit3.unitNr, playerUnit3.unitLevel, playerUnit3.damage, playerUnit3.maxHP, playerUnit3.currentHP, 
            playerUnit3.defense, playerUnit3.speed, playerUnit3.specialSkill1, playerUnit3.specialSkill2, playerUnit3.specialSkill3, 
            playerUnit3.maxExp, playerUnit3.currentExp, playerUnit3.noOfSpecialAttacks, playerUnit3.noOfHeals, playerUnit3.maxOfSpecialAttacks, playerUnit3.maxOfHeals);

        Debug.Log("-BattleSystem- says: re-assigned stats");

        if (state == BattleState.WON)
        {
 
            battleNumber = PlayerPrefs.GetInt("currentBattle");

            completedBattles[battleNumber] = 1;
            PlayerPrefsExtra.SetList("completedBattles", completedBattles);

            //SceneManager.LoadScene(PlayerPrefs.GetString("currentWorld")); // Load overworld
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("currentWorld"));
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost the battle.";
            // Back to menu for now if player loses the battle
            SceneManager.LoadScene("Menu");
        }
        else if (state == BattleState.FLEE)
        {
            dialogueText.text = "You fled the battle";

            SceneManager.LoadScene(PlayerPrefs.GetString("currentWorld"));
        }

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

        bool hasHealed = currentUnit.Heal(5);

        if (currentUnit.unitNr == 1 && hasHealed)
        {
            playerHUD1.SetHP(currentUnit.currentHP);
            PlayerPrefs.SetInt("Chicken1cHP", playerUnit1.currentHP);
        }
        else if (allUnit[turnIndex].unitNr == 2 && hasHealed)
        {
            playerHUD2.SetHP(currentUnit.currentHP);
            PlayerPrefs.SetInt("Chicken2cHP", playerUnit2.currentHP);
        }
        else if(allUnit[turnIndex].unitNr == 3 && hasHealed)// Unit 3
        {
            playerHUD3.SetHP(currentUnit.currentHP);
            PlayerPrefs.SetInt("Chicken3cHP", playerUnit3.currentHP);
        }

        //currentUnit.battleHud.SetHP(currentUnit.currentHP);

        state = BattleState.WAITING; // Prevent button spamming

        dialogueText.text = allUnit[turnIndex].unitName + " feel renewed!";

        yield return new WaitForSeconds(2f);

        //state = BattleState.ENEMYTURN;
        //StartCoroutine(EnemyTurn());
        GetState(allUnit[turnIndex].unitType);
    }

    // Enemy and other 
    IEnumerator PlayerSpecialAttack()
    {
        // Damage the enemy
        //bool isDead = enemyUnit.TakeDamage(playerUnit1.damage);
        bool isDead;

        Unit currentUnit = allUnit[turnIndex];

        // Check which chicken is attacking
        // Return bool if enemy is dead
        if (currentUnit.unitNr == 1)
        {
            isDead = enemyUnit.TakeDamage(playerUnit1.damage, playerUnit1.specialSkill2);
            PlayerPrefs.SetInt("EnemycHP", enemyUnit.currentHP);

            //playerUnit1.currentHP -= playerUnit1.specialSkill2;
            player1Dead = playerUnit1.TakeDamage(playerUnit1.specialSkill2);
            PlayerPrefs.SetInt("Chicken1cHP", playerUnit1.currentHP);
            playerHUD1.SetHP(playerUnit1.currentHP);
            Debug.Log(playerUnit1 + " current HP: " +playerUnit1.currentHP);

        }
        else if (currentUnit.unitNr == 2)
        {
            isDead = enemyUnit.TakeDamage(playerUnit2.damage, playerUnit2.specialSkill2);
            PlayerPrefs.SetInt("EnemycHP", enemyUnit.currentHP);

            //playerUnit2.currentHP -= playerUnit2.specialSkill2;
            player2Dead = playerUnit2.TakeDamage(playerUnit2.specialSkill2);
            PlayerPrefs.SetInt("Chicken2cHP", playerUnit2.currentHP);
            playerHUD2.SetHP(playerUnit2.currentHP);
        }
        else // Unit 3
        {
            isDead = enemyUnit.TakeDamage(playerUnit3.damage, playerUnit3.specialSkill2);
            PlayerPrefs.SetInt("EnemycHP", enemyUnit.currentHP);

            //playerUnit3.currentHP -= playerUnit3.specialSkill2;
            player3Dead = playerUnit3.TakeDamage(playerUnit3.specialSkill2);
            PlayerPrefs.SetInt("Chicken3cHP", playerUnit3.currentHP);
            playerHUD3.SetHP(playerUnit3.currentHP);
        }

        enemyHUD.SetHP(enemyUnit.currentHP); // Change later depending on if more then one enemy unit
        state = BattleState.WAITING; // Prevent button spamming

        // Change the name of the attack later
        dialogueText.text = "The special attack is succesfull! \n" + currentUnit.unitName + " took " + currentUnit.specialSkill2 + " damage.";

        yield return new WaitForSeconds(2f);

        // Check if enemy is dead 
        if (isDead)
        {
            // End the battle
            state = BattleState.WON;
            StartCoroutine(GetEXP()); // Set exp when the battle is done

            spaceBarIcon.SetActive(true);
            yield return new WaitForKey(KeyCode.Space);
            spaceBarIcon.SetActive(false);

            StartCoroutine(EndBattle());
        }
        else
        {
            GetState(allUnit[turnIndex].unitType);
        }
        // Change state based on what has happened

    }

    public IEnumerator PlayerFlee()
    {
        state = BattleState.WAITING;
        state = BattleState.FLEE;
        StartCoroutine(EndBattle());
        yield return new WaitForSeconds(2f);
    }


    // When the attack button is pressed
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) // Borde g�ras p� ett b�ttre s�tt
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

    public void OnSpecialAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //EnableBattleMenu();
        //StartCoroutine(PlayerBlock());
        StartCoroutine(PlayerSpecialAttack());
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

}

