using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] TMPro.TextMeshProUGUI responseText;
    [SerializeField] int health;



    private void Start()
    {
        responseText.gameObject.SetActive(false);
    }

    public void getResponseFromPlayer(Word[] words)
	{
        string response = string.Empty;
        foreach(Word word in words)
		{
            response += word.WordText + " ";
		}
        setOpponentResponseTextBox(response);
	}

    private void setOpponentResponseTextBox(string text)
    {
        responseText.gameObject.SetActive(true);
        responseText.text = text;
    }


}
