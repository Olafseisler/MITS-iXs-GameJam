using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordButton : MonoBehaviour
{
	SlotsContainer slotsContainer;
	bool inContainer = false;

	private void Start()
	{
		slotsContainer = GameObject.FindWithTag("SlotsContainer").GetComponent<SlotsContainer>();
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