using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NumbersManager : MonoBehaviour
{
    #region Singleton
    public static NumbersManager instance;
    public void Awake()
    {
        instance = this;
    }
    #endregion

    public enum Gamemode
    {
        Down, Up
    }

    public Gamemode CurrentGamemode = Gamemode.Down;

    [Header ("Values")]
    public int StartValue;
    public int FinishValue;

    public int[] StartValues;
    public int[] FinishValues;

    [Header("Tools")]
    public GameObject Add;
    public GameObject Subtract;
    public GameObject Multiple;
    public GameObject Divide;

    [Header("Config")]
    public Color ChosenColor;
    public int MinCount;
    public int MaxCount;
    public int MaxValueDown;
    public int MaxValueUp;

    [Header("Numbers Field")]
    public List<NumberView> EnemiesViews = new List<NumberView>();
    public RectTransform EnemiesViewsPanel;

    public NumberView CurrentView;

    public List<NumberView> AlliesViews = new List<NumberView>();
    public RectTransform AlliesViewsPanel;

    public NumberView ViewPrefab;

    public void Start()
    {
        switch(CurrentGamemode)
        {
            case Gamemode.Down:
                GenerateDown();
                break;
            case Gamemode.Up:
                GenerateUp();
                break;
        }
    }

    public void Update()
    {

    }
    #region Views

    public void ResetEnemiesBack()
    {
        foreach(NumberView view in EnemiesViews)
        {
            view.StopAllCoroutines();
            view.ResetBack(); 
        }
    }

    public void ResetAlliesBack()
    {
        foreach (NumberView view in AlliesViews)
        {
            view.StopAllCoroutines();
            view.ResetBack();
        }
    }

    #endregion

    public void OpenTool(GameObject obj)
    {
        if(CurrentView != null)
        {
            obj.SetActive(true);
            obj.GetComponentInChildren<ToolPanel>().Setup(CurrentView.Value);
        }

    }

    public void CreateAllyView(int value)
    {
        NumberView aView = Instantiate<NumberView>(ViewPrefab, AlliesViewsPanel);
        aView.Setup(value, NumberView.ViewType.Ally);
        AlliesViews.Add(aView);
    }

    public void CreateEnemyView(int value)
    {
        NumberView eView = Instantiate<NumberView>(ViewPrefab, EnemiesViewsPanel);
        eView.Setup(value, NumberView.ViewType.Enemy);
        EnemiesViews.Add(eView);
    }

    #region Down

    public void GenerateDown()
    {
        int newMax = Random.Range(MinCount, MaxCount + 1);
        FinishValues = new int[newMax];
        StartValue = 0;
        for(int i = 0; i < newMax; i++)
        {
            int value = Random.Range(1, MaxValueDown);
            FinishValues[i] = value;
            StartValue += value;
            CreateAllyView(value);
        }
        CreateEnemyView(StartValue);
    }

    public void UpdateDown()
    {
     
    }

    #endregion

    public void UpdateUp()
    {
     
    }

    public void GenerateUp()
    {
    
    }

}
