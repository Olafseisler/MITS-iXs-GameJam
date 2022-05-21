using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsContainer : MonoBehaviour
{
    public Transform[] wordSlots { get; set; }

    [SerializeField] int numberOfWords = 5;

    // Start is called before the first frame update
    void Awake()
    {
        wordSlots = new Transform[numberOfWords];
        for (int i = 0; i < wordSlots.Length; i++)
        {
            wordSlots[i] = transform.GetChild(i);
        }
    }

    public void addWordToContainer(Transform wordButton)
	{
        for (int i = 0; i < wordSlots.Length; i++)
		{
            if (wordSlots[i].childCount ==  0)
			{
                wordButton.SetParent(wordSlots[i]);
                wordButton.position = wordSlots[i].position;
                break;
			}
		}
	}
}
