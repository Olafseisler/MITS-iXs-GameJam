using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject slotsContainer;
    [SerializeField] WordButton wordbuttonPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        slotsContainer = GameObject.FindWithTag("SlotsContainer");
        wordbuttonPrefab.setWordButtonText(new Word("Yo mama", WordType.Noun));
    }
    
    public Word[] getWords()
	{
        Word[] words = new Word[5];
        Transform[] wordSlots = slotsContainer.GetComponent<SlotsContainer>().wordSlots;

        for (int i = 0; i < wordSlots.Length; i++)
        {
            if (wordSlots[i].childCount > 0) { 
                Transform t = wordSlots[i].GetChild(0);
                words[i] = t.GetComponent<WordButton>().word;
                Debug.Log(words[i].WordText != null ? words[i].WordText : " ");
            }
            
        }

        return words;
    }

    public void checkWordOrder()
	{
        Word[] words = getWords();
	}

    void checkTriggers()
	{

	}
}
