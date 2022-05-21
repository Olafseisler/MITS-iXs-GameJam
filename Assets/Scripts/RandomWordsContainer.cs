using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWordsContainer : MonoBehaviour
{
    [SerializeField] GameObject randomWordsPanel;
    private int childrenCount;
    public WordButton[] wordButtons { get; set; }

    [SerializeField] WordGen wordGen;
    public GameController gameController;


    // Start is called before the first frame update
    void Awake()
    {
        childrenCount = randomWordsPanel.transform.childCount;
        wordButtons = new WordButton[childrenCount];

        for (int i = 0; i < childrenCount; i++)
        {
            wordButtons[i] = randomWordsPanel.transform.GetChild(i).GetComponent<WordButton>();
            wordButtons[i].gameObject.SetActive(false);
            wordButtons[i].parentContainer = this;
        }
    }


    public void generateNewWords()
    {
        Word[] generatedWords = wordGen.GenerateWords();
        Debug.Log(wordButtons.Length);
        for(int i = 0; i < generatedWords.Length; i++) 
		{

            wordButtons[i].GetComponent<WordButton>().setWordButtonText(generatedWords[i]);
            wordButtons[i].gameObject.SetActive(true);
		}
    }

    public void sendWordToInsult(WordButton button)
	{
        button.gameObject.SetActive(false);
        gameController.sendWordToInsultContainer(button.word);
	}

    public void removeOldWords()
    {
        for(int i = 0; i < childrenCount; i++)
		{
            wordButtons[i].gameObject.SetActive(false);
		}
    }


}
