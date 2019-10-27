using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public static AI instance;

    public void Awake()
    {
        instance = this;
    }


    public NavMeshAgent AgentE;
    public NavMeshAgent AgentS;
    public bool AllowBattle = false;
    public Generate ListOfClones;
    private Vector3 BattlePoint = Vector3.zero;
    public GameObject canvas;
    public Canvas Information;
    public Text EnemiesCount;
    public Text AlliesCount;
    public string Result;
    void Start()
    {
        AgentE = ListOfClones.Enemy.GetComponentInChildren<NavMeshAgent>();
        AgentS = ListOfClones.Soldier.GetComponentInChildren<NavMeshAgent>();
    }

    void Update()
    {
        if (AllowBattle)
        {
            foreach (GameObject agent in ListOfClones.Enemies)
            {
                agent.GetComponentInChildren<NavMeshAgent>().destination = BattlePoint;
            }
            foreach (GameObject agent in ListOfClones.Soldiers)
            {
                agent.GetComponentInChildren<NavMeshAgent>().destination = BattlePoint;
            }
        }

        int eC = ListOfClones.Enemies.Count;
        int aC = ListOfClones.Soldiers.Count;
        EnemiesCount.text = "Разбойники: " + eC;
        AlliesCount.text = "Армия Волшебника: " + aC;

        if (aC == 0 || eC == 0)
        {
            if (aC == 0 && eC > 0)
            {
                Result = "Lose";
            }
            if (eC == 0 && aC > 0)
            {
                Result = "Win";
            }
        }

        
    }
    public void Begin()
    {
        AllowBattle = true;
        canvas.SetActive(false);
    }
    public void GotoBattle()
    {
        Information.enabled = false;
        canvas.SetActive(true);
    }
}
