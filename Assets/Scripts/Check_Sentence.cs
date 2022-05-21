using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Sentence : MonoBehaviour
{

    Word[] sentence;
    public bool Check5WordSentence()
    {
        bool isCorrect = false;
        for (int i = 0; i < sentence.Length; i++)
        {
            if (i == 0)
            {
                // WordType template-d peaks �le m�tlema. Siin praegu lambist tehtud.
                if (sentence[0].WordType == WordType.Noun)
                {
                    isCorrect = true;
                }
                else
                {
                    isCorrect = false;
                    break;
                }
            }
            else if (i == 1)
            {
                if (sentence[1].WordType == WordType.Verb)
                {
                    isCorrect = true;
                }
                else
                {
                    isCorrect = false;
                    break;
                }
            }
            else if (i == 2)
            {
                if (sentence[2].WordType == WordType.Adjective)
                {
                    isCorrect = true;
                }
                else
                {
                    isCorrect = false;
                    break;
                }
            }
            else if (i == 3)
            {
                if (sentence[3].WordType == WordType.Adjective)
                {
                    isCorrect = true;
                }
                else
                {
                    isCorrect = false;
                    break;
                }
            }
            else if (i == 4)
            {
                if (sentence[4].WordType == WordType.Noun)
                {
                    isCorrect = true;
                }
                else
                {
                    isCorrect = false;
                    break;
                }
            }
        }
        return isCorrect;
    }

    public int checkWordOrder()
	{
        int score = 0;

        if (sentence[0].WordType != WordType.Verb || sentence[0].WordType != WordType.Noun)
		{
            score--;
		}

        return score;
	}

    public int checkTriggers(Opponent opponent)
	{
        int score = 0;
        for (int i = 0; i < sentence.Length; i++)
		{
            for (int j = 0; i < opponent.triggers.Length; i++)
			{
                if (sentence[i].Equals(opponent.triggers[j]))
				{
                    score++;
				}
			}
		}

        return score;
	}

}
