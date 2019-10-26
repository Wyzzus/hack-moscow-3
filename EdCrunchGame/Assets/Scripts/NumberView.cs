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
    public int Value;

    public void Setup(int HP, ViewType type)
    {
        this.HP.text = HP.ToString();
        this.MyType = type;
        this.Value = HP;
    }

    public void ResetValue()
    {
        HP.text = Value.ToString();
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
