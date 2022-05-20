using UnityEngine;

public enum WordType {
    Verbs,
    Adjectives,
    Nouns
}

public class Word {
    string WordText { get; set; }
    WordType WordType { get; set; }

    public Word(string text, WordType type) {
        WordText = text;
        WordType = type;
    }
}
