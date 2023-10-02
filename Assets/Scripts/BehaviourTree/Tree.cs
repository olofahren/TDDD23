using System;
using System.Collections;
using System.Collections.Generic;
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
            //battleSystem = gameObjectBattleSystem.GetComponent<BattleSystem>();
            currentState = GameObject.FindObjectOfType<BattleSystem>().state;
            Debug.Log("BattleState: " + currentState);

            _root = SetupTree();
        }

        // Få funktionen att bara köra en gång när vi vill att den ska köras en gång 
        public void FixedUpdate()
        {
            int doOnce = PlayerPrefs.GetInt("behaviorTreeDoOnce");
            currentState = GameObject.FindObjectOfType<BattleSystem>().state;
            if (currentState == BattleState.ENEMYTURN && doOnce == 1)
            {
                // To make the behavior tree run once
                doOnce = 2;
                PlayerPrefs.SetInt("behaviorTreeDoOnce", doOnce);

                Debug.Log("Tree node created");
                if (_root != null)
                {
                    Debug.Log("Root is noy null");
                    _root.Evaluate();

                }
            }
        }

        /*public void GetEvaluate()
        {
            currentState = GameObject.FindObjectOfType<BattleSystem>().state;
            if (currentState == BattleState.ENEMYTURN)
            {
                Debug.Log("Tree node created");
                if (_root != null)
                {
                    _root.Evaluate();
                }
            }

        }*/

        protected abstract Node SetupTree();
    }
}