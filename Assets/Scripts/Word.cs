using UnityEngine;

public enum WordType {
    Verb,
    Adjective,
    Noun,
    Subjective,
    Conjunction
}

public class Word {

    public string WordText { get; set; }
    public string WordTextPlural { get; set; }

    public WordType wordType { get; set; }

    public Word(string singular, WordType type, string plural = "") {
        WordText = singular;
        if (plural.Length == 0) // in case there is no plural form, default to singular
        {
            WordTextPlural = singular;
        }
        else
        {
            WordTextPlural = plural;
        }
        wordType = type;
    }
}
