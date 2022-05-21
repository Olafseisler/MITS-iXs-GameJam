using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] InsultContainer insultsContainer;
    [SerializeField] RandomWordsContainer randomWordsContainer;
    [SerializeField] Lifecycle lifecycle;
    Word[] generatedWords;
    [SerializeField] GameObject wordbuttonPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        this.insultsContainer.gameController = this;
        randomWordsContainer.gameController = this;
    }
    


    public void genererateRandomWords()
	{
        randomWordsContainer.GetComponent<RandomWordsContainer>().generateNewWords();
        
	}

    public void checkWordOrder()
	{

	}

    public void allSlotsFull()
	{
        lifecycle.doTransition();
    }


    public void sendWordToInsultContainer(Word word)
	{
        this.insultsContainer.addToWordToInsult(word);
	}

    void checkTriggers()
	{

	}
}
