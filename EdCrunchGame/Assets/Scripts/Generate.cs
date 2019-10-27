using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generate : MonoBehaviour
{
    public float BiasSoldierX = 15f;
    public float BiasSoldierZ = 5f;
    public float BiasEnemiesX = 15f;
    public float BiasEnemiesZ = -5f;
    public float BiasArmy = 3f;
    public GameObject Enemy;
    public GameObject Soldier;
    public List<GameObject> Enemies = new List<GameObject>();
    public List<GameObject> Soldiers = new List<GameObject>();
    public Text CountSoldier;
    public Text CountEnemy;
    public int PopulationArmyEnemy = 50;
    public int PopulationArmySoldier = 10;
    public AI_battle ScriptBattle;
    public Slider Power;
    public int MaxValueOfPower = 300;
    public int MinValueOfPower = 0;
    public int CostALot = 120;
    public int CostALow = 20;
    void Start()
    {
        Power.maxValue = MaxValueOfPower;
        Power.minValue = MinValueOfPower;
        PlusEnemies(PopulationArmyEnemy);
        PlusSoldiers(PopulationArmySoldier);
        Power.value += 40;
        ScriptBattle.CountEnemy = PopulationArmyEnemy;
        ScriptBattle.CountSoldier = PopulationArmySoldier;
    }

    void Update()
    {
        OutPutCountArmy();
    }

    public void OutPutCountArmy()
    {
        CountSoldier.text = System.Convert.ToString(ScriptBattle.CountSoldier);
        CountEnemy.text = System.Convert.ToString(ScriptBattle.CountEnemy);
    }

    public void PlusEnemies(int count)
    {
        if (CostALow <= Power.value)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(Enemy, Vector3.zero + new Vector3((Enemies.Count % 10) * BiasArmy - BiasEnemiesX, Enemy.transform.localScale.y / 2, 0 - (BiasEnemiesZ + Enemies.Count / 10 * 1.5f)), Quaternion.Euler(0, 0, 0));
                Enemies.Add(clone);

            }
            Power.value -= CostALow;
        }
    }
    public void MinusEnemies(int count)
    {
        if (CostALow <= Power.value)
        {
            for (int i = 0; i < count; i++)
            {
                if (Enemies.Count > 0)
                {
                    GameObject enemy = Enemies[Enemies.Count - 1];
                    Enemies.Remove(enemy);
                    Destroy(enemy);
                }
            }
            Power.value -= CostALow;
        }      
    }
    public void MultipleEnemies()
    {
        if (CostALot <= Power.value)
        {
            int Count = Enemies.Count;
            for (int j = 0; j < Count; j++)
            {
                GameObject clone = Instantiate(Enemy, Vector3.zero + new Vector3((Enemies.Count % 10) * BiasArmy - BiasEnemiesX, 0.5f, 0 - (BiasEnemiesZ + Enemies.Count / 10 * 1.5f)), Quaternion.Euler(0, 0, 0));
                Enemies.Add(clone);
            }
            Power.value -= CostALot;
        }
        
    }
    public void DivEnemies()
    {
        if (CostALot <= Power.value)
        {
            int Count = Enemies.Count;
            for (int i = 0; i < Count / 2; i++)
            {
                if (Enemies.Count > 0)
                {
                    GameObject enemy = Enemies[Enemies.Count - 1];
                    Enemies.Remove(enemy);
                    Destroy(enemy);
                }
            }
            if (Enemies.Count > 0) ScriptBattle.CountEnemy /= 2;
            Power.value -= CostALot;
        }
    }
    public void PlusSoldiers(int count)
    {
        if (CostALow <= Power.value)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(Soldier, Vector3.zero + new Vector3((Soldiers.Count % 10) * BiasArmy - BiasSoldierX, 0.5f, 0 - (BiasSoldierZ + Soldiers.Count / 10 * 1.5f)), Quaternion.Euler(0, 0, 0));
                Soldiers.Add(clone);
            }
            ScriptBattle.CountSoldier += 2;
            Power.value -= CostALow;
        }
        
    }
    public void MinusSoldiers(int count)
    {
        if (CostALow <= Power.value)
        {
            for (int i = 0; i < count; i++)
            {
                if (Soldiers.Count > 0)
                {
                    GameObject soldier = Soldiers[Soldiers.Count - 1];
                    Debug.Log(soldier.name);
                    Soldiers.Remove(soldier);
                    Destroy(soldier);
                }
            }
            ScriptBattle.CountSoldier -= 2;
            Power.value -= CostALow;
        }
        
    }
    public void MultipleSoldiers()
    {
        if (CostALot <= Power.value)
        {
            int Count = Soldiers.Count;
            for (int j = 0; j < Count; j++)
            {
                GameObject clone = Instantiate(Soldier, Vector3.zero + new Vector3((Soldiers.Count % 10) * BiasArmy - BiasSoldierX, 0.5f, 0 - (BiasSoldierZ + Soldiers.Count / 10 * 1.5f)), Quaternion.Euler(0, 0, 0));
                Soldiers.Add(clone);
            }
            ScriptBattle.CountSoldier *= 2;
            Power.value -= CostALot;
        }
        
    }
    public void DivSoldiers()
    {
        if (CostALot <= Power.value)
        {
            int Count = Soldiers.Count;
            for (int i = 0; i < Count / 2; i++)
            {
                if (Soldiers.Count > 0)
                {
                    GameObject soldier = Soldiers[Soldiers.Count - 1];
                    Debug.Log(soldier.name);
                    Soldiers.Remove(soldier);
                    Destroy(soldier);
                }
            }
            if (Soldiers.Count > 0)
                ScriptBattle.CountSoldier /= 2;
            Power.value -= CostALot;
        }
        
    }
}
