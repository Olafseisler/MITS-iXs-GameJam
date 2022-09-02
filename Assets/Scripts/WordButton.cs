using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordButton : MonoBehaviour
{
	public Word word { get; set; }
	[SerializeField] TMPro.TextMeshProUGUI buttonText;

	public RandomWordsContainer parentContainer { get; set; }

	public void setWordButtonText(Word word)
	{
		this.word = word;
		buttonText.text = word.WordText;
		buttonText.autoSizeTextContainer = true; // words will go over the box, but it's better than not seeing what it says

	}

    public void sendSelfToInsult()
	{
		parentContainer.sendWordToInsult(this);
		playThisSoundEffect();
	}

	public void playThisSoundEffect()
    {
		AudioManager.instance.PlaySound("ButtonClick");
		Debug.Log("shoulda played sfx");
    }
	
}