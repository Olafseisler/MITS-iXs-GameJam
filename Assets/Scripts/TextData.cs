using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class TextData : ScriptableObject
{
    [TextArea] public string verbs;
    [TextArea] public string nouns;
    [TextArea] public string adjectives;
    [TextArea] public string subjectives;
    [TextArea] public string conjunctions;
    [TextArea] public string templates;
    [TextArea] public string opponentPig;
    [TextArea] public string opponentHorse;
    [TextArea] public string responses;
}
