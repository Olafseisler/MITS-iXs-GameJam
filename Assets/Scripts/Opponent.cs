using System.Linq;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public Word[] triggers { get; set; }
    public Opponent(Word[] words)
    {
        triggers = words;
    }

    // check if sentence contains a trigger
    public bool HasTrigger(Word[] sentence)
    {
        foreach (Word word in triggers)
        {
            if (sentence.Contains(word))
            {
                return true;
            }
        }
        return false;
    }
}
