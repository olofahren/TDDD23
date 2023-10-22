using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        public UnityEngine.GameObject gameObjectEnemy;
        public UnityEngine.GameObject gameObjectPlayer1;
        public UnityEngine.GameObject gameObjectPlayer2;
        public UnityEngine.GameObject gameObjectPlayer3;
        //public UnityEngine.GameObject gameObjectBattleSystem;

        protected Unit enemy;
        protected Unit player1;
        protected Unit player2;
        protected Unit player3;
        //private BattleSystem battleSystem;
        private BattleState currentState;

        public Boolean unitIsDead = false;

        private Boolean oneTime = false;

        protected void Start ()
        {
            enemy = gameObjectEnemy.GetComponent<Unit>();
            player1 = gameObjectPlayer1.GetComponent<Unit>();
            player2 = gameObjectPlayer2.GetComponent<Unit>();
            player3 = gameObjectPlayer3.GetComponent<Unit>();
            currentState = GameObject.FindObjectOfType<BattleSystem>().state;
        }

        public void FixedUpdate()
        {
            int doOnce = PlayerPrefs.GetInt("behaviorTreeDoOnce");
            currentState = GameObject.FindObjectOfType<BattleSystem>().state;
            if (currentState == BattleState.ENEMYTURN && doOnce == 1)
            {

                // To make the behavior tree run once
                doOnce = 2;
                PlayerPrefs.SetInt("behaviorTreeDoOnce", doOnce);
                enemy = GameObject.Find("BattleStation-Enemy").GetComponentInChildren<Unit>();
                player1 = GameObject.Find("BattleStation-Player1").GetComponentInChildren<Unit>();
                player2 = GameObject.Find("BattleStation-Player2").GetComponentInChildren<Unit>();
                player3 = GameObject.Find("BattleStation-Player3").GetComponentInChildren<Unit>();
                Debug.Log("PLAYER DEBUG INFO: " + player2);
                Debug.Log("Tree node created");

                _root = SetupTree(); // Set up tree

                if (_root != null)
                {
                    Debug.Log("Root is not null");
                    _root.Evaluate();

                }
            }
        }

        protected abstract Node SetupTree();
    }
}