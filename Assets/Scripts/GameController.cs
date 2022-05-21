using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject slotsContainer;
    [SerializeField] GameObject randomWordsContainer;
    Word[] generatedWords;
    [SerializeField] WordGen wordGen;
    [SerializeField] GameObject wordbuttonPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        generatedWords = wordGen.GenerateWords();
        GenerateWordButtons(generatedWords);
    }
    
    public Word[] getWordsFromSlots()
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

    void GenerateWordButtons(Word[] words)
	{
		foreach (Word w in words)
		{
			GameObject newButton = Instantiate(wordbuttonPrefab);
            newButton.GetComponent<WordButton>().setWordButtonText(w);
            randomWordsContainer.GetComponent<SlotsContainer>().addWordToContainer(newButton.transform);
		}
	}

    public void checkWordOrder()
	{
        Word[] words = getWordsFromSlots();
	}

    void checkTriggers()
	{

	}
}
