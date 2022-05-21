using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Opponent : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] GameController gameController;
    [SerializeField] Check_Sentence checkSentence;
    [SerializeField] TMPro.TextMeshProUGUI responseText;
    List<string> ResponseList;
	private void Start()
	{
        responseText.gameObject.SetActive(false);
    }

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
            ResponseList = ReadResponses();
    }

    static List<string> ReadResponses()
    {
        List<string> responses = new();
        foreach (string line in File.ReadLines("Assets/Resources/Opponents/responses.txt"))
        {
            string item = line.Trim();
            if (item.Length > 0)
            {
                responses.Add(item);
            }
        }
        return responses;
    }
    public void getResponseFromOpponent()
    {
        int responseIndex = Random.Range(0, ResponseList.Count);
        setOpponentResponseTextBox(ResponseList[responseIndex]);
        ResponseList.RemoveAt(responseIndex); // remove item, so the responses won't repeat as much
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
    public int TriggerCount(Word[] sentence)
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

    public void doDamage(Word[] sentence)
	{
        health -= getDamage(sentence);
        if (health <= 0)
		{
            gameController.eventOpponentDead();
		}
	}

    private int getDamage(Word[] sentence)
	{
        int grammarScore = 0;
        if (checkSentence.VerifySentence(new List<Word>(sentence)))
		{
            grammarScore += 3;
            grammarScore += TriggerCount(sentence);
		}
        return grammarScore;
	}


    private void setOpponentResponseTextBox(string text)
	{
        responseText.gameObject.SetActive(true);
        responseText.text = text;
	}

}
