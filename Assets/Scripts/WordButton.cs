using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordButton : MonoBehaviour
{
	public Word word { get; set; }
	[SerializeField] TMPro.TextMeshProUGUI buttonText;
	SlotsContainer slotsContainer;
	bool inContainer = false;

	private void Start()
	{
		slotsContainer = GameObject.FindWithTag("SlotsContainer").GetComponent<SlotsContainer>();
	}

	public void setWordButtonText(Word word)
	{
		this.word = word;
		buttonText.text = word.WordText;
	}

	public void sendWordToContainer()
	{
		if (!inContainer)
		{
			slotsContainer.addWordToContainer(transform);
			inContainer = true;
		}
	}
}