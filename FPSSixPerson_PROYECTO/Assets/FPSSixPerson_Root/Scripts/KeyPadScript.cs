using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPadScript : MonoBehaviour
{
    [SerializeField] Text ans;

    public void Number(int number)
    {
        ans.text += number.ToString();
    }
}
