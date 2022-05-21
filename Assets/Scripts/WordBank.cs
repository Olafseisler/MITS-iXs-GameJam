using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
public class WordBank : MonoBehaviour
{
    public Dictionary<string, Word[]> wordDictionary = new()
    {
        {"verbs", new Word[] {} },
        {"nouns", new Word[] {} },
        {"adjectives", new Word[] {} },
        {"subjectives", new Word[] {} },
        {"conjunctions", new Word[] {} },
    };
    public static WordType[][] SentenceTemplates { get; set; }

    static Word[] ReadWords(string path, WordType type)
    {
        var lineCount = File.ReadLines(path).Count();
        Word[] words = new Word[lineCount];
        int i = 0;
        foreach (string line in File.ReadLines(path))
        {
            words[i] = new Word(line, type);
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
            WordType[] words = new WordType[line.Length];
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
    void Awake()
    {
        wordDictionary = new Dictionary<string, Word[]>()
        {
            {"verbs", ReadWords("Assets/Resources/verbs.txt", WordType.Verb) },
            {"nouns",  ReadWords("Assets/Resources/nouns.txt", WordType.Noun) },
            {"adjectives",  ReadWords("Assets/Resources/adjectives.txt", WordType.Adjective) },
            {"subjectives",  ReadWords("Assets/Resources/subjectives.txt", WordType.Subjective) },
            {"conjunctions",  ReadWords("Assets/Resources/conjunctions.txt", WordType.Conjunction) },
        };
        SentenceTemplates = ReadTemplate("Assets/Resources/templates.txt");
        // print all as a test if running in editor
        if (!Application.isEditor)
        {
            return;
        }

        Debug.Log("--VERBS--");
        foreach (Word word in wordDictionary["verbs"])
        {
            Debug.Log(word.WordText);
        }

        Debug.Log("--NOUNS--");
        foreach (Word word in wordDictionary["nouns"])
        {
            Debug.Log(word.WordText);
        }

        Debug.Log("--ADJECTIVES--");
        foreach (Word word in wordDictionary["adjectives"])
        {
            Debug.Log(word.WordText);
        }

        Debug.Log("--SUBJECTIVES--");
        foreach (Word word in wordDictionary["subjectives"])
        {
            Debug.Log(word.WordText);
        }

        Debug.Log("--CONJUNCTIONS--");
        foreach (Word word in wordDictionary["conjunctions"])
        {
            Debug.Log(word.WordText);
        }
    }

}