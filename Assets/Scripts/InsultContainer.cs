using UnityEngine;

public class InsultContainer : MonoBehaviour
{
    public GameController gameController;
    public WordButton[] wordButtons { get; set; }
    private int currentInsult = 0;

    // Start is called before the first frame update
    void Start()
    {
        wordButtons = new WordButton[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            wordButtons[i] = transform.GetChild(i).GetComponent<WordButton>();
            wordButtons[i].gameObject.SetActive(false);
        }
    }

    public void addToWordToInsult(Word word)
    {
        wordButtons[currentInsult].setWordButtonText(word);
        wordButtons[currentInsult].gameObject.SetActive(true);
        currentInsult++;

        if(currentInsult == wordButtons.Length)
		{
            gameController.allSlotsFull();
		}
	}

    public Word[] getWords()
	{
        Word[] words = new Word[5];
        for(int i = 0; i < wordButtons.Length; i++)
		{
            words[i] = wordButtons[i].word;
		}
        return words; 
	}


    public void removeOldInsults()
    {
        currentInsult = 0;
        for (int i = 0; i < 5; i++)
        {
            wordButtons[i].gameObject.SetActive(false);
        }
    }


}
