using UnityEngine;

public enum WordType {
    Verbs,
    Adjectives,
    Nouns
}

public class Word {
    public string WordText { get; set; }
    public WordType WordType { get; set; }

    public Word(string text, WordType type) {
        WordText = text;
        WordType = type;
    }
}
