using UnityEngine;

public enum wordType {
    Verbs,
    Adjectives,
    Nouns
}

public class Word {
    string word;
    wordType type;

    public Word(string word, wordType type) {
        this.word = word;
        this.type = type;
    }


    public void SetWord(string word) {
        this.word = word;
    }

    public string GetWord() {
        return word;
    }

    public void SetWord(wordType type) {
        this.type = type;
    }
}
