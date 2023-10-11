using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
public class WordBank : MonoBehaviour
{
    [SerializeField] bool testMode = false;
    [SerializeField] public TextData textData;
    public static Dictionary<WordType, List<Word>> wordDictionary = new() {
        { WordType.Verb, new List<Word>() },
        { WordType.Noun, new List<Word>() },
        { WordType.Adjective, new List<Word>() },
        { WordType.Subjective, new List<Word>() },
        { WordType.Conjunction, new List<Word>() },
    };
    public static WordType[][] SentenceTemplates { get; set; }

    public static WordBank instance;

    static List<Word> ReadWords(string textData, WordType type)
    {
        List<Word> words = new();
        int i = 0;
        foreach (string line in textData.Split("\n"))
        {
            string[] data = line.Trim().Split(':');
            if (data[0].Length > 0) {
            // has plural form
                if (data.Length > 1)
                {
                    words.Add(new Word(data[0], type, data[1]));
                }
            // no plural form
                else { 
                    words.Add(new Word(data[0], type));
                }
            }
            i++;
        }
        return words;
    }

    static WordType[][] ReadTemplate(string textData)
    {
        int i = 0;
        int o;
        var lineCount = textData.Split("\n").Count();
        WordType[][] sentences = new WordType[lineCount][];
        foreach (string temp in textData.Split("\n"))
        {
            string line = temp.Trim();
            WordType[] words = new WordType[5];
            o = 0;
            foreach (char type in line.Split(' ')[0])
            {
                switch (type)
                {
                    case 'V':
                        words[o] = WordType.Verb;
                        break;
                    case 'A':
                        words[o] = WordType.Adjective;
                        break;
                    case 'N':
                        words[o] = WordType.Noun;
                        break;
                    case 'S':
                        words[o] = WordType.Subjective;
                        break;
                    case 'C':
                        words[o] = WordType.Conjunction;
                        break;
                    case ' ':
                        break;
                    default:
                        // it should never get here
                        Debug.LogWarning("Invalid type: " + type);
                        break;
                }
                o++;
            }
            sentences[i] = words;
            i++;
        }
        return sentences;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        wordDictionary = new Dictionary<WordType, List<Word>>()
        {
            {WordType.Verb, ReadWords(textData.verbs, WordType.Verb) },
            {WordType.Noun,  ReadWords(textData.nouns, WordType.Noun) },
            {WordType.Adjective,  ReadWords(textData.adjectives, WordType.Adjective) },
            {WordType.Subjective,  ReadWords(textData.subjectives, WordType.Subjective) },
            {WordType.Conjunction,  ReadWords(textData.conjunctions, WordType.Conjunction) },
        };
        SentenceTemplates = ReadTemplate(textData.templates);
        // print all as a test
        if (!testMode)
        {
            return;
        }

        Debug.Log("--VERBS--");
        foreach (Word word in wordDictionary[WordType.Verb])
        {
            Debug.Log(word.WordText + " and plural: " + word.WordTextPlural);
        }

        Debug.Log("--NOUNS--");
        foreach (Word word in wordDictionary[WordType.Noun])
        {
            Debug.Log(word.WordText + " and plural: " + word.WordTextPlural);
        }

        Debug.Log("--ADJECTIVES--");
        foreach (Word word in wordDictionary[WordType.Adjective])
        {
            Debug.Log(word.WordText + " and plural: " + word.WordTextPlural);
        }

        Debug.Log("--SUBJECTIVES--");
        foreach (Word word in wordDictionary[WordType.Subjective])
        {
            Debug.Log(word.WordText + " and plural: " + word.WordTextPlural);
        }

        Debug.Log("--CONJUNCTIONS--");
        foreach (Word word in wordDictionary[WordType.Conjunction])
        {
            Debug.Log(word.WordText + " and plural: " + word.WordTextPlural);
        }
    }

}