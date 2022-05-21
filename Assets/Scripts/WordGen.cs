using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordGen : MonoBehaviour
{
    // be careful when changing, it might break if there's not enough words
    [SerializeField] int WordCount = 7;
    [SerializeField] int maxAmountOfType = 2;
    [SerializeField] bool testMode = false;


    public Word[] GenerateWords(Dictionary<string, List<Word>> triggerDictionary = null)
    {
        bool is_valid = false;
        int wordIndex;
        Word new_word;
        string prev_trigger_key = null;
        Word[] wordList = new Word[WordCount];
        List<Word> currentList;
        int[] typeCount = { 1, 1, 0, 0, 0 }; // verb, adjective, noun, subjective, conjunction
        
        for (int i=0;i<WordCount; i++)
        {
            // first two are always necessary
            if (i == 0)
            {
                currentList = WordBank.wordDictionary["verbs"];
            }
            else if (i == 1)
            {
                currentList = WordBank.wordDictionary["adjectives"];
            }
            // triggers
            else if (i >= WordCount - 2)
            {
                if (triggerDictionary != null)
                {
                    KeyValuePair<string, List<Word>> randomPair = triggerDictionary.ElementAt(Random.Range(0, WordBank.wordDictionary.Count));
                    while ((randomPair.Key == prev_trigger_key) || (randomPair.Value.Count == 0))
                    {
                        randomPair = triggerDictionary.ElementAt(Random.Range(0, WordBank.wordDictionary.Count));
                    }
                    prev_trigger_key = randomPair.Key;
                    currentList = randomPair.Value;
                }
                else // fallback to random
                {
                    currentList = WordBank.wordDictionary.ElementAt(Random.Range(0, WordBank.wordDictionary.Count)).Value;
                }
            }
            // these can be random, but not more than 2 of each
            else
            {
                KeyValuePair<string, List<Word>> randomPair;
                do
                {
                    randomPair = WordBank.wordDictionary.ElementAt(Random.Range(0, WordBank.wordDictionary.Count));
                    switch (randomPair.Key)
                    {
                        case "verbs":
                            if (typeCount[0] <= maxAmountOfType)
                            {
                                typeCount[0] += 1;
                                is_valid = true;
                            }
                            break;
                        case "adjectives":
                            if (typeCount[1] <= maxAmountOfType)
                            {
                                typeCount[1] += 1;
                                is_valid = true;
                            }
                            break;
                        case "nouns":
                            if (typeCount[2] <= maxAmountOfType)
                            {
                                typeCount[2] += 1;
                                is_valid = true;
                            }
                            break;
                        case "subjectives":
                            if (typeCount[3] <= maxAmountOfType)
                            {
                                typeCount[3] += 1;
                                is_valid = true;
                            }
                            break;
                        case "conjunctions":
                            if (typeCount[4] <= maxAmountOfType)
                            {
                                typeCount[4] += 1;
                                is_valid = true;
                            }
                            break;
                        default:
                            // it shouldn't hit this
                            break;
                    }
                } while (!is_valid);
                currentList = randomPair.Value;
            }
            wordIndex = Random.Range(0, currentList.Count);
            new_word = currentList[wordIndex];
            while (wordList.Contains(new_word))
            {
                wordIndex = Random.Range(0, currentList.Count);
                new_word = currentList[wordIndex];
            }
            wordList[i] = new_word;
        }
        return wordList;
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
