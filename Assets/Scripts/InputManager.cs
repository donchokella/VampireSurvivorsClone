using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    void Awake()
    {
            DontDestroyOnLoad(gameObject);
            instance = this;   
    }

    public List<string> playerInputs = new List<string>();
    public TMP_InputField[] inputFields;

    public void GetInputs()
    {
        foreach (TMP_InputField inputField in inputFields)
        {
            playerInputs.Add(inputField.text);
        }
    }
}
