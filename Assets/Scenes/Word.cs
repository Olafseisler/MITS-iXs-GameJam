using UnityEngine;

public enum wordType {
    Verbs,
    Adjectives,
    Nouns;
}

public class Word {
    String word;
    wordType type;

    public Word(string word, wordType type) {
        word = word;
        type = type;
    }


    public void SetWord(String word) {
        word = word;
    }

    public GetWord() {
        return word;
    }

    public void SetWord(wordType type) {
        type = type;
    }
}
