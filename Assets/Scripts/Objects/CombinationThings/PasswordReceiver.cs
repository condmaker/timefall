using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordReceiver : MonoBehaviour
{
    private bool sucess;

    [SerializeField]
    private string password;

    [SerializeField]
    private string input;
    public string Input { get => input; }

    private bool ComparePassword(string password, string input)
    {
        if (password == input)
            return true;
        return false;
    }

    private void OnRecieveInput()
    {
        if (password.Length == input.Length)
            sucess = ComparePassword(password, input);
        /*if (sucess == true)
            open door
        else
        {
            input = "";
            reset states to  0;
        }*/
    }
}
