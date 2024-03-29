using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordGen : MonoBehaviour
{
    // be careful when changing, it might break if there's not enough words
    [SerializeField] int WordCount = 10;


    public Word[] GenerateWords(Opponent opponent)
    {
        // Selects random words to add to the word bank based on given sentence template
        int templateIndex = Random.Range(0, WordBank.SentenceTemplates.Length);
        Word randomWord;
        WordType[] template = WordBank.SentenceTemplates[templateIndex];
        Word[] generatedWords = new Word[WordCount];
        List<int> usedIndices = new();
        for(int i = 0; i < template.Length; i++)
        {
            List<Word> wordsOfType = WordBank.wordDictionary[template[i]];
            int randomIndex = Random.Range(0, wordsOfType.Count);
            for (int j = 0; j < 10; j++)
			{
                if (!usedIndices.Contains(randomIndex))
                    break;
                else 
                    randomIndex = Random.Range(0, wordsOfType.Count);
            }
            randomWord = wordsOfType[randomIndex];
            usedIndices.Add(randomIndex);
            generatedWords[i] = randomWord;
        }

        // Selects opponent-based trigger words
        if (opponent.triggerDictionary != null)
        {
            int amountOfTriggers = Random.Range(1, 3);
            for (int i = template.Length; i < template.Length + amountOfTriggers; i++)
            {
                //WordType randomTriggerType = (WordType)Random.Range(0, 4);
                //List<Word> wordsOfType = opponent.triggerDictionary[randomTriggerType];
                List<Word> wordsOfType = opponent.triggerDictionary.ElementAt(Random.Range(0, opponent.triggerDictionary.Count)).Value;
                randomWord = wordsOfType[Random.Range(0, wordsOfType.Count)];

                generatedWords[i] = randomWord;
            }

            for (int i = template.Length + amountOfTriggers; i < generatedWords.Length; i++)
            {
                WordType randomTriggerType = (WordType)Random.Range(0, 4);
                List<Word> wordsOfType = WordBank.wordDictionary[randomTriggerType];
                randomWord = wordsOfType[Random.Range(0, wordsOfType.Count)];
                generatedWords[i] = randomWord;
            }

        }
        else
        {
            for (int i = template.Length; i < generatedWords.Length; i++)
            {
                WordType randomTriggerType = (WordType)Random.Range(0, 4);
                List<Word> wordsOfType = WordBank.wordDictionary[randomTriggerType];
                randomWord = wordsOfType[Random.Range(0, wordsOfType.Count)];
                generatedWords[i] = randomWord;
            }
        }

        generatedWords = new List<Word>(generatedWords).OrderBy(x => Random.value).ToArray();

        return generatedWords;
    }

}
