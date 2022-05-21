using System.Collections.Generic;
using UnityEngine;

public class Check_Sentence : MonoBehaviour
{
    // checks if the sentence matches a template
    public bool VerifySentence(List<Word> sentence)
    {
        foreach (WordType[] template in WordBank.SentenceTemplates)
        {
            for (int i = 0; i < sentence.Count; i++)
            {
                    // if sentence does not match template
                    if (!(sentence[i].wordType == template[i]))
                    {
                        break;
                    }

                    // if the last word is correct, so is the entire sentence
                    if (i == sentence.Count -1 ) {
                        return true;
                    }
            }
        }
        return false;
    }

    void Start()
    {
        if (Application.isEditor) {
            Debug.Log("--CHECK SENTENCE DEBUG--");
            List<Word> sentence = new();
            sentence.Add(new Word("test", WordType.Noun));
            sentence.Add(new Word("test", WordType.Verb));
            sentence.Add(new Word("test", WordType.Adjective));
            sentence.Add(new Word("test", WordType.Adjective));
            sentence.Add(new Word("test", WordType.Noun));
            Debug.Log("Is sentence NVAAN valid: " + VerifySentence(sentence));
        }
    }
}
