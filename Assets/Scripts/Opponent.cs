using System.Linq;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    readonly Word[] triggers;
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
