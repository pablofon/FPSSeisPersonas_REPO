using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPadScript : MonoBehaviour
{
    [SerializeField] TMP_Text ans;
    [SerializeField] string answer;
    [SerializeField] float resetTime = 0.5f;

    public void Number(int number)
    {
        ans.text += number.ToString();
    }

    public void EnterNumber()
    {
        if (ans.text == answer)
        {
            ans.text = "Correct";
            GameManager.Instance.correctCode = true;
        }
        else
        {
            ans.text = "Incorrect";
            Invoke(nameof(ResetAns), resetTime);
        }
    }

    public void ClosePad()
    {
        GameManager.Instance.usingKeypad = false;
    }

    public void ResetAns()
    {
        ans.text = "";
    }
}
