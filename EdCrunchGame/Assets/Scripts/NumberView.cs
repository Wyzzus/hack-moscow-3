using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NumberView : MonoBehaviour
{
    public enum ViewType
    {
    Enemy, Ally 
    }

    public ViewType MyType;

    public Text HP;
    public Image BackPanel;
    public Image Icon;
    public int Value;

    public void Setup(int HP, ViewType type)
    {
        this.HP.text = HP.ToString();
        this.MyType = type;
        this.Value = HP;
        ResetValue();
    }

    public void ResetValue()
    {
        HP.text = Value.ToString();
        if(MyType == ViewType.Enemy)
        {
            switch(Value)
            {
                case 1:
                    Icon.color = Color.blue;
                    break;
                case 2:
                    Icon.color = Color.cyan;
                    break;
                case 3:
                    Icon.color = Color.green;
                    break;
                case 4:
                    Icon.color = Color.magenta;
                    break;
                case 5:
                    Icon.color = Color.red;
                    break;
                case 6:
                    Icon.color = Color.yellow;
                    break;
                default:
                    Icon.color = Color.white;
                    break;
            }
        }
    }

    public void ResetBack()
    {
        BackPanel.color = new Color(0, 0, 0, 0);
    }

    public void Choose()
    {
        if(MyType.Equals(ViewType.Enemy))
        {
            StopAllCoroutines();
            switch (MyType)
            {
                case ViewType.Ally:
                    NumbersManager.instance.ResetAlliesBack();
                    break;
                case ViewType.Enemy:
                    NumbersManager.instance.ResetEnemiesBack();
                    break;
            }
            StartCoroutine(Fluct());
        }

    }

    public IEnumerator Fluct()
    {
        NumbersManager.instance.CurrentView = this;
        float delayTime = 0.1f;
        int delayCount = 10;
        WaitForSeconds delay = new WaitForSeconds(delayTime);
        for(float i = delayTime; i <= delayTime * delayCount; i += delayTime)
        {
            yield return delay;
            Color nC = NumbersManager.instance.ChosenColor;
            BackPanel.color = new Color(nC.r, nC.g, nC.b, i);
        }
        //yield return new WaitForSeconds(3f);
        //NumbersManager.instance.CurrentView = null;
        //ResetBack();
    }
}
