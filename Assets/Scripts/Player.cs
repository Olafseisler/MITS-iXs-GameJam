using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    [SerializeField] TMPro.TextMeshProUGUI responseText;
    [SerializeField] int health;
    [SerializeField] public Animator anim;
    [SerializeField] public GameController gameController;
    [SerializeField] public Sprite winningSprite;


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

    private void animationEnd()
	{
        gameController.playerEnterSceneEnded();
	}

    public IEnumerator deactivateTextBox()
	{
        yield return new WaitForSeconds(3.0f);
        responseText.gameObject.SetActive(false);
    }
}
