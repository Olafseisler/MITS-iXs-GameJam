using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
public class WordBank : MonoBehaviour
{
    [SerializeField] bool testMode = false;

    public static Dictionary<WordType, List<Word>> wordDictionary = new() {
        { WordType.Verb, new List<Word>() },
        { WordType.Noun, new List<Word>() },
        { WordType.Adjective, new List<Word>() },
        { WordType.Subjective, new List<Word>() },
        { WordType.Conjunction, new List<Word>() },
    };
    public static WordType[][] SentenceTemplates { get; set; }

    static List<Word> ReadWords(string path, WordType type)
    {
        List<Word> words = new();
        int i = 0;
        foreach (string line in File.ReadLines(path))
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

    static WordType[][] ReadTemplate(string path)
    {
        int i = 0;
        int o;
        var lineCount = File.ReadLines(path).Count();
        WordType[][] sentences = new WordType[lineCount][];
        foreach (string line in File.ReadLines(path))
        {
            WordType[] words = new WordType[5];
            o = 0;
            foreach (char type in line.Split(' ')[0])
            {
                Debug.Log(type);
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
                    default:
                        // it should never get here
                        Debug.LogError("Invalid type: " + type);
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
    public WordBank()
    {
        wordDictionary = new Dictionary<WordType, List<Word>>()
        {
            {WordType.Verb, ReadWords("Assets/Resources/verbs.txt", WordType.Verb) },
            {WordType.Noun,  ReadWords("Assets/Resources/nouns.txt", WordType.Noun) },
            {WordType.Adjective,  ReadWords("Assets/Resources/adjectives.txt", WordType.Adjective) },
            {WordType.Subjective,  ReadWords("Assets/Resources/subjectives.txt", WordType.Subjective) },
            {WordType.Conjunction,  ReadWords("Assets/Resources/conjunctions.txt", WordType.Conjunction) },
        };
        SentenceTemplates = ReadTemplate("Assets/Resources/templates.txt");
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