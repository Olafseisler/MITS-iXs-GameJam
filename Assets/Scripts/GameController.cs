using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject[] wordSlots;

    // Start is called before the first frame update
    void Start()
    {
        wordSlots = new GameObject[5];

    }
    
    Word[] getWords()
	{
        Word[] words = new Word[5];

        for (int i = 0; i < wordSlots.Length; i++)
        {
            // TODO: Extract the word from word slots 
        }

        return words;
    }

    public void checkWordOrder(Word[] words)
	{
        
	}

    void checkTriggers()
	{

	}
}
