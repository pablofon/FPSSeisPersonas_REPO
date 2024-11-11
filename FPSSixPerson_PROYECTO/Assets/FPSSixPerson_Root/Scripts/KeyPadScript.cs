using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPadScript : MonoBehaviour
{
    Audio_Manager audiomanager;
    [SerializeField] TMP_Text ans;
    [SerializeField] string answer;
    [SerializeField] float resetTime = 0.5f;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Manager>();
    }

    public void Number(int number)
    {
        ans.text += number.ToString();
        audiomanager.PlaySFX(audiomanager.Buttons);
    }

    public void EnterNumber()
    {
        if (ans.text == answer)
        {
            ans.text = "Correct";
            GameManager.Instance.correctCode = true;
            audiomanager.PlaySFX(audiomanager.correct);
        }
        else
        {
            ans.text = "Incorrect";
            Invoke(nameof(ResetAns), resetTime);
            audiomanager.PlaySFX(audiomanager.Incorrect);
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
