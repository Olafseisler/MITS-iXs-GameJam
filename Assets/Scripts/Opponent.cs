using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Opponent : MonoBehaviour
{
    public List<Word> Triggers { get; set; }
    public Opponent(string character)
    {
            Triggers = ReadTriggers(character);
    }

    public List<Word> ReadTriggers(string character)
    {
        WordType wordType;
        string path = Path.ChangeExtension(Path.Combine("Assets/Resources/Opponents", character), ".txt");
        List<Word> words = new();
        foreach (string line in File.ReadLines(path))
        {
            string[] data = line.Trim().Split(' ');
            if (data[0].Length > 0)
            {
                if (data.Length > 1)
                {            
                        switch (data[0])
                        {
                            case "V":
                                wordType = WordType.Verb;
                                break;
                            case "A":
                            wordType = WordType.Adjective;
                                break;
                            case "N":
                            wordType = WordType.Noun;
                                break;
                            case "S":
                            wordType = WordType.Subjective;
                                break;
                            case "C":
                            wordType = WordType.Conjunction;
                                break;
                            default:
                                // it should never get here
                                Debug.LogError("Invalid type: " + data[0]);
                            wordType = WordType.Noun;
                                break;
                        }
                        words.Add(new Word(data[1], wordType));
                    }
            }
        }
        return words;
    }

    // check if sentence contains a trigger
    public bool HasTrigger(Word[] sentence)
    {
        foreach (Word word in Triggers)
        {
            if (sentence.Contains(word))
            {
                return true;
            }
        }
        return false;
    }

}
