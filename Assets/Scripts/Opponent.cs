using System.Linq;
using System.Collections.Generic;
using UnityEngine;
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

    int pigHealth = 6;
    int horseHealth = 12;
   
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
        health -= 3; //getDamage(sentence);
        if (health <= 0)
		{
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
        return grammarScore;
	}

    public void getResponseFromOpponent()
	{
        setOpponentResponseTextBox("Jobi oled rsk");

    }

    private void setOpponentResponseTextBox(string text)
	{
        responseText.gameObject.SetActive(true);
        responseText.text = text;
	}

    private void animationEnd()
	{
        gameController.opponentLeaveSceneEnded();
	}

    public void switchAnimal()
	{
        health = horseHealth;
        image.sprite = horseImage;
        anim.SetTrigger("EnterScene");
	}
}
