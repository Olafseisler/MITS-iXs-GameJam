using UnityEngine;
using System.IO;
using System.Linq;

public class WordBank : MonoBehaviour
{
    public Word[] Verbs { get; set; }
    public Word[] Nouns { get; set; }
    public Word[] Adjectives { get; set; }
    static Word[] ReadWords(string path, WordType type)

    {
        var lineCount = File.ReadLines(path).Count();
        Word[] words = new Word[lineCount];
        int i = 0;
        foreach (string line in File.ReadLines(path))
        {
            System.Console.WriteLine(line);
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

        // print all as a test
        Debug.Log("--VERBS--");
        foreach (Word word in Verbs)
        {
            // Do something with line
            Debug.Log(word.WordText);

        }

        Debug.Log("--NOUNS--");
        foreach (Word word in Nouns)
        {
            // Do something with line
            Debug.Log(word.WordText);

        }

        Debug.Log("--ADJECTIVES--");
        foreach (Word word in Adjectives)
        {
            // Do something with line
            Debug.Log(word.WordText);

        }
    }

}