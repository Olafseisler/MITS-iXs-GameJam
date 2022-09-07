using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CheckSentence : MonoBehaviour
{
    // checks if the sentence matches a template
    public bool VerifySentence(List<Word> sentence)
    {
        WordType[] words = {sentence[0].wordType, sentence[1].wordType, sentence[2].wordType, sentence[3].wordType, sentence[4].wordType};
        Debug.Log((words[0], words[1], words[2], words[3], words[4]));
        foreach (WordType[] template in WordBank.SentenceTemplates)
        {
            if (template.SequenceEqual(words))
            {
                Debug.Log("Valid sentence");
                return true;
            }
        }
        Debug.Log("Invalid sentence");
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
