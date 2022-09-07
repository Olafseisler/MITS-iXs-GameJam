using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    [SerializeField] TMPro.TextMeshProUGUI responseText;
    [SerializeField] int health;
    public Animator anim;
    public GameController gameController;
    public Sprite winningSprite;


    private void Start()
    {
        responseText.gameObject.SetActive(false);
    }

    public void getResponseFromPlayer(Word[] words)
	{
        responseText.gameObject.SetActive(true);
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
        StartCoroutine("deactivateTextBox");
    }

#pragma warning disable IDE0051 // Remove unused private members
    private void animationEnd()
#pragma warning restore IDE0051 // Remove unused private members
	{
        gameController.playerEnterSceneEnded();
	}

    public IEnumerator deactivateTextBox()
	{
        yield return new WaitForSeconds(3.0f);
        responseText.gameObject.SetActive(false);
    }
}
