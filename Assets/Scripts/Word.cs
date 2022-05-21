using UnityEngine;

public enum WordType {
    Verb,
    Adjective,
    Noun,
    Subjective,
    Conjunction
}

public class Word {
    [SerializeField] public string WordText { get; set; }
    public WordType WordType { get; set; }

    public Word(string text, WordType type) {
        WordText = text;
        WordType = type;
    }
}
