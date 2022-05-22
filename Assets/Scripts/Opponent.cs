using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Image image;
    [SerializeField] GameController gameController;
    [SerializeField] Check_Sentence checkSentence;
    [SerializeField] TMPro.TextMeshProUGUI responseText;
    [SerializeField] public Animator anim;

    [SerializeField] Sprite pigImage;
    [SerializeField] Sprite horseImage;

    static int pigHealth = 3;
    static int horseHealth = 3;
   

    List<string> ResponseList;

    private void Start()
	  {
        responseText.gameObject.SetActive(false);
    }

    public Dictionary<WordType, List<Word>> triggerDictionary = new() {
        { WordType.Verb, new List<Word>() },
        { WordType.Noun, new List<Word>() },
        { WordType.Adjective, new List<Word>() },
        { WordType.Subjective, new List<Word>() },
        { WordType.Conjunction, new List<Word>() },
    };

    public Opponent()
    {
        ResponseList = ReadResponses();
        ReadTriggers("pig");
    }

    void ReadTriggers(string character)
    {
        triggerDictionary = new()
        {
            { WordType.Verb, new List<Word>() },
            { WordType.Noun, new List<Word>() },
            { WordType.Adjective, new List<Word>() },
            { WordType.Subjective, new List<Word>() },
            { WordType.Conjunction, new List<Word>() },
        };
        WordType wordType;
        List<WordType> removeList = new();
        string plural = "";
        string path = Path.ChangeExtension(Path.Combine("TextData/Opponents", character), ".txt");
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
                                triggerDictionary[WordType.Verb].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            case "A":
                            {
                                wordType = WordType.Adjective;
                                triggerDictionary[WordType.Adjective].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            case "N":
                            {
                                wordType = WordType.Noun;
                                triggerDictionary[WordType.Noun].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            case "S":
                            {
                                wordType = WordType.Subjective;
                                triggerDictionary[WordType.Subjective].Add(new Word(data[1], wordType, plural));
                                break;
                            }
                            case "C":
                            {
                                wordType = WordType.Conjunction;
                                triggerDictionary[WordType.Conjunction].Add(new Word(data[1], wordType, plural));
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


        // remove empty keys
        foreach (KeyValuePair<WordType, List<Word>> entry in triggerDictionary)
        {
            if (entry.Value.Count == 0)
            {
                removeList.Add(entry.Key);
            }
        }
        foreach (WordType key in removeList)
        {
            triggerDictionary.Remove(key);
        }

    }

    // check how many triggers a sentence contains (max two)
    public int TriggerCount(Word[] sentence)
    {
        int trigger_count = 0;
        foreach (WordType key in triggerDictionary.Keys)
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
            gameController.opponentsLeft--;
            gameController.eventOpponentDead();
		}
	}

    public void startExitAnimation()
	{
        anim.SetTrigger("LeaveScene");
    }

    private int getDamage(Word[] sentence)
	{
        int grammarScore = 0;
        if (checkSentence.VerifySentence(new List<Word>(sentence)))
		{
            grammarScore += 3;
            grammarScore += TriggerCount(sentence);
		}
        Debug.Log("grammar: " + grammarScore);
        return grammarScore;
	}

    static List<string> ReadResponses()
    {
        List<string> responses = new();
        foreach (string line in File.ReadLines("TextData/Opponents/responses.txt"))
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
        Debug.Log("current response is " + ResponseList[responseIndex]);
        setOpponentResponseTextBox(ResponseList[responseIndex]);
        //ResponseList.RemoveAt(responseIndex); // remove item, so the responses won't repeat as much
    }


    private void setOpponentResponseTextBox(string text)
	{
        responseText.text = text;
        StartCoroutine("handleTextBox");
    }

    private void animationEnd()
	{
        gameController.opponentLeaveSceneEnded();
	}

    public void switchAnimal()
	{
        ReadTriggers("donkey");
        health = horseHealth;
        image.sprite = horseImage;
        anim.SetTrigger("EnterScene");
    }

    public IEnumerator handleTextBox()
    {
        yield return new WaitForSeconds(3.0f);
        responseText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        responseText.gameObject.SetActive(false);
    }
}
