using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordGen : MonoBehaviour
{
    // be careful when changing, it might break if there's not enough words
    [SerializeField] int WordCount = 10;
    [SerializeField] int maxAmountOfType = 2;
    [SerializeField] bool testMode = false;


    public Word[] GenerateWords(Dictionary<WordType, List<Word>> triggerDictionary = null)
    {
        int templateIndex = Random.Range(0, WordBank.SentenceTemplates.Length);
        WordType[] template = WordBank.SentenceTemplates[templateIndex];
        Word[] generatedWords = new Word[WordCount];
        for(int i = 0; i < template.Length; i++)
        { 
            List<Word> wordsOfType = WordBank.wordDictionary[template[i]];
            generatedWords[i] = wordsOfType[Random.Range(0, wordsOfType.Count)];
        }

        if (triggerDictionary != null)
        {
            int amountOfTriggers = Random.Range(1, 3);
            for (int i = template.Length; i < template.Length + amountOfTriggers; i++)
            {
                WordType randomTriggerType = (WordType)Random.Range(0, 4);
                List<Word> wordsOfType = WordBank.wordDictionary[randomTriggerType];
                generatedWords[i] = wordsOfType[Random.Range(0, wordsOfType.Count)];
            }

            for (int i = template.Length + amountOfTriggers; i < generatedWords.Length; i++)
            {
                WordType randomTriggerType = (WordType)Random.Range(0, 4);
                List<Word> wordsOfType = WordBank.wordDictionary[randomTriggerType];
                generatedWords[i] = wordsOfType[Random.Range(0, wordsOfType.Count)];
            }

        }
        else
        {
            for (int i = template.Length; i < generatedWords.Length; i++)
            {
                WordType randomTriggerType = (WordType)Random.Range(0, 4);
                List<Word> wordsOfType = WordBank.wordDictionary[randomTriggerType];
                generatedWords[i] = wordsOfType[Random.Range(0, wordsOfType.Count)];
            }
        }

        return generatedWords;
    }

    // Start is called before the first frame update
    void Start()
    {
		if (!testMode)
		{
			return;
		}
        Word[] words = GenerateWords();
		foreach (Word word in words)
		{
			Debug.Log(word.WordText + " and plural: " + word.WordTextPlural);
		}
	}

}
