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
	}

	public void sendSelfToInsult()
	{
		this.parentContainer.sendWordToInsult(this);
	}

	
}