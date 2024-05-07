using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Text showedWord;
    [SerializeField]
    Text Result;

    [SerializeField]
    Dropdown difficulty;
    [SerializeField]
    InputField inputField;
    [SerializeField]
    Button ResetButton;

    private string target;
    private StringBuilder showWord;
    private int health;
    private GameObject[] parts;
    public void Start()
    {
        changeDifficulty();
        parts = GameObject.FindGameObjectsWithTag("Parts");
        health = parts.Length;
        foreach (GameObject part in parts) 
        {
            part.GetComponent<Renderer>().enabled = false;
        }
        ResetButton.gameObject.SetActive(false);
        Result.text = "";
    }

    public void changeDifficulty()
    {
        if (difficulty.value == 0)
        {
            target = "lion";
            showWord = new StringBuilder("____");
        }
        else
        {
            target = "dermatoglyphics";
            showWord = new StringBuilder("_______________");
        }
        showedWord.text = showWord.ToString();
    }

    public void CheckLetter()
    {
        string trytext = inputField.text.ToLower();
        if (health > 0 && trytext.Length == 1)
        {
            char letter = char.Parse(trytext);
            if (target.Contains(letter)) 
            {
                showWord[target.IndexOf(letter)] = letter;
                showedWord.text = showWord.ToString();
                if (Equals(showWord.ToString(), target))
                {
                    Result.text = "You won!";
                    ResetButton.gameObject.SetActive(true);
                    health = 0;
                }
            }
            else 
            {
                health--;
                parts[health].GetComponent<Renderer>().enabled = true;
                if (health == 0)
                {
                    Result.text = "You lost!";
                    ResetButton.gameObject.SetActive(true);
                }
            }
        }
        inputField.text = "";
    }
}
