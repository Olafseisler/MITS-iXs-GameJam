using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Sentence : MonoBehaviour
{

    List<Word> lause = new();
    public bool Check5WordSentence()
    {
        bool isCorrect = false;
        for (int i = 0; i < lause.Count; i++)
        {
            if (i == 0)
            {
                // WordType template-d peaks üle mõtlema. Siin praegu lambist tehtud.
                if (lause[0].WordType == WordType.Nouns)
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
                if (lause[1].WordType == WordType.Verbs)
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
                if (lause[2].WordType == WordType.Adjectives)
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
                if (lause[3].WordType == WordType.Adjectives)
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
                if (lause[4].WordType == WordType.Nouns)
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
