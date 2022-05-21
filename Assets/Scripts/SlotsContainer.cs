using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsContainer : MonoBehaviour
{
    static Transform[] wordSlots;

    // Start is called before the first frame update
    void Start()
    {
        wordSlots = new Transform[5];
        for (int i = 0; i < 5; i++)
		{
            wordSlots[i] = transform.GetChild(i);
		}
    }

    public void addWordToContainer(Transform wordButton)
	{
        for (int i = 0; i < wordSlots.Length; i++)
		{
            if (wordSlots[i].childCount == 0)
			{
                wordButton.SetParent(wordSlots[i]);
                wordButton.position = wordSlots[i].position;
                break;
			}
		}
	}
}
