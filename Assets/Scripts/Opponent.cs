using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Opponent : MonoBehaviour
{
    public Dictionary<string, List<Word>> triggerDictionary = new()
    {
        { "verbs", new List<Word>() },
        { "nouns", new List<Word>() },
        { "adjectives", new List<Word>() },
        { "subjectives", new List<Word>() },
        { "conjunctions", new List<Word>() },
    };

    public Opponent(string character)
    {
            ReadTriggers(character);
    }

    void ReadTriggers(string character)
    {
        WordType wordType;
        string plural = "";
        string path = Path.ChangeExtension(Path.Combine("Assets/Resources/Opponents", character), ".txt");
        foreach (string line in File.ReadLines(path))
        {
            string[] data = line.Trim().Split(':');
            if (data[0].Length > 0)
            {
                if (data.Length > 1)
                {            
                    if (data.Length > 2)
                    {
                        plural = data[2];
                    }
                        switch (data[0])
                        {
                            case "V":
                            {
                                wordType = WordType.Verb;
                                triggerDictionary["verbs"].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            case "A":
                            {
                                wordType = WordType.Adjective;
                                triggerDictionary["adjectives"].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            case "N":
                            {
                                wordType = WordType.Noun;
                                triggerDictionary["nouns"].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            case "S":
                            {
                                wordType = WordType.Subjective;
                                triggerDictionary["subjectives"].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            case "C":
                            {
                                wordType = WordType.Conjunction;
                                triggerDictionary["conjunctions"].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            default:
                            {
                                // it should never get here
                                Debug.LogError("Invalid type: " + data[0]);
                                wordType = WordType.Noun;
                                break;
                            }
                        }
                    }
            }
        }
    }

    // check how many triggers a sentence contains (max two)
    public int HasTrigger(Word[] sentence)
    {
        int trigger_count = 0;
        foreach (string key in triggerDictionary.Keys)
        {
            foreach (Word word in triggerDictionary[key])
            {
                if (sentence.Contains(word))
                {
                    trigger_count += 1;
                    if (trigger_count == 2)
                    {
                        return trigger_count;
                    }
                }
            }
        }
        return trigger_count;
    }

}
