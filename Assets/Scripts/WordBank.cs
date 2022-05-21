using UnityEngine;
using System.IO;
using System.Linq;

public class WordBank : MonoBehaviour
{
    public Word[] Verbs { get; set; }
    public Word[] Nouns { get; set; }
    public Word[] Adjectives { get; set; }
    public Word[] Subjectives { get; set; }
    public Word[] Conjunctions { get; set; }
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

    // Start is called before the first frame update
    void Start()
    {
        Verbs = ReadWords("Assets/Resources/verbs.txt", WordType.Verb);
        Nouns = ReadWords("Assets/Resources/nouns.txt", WordType.Noun);
        Adjectives = ReadWords("Assets/Resources/adjectives.txt", WordType.Adjective);
        Subjectives = ReadWords("Assets/Resources/subjectives.txt", WordType.Subjective);
        Conjunctions = ReadWords("Assets/Resources/conjunctions.txt", WordType.Conjunction);

        // print all as a test
        Debug.Log("--VERBS--");
        foreach (Word word in Verbs)
        {    
            Debug.Log(word.WordText);
        }

        Debug.Log("--NOUNS--");
        foreach (Word word in Nouns)
        {    
            Debug.Log(word.WordText);
        }

        Debug.Log("--ADJECTIVES--");
        foreach (Word word in Adjectives)
        {      
            Debug.Log(word.WordText);
        }

        Debug.Log("--SUBJECTIVES--");
        foreach (Word word in Subjectives)
        {     
            Debug.Log(word.WordText);
        }

        Debug.Log("--CONJUNCTIONS--");
        foreach (Word word in Conjunctions)
        {          
            Debug.Log(word.WordText);
        }
    }

}