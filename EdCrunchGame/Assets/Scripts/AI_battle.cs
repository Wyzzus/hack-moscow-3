using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_battle : MonoBehaviour
{
    public Generate GenerateScript;
    public int CountEnemy;
    public int CountSoldier;
    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(CountEnemy);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "Enemy" && other.tag == "Soldier")
        {
            if (AI.instance.ListOfClones.Soldiers.Contains(other.gameObject))
                AI.instance.ListOfClones.Soldiers.Remove(other.gameObject);
            if (AI.instance.ListOfClones.Enemies.Contains(other.gameObject))
                AI.instance.ListOfClones.Enemies.Remove(other.gameObject);
            Destroy(other.gameObject);
            //CountSoldier--;
        }
        if (this.gameObject.tag == "Soldier" && other.tag == "Enemy")
        {
            if (AI.instance.ListOfClones.Soldiers.Contains(other.gameObject))
                AI.instance.ListOfClones.Soldiers.Remove(other.gameObject);
            if (AI.instance.ListOfClones.Enemies.Contains(other.gameObject))
                AI.instance.ListOfClones.Enemies.Remove(other.gameObject);
            Destroy(other.gameObject);
            //CountEnemy--;
        }
    }
}
