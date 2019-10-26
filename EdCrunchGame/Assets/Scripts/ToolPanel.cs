using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolPanel : MonoBehaviour
{
    public enum ToolType
    {
    Add, Subtract, Multiple, Divide 
    }
    public ToolType MyType;
    public string ToolPrefix;
    public string ToolPostfix;
    public InputField InputNumber;
    public Text ToolTitle;
    public int OperNumber;

    public void Awake()
    {
        Setup(NumbersManager.instance.CurrentView.Value);
    }

    public void Setup(int number)
    {
        ToolTitle.text = ToolPrefix + " " + number + " " + ToolPostfix;
        //OperNumber = number;
    }

    public void Cancel()
    {
        NumbersManager.instance.ResetEnemiesBack();
        NumbersManager.instance.CurrentView = null;
        gameObject.SetActive(false); 
    }

    public void CompleteOperation()
    {
        switch(MyType)
        {
            case ToolType.Add:
                Add();
                break;
            case ToolType.Subtract:
                Subtract();
                break;
            case ToolType.Multiple:
                Multiple();
                break;
            case ToolType.Divide:
                Divide();
                break;
        }
    }

    #region Operators


    public void SetOperNumber()
    {
        OperNumber = int.Parse(InputNumber.text);
    }

    public void Add()
    {
        SetOperNumber();
        if (NumbersManager.instance.CurrentView != null)
        {
            NumbersManager.instance.CurrentView.Value += OperNumber;
            NumbersManager.instance.CurrentView.ResetValue();
        }
    }

    public void Subtract()
    {
        SetOperNumber();
        if (NumbersManager.instance.CurrentView != null)
        {
            NumbersManager.instance.CurrentView.Value -= OperNumber;
            NumbersManager.instance.CurrentView.ResetValue();
            if (OperNumber > 0)
            {
                NumbersManager.instance.CreateEnemyView(OperNumber);
            }
        }

    }

    public void Multiple()
    {
        SetOperNumber();
        if (NumbersManager.instance.CurrentView != null)
        {
            NumbersManager.instance.CurrentView.Value *= OperNumber;
            NumbersManager.instance.CurrentView.ResetValue();
            if (OperNumber > 0)
            {
                NumbersManager.instance.CreateEnemyView(OperNumber);
            }
            else
            {
                NumbersManager.instance.EnemiesViews.Remove(NumbersManager.instance.CurrentView);
                Destroy(NumbersManager.instance.CurrentView.gameObject);
            }
        }

    }

    public void Divide()
    {
        SetOperNumber();
        if (NumbersManager.instance.CurrentView != null)
        {
            if(NumbersManager.instance.CurrentView.Value % OperNumber == 0)
            {
                NumbersManager.instance.CurrentView.Value /= OperNumber;
                NumbersManager.instance.CurrentView.ResetValue();
                if (OperNumber > 0)
                {
                    NumbersManager.instance.CreateEnemyView(OperNumber);
                }
                else
                {
                    NumbersManager.instance.EnemiesViews.Remove(NumbersManager.instance.CurrentView);
                    Destroy(NumbersManager.instance.CurrentView.gameObject);
                }
            }
        }

    }

    #endregion
}
