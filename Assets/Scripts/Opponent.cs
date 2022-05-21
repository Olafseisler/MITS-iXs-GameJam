using System.Linq;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public Word[] triggers { get; }
    public Opponent(Word[] words)
    {
        triggers = words;
    }

}
