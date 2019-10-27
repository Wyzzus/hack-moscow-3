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

    public enum GameState
    {
        Win, None 
    }

    public GameState CurrentGameState = GameState.None;

    public Gamemode CurrentGamemode = Gamemode.Down;

    [Header ("Values")]
    public int StartValue;
    public int FinishValue;

    public int[] StartValues;
    public int[] FinishValues;

    public int Level = 0;

    [Header("Tools")]
    public GameObject Add;
    public GameObject Subtract;
    public GameObject Multiple;
    public GameObject Divide;

    public GameObject WinScreen;

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
    public NumberView AllyViewPrefab;

    public void Start()
    {
        GenerateLevel();
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
            ActDeactObj(obj);
            obj.GetComponentInChildren<ToolPanel>().Setup(CurrentView.Value);
        }
    }

    public void CreateAllyView(int value)
    {
        NumberView aView = Instantiate<NumberView>(AllyViewPrefab, AlliesViewsPanel);
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

    public void GenerateLevel()
    {
        foreach(NumberView nv in EnemiesViews)
        {
            Destroy(nv.gameObject);
        }
        foreach (NumberView nv in AlliesViews)
        {
            Destroy(nv.gameObject);
        }
        EnemiesViews.Clear();
        AlliesViews.Clear();
        switch (CurrentGamemode)
        {
            case Gamemode.Down:
                GenerateDown();
                break;
            case Gamemode.Up:
                GenerateUp();
                break;
        }
    }

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

    public void Restart()
    {
        foreach(NumberView nv in EnemiesViews)
        {
            Destroy(nv.gameObject);
        }
        EnemiesViews.Clear();
        CreateEnemyView(StartValue);
    }

    public void CheckWin()
    {
        EnemiesViews.Sort(delegate (NumberView x, NumberView y)
        {
            return x.Value.CompareTo(y.Value);
        });

        AlliesViews.Sort(delegate (NumberView x, NumberView y)
        {
            return x.Value.CompareTo(y.Value);
        });

        if(AlliesViews.Count == EnemiesViews.Count)
        {
            int equals = 0;
            for(int i = 0; i < AlliesViews.Count; i++)
            {
                if(AlliesViews[i].Value == EnemiesViews[i].Value)
                //  Debug.Log(AlliesViews[i].Value + " | " + EnemiesViews[i].Value);
                    equals++;
            }
            if(equals == AlliesViews.Count)
            {
                CurrentGameState = GameState.Win;
                WinGame();
            }
            else
            {
                CurrentGameState = GameState.None;
            }
        }
        else
        {
            CurrentGameState = GameState.None;
        }
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

    public void WinGame()
    {
        WinScreen.SetActive(true);
        Level++;
    }

    public void ActDeactObj(GameObject obj)
    {
        if (obj.activeSelf)
            obj.SetActive(false);
        else
            obj.SetActive(true);
    }
    
}
