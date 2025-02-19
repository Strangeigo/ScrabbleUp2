using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    private HashSet<string> wordSet = new HashSet<string>();

    void Start()
    {
        LoadDictionary();
        CheckWord("BLASPHEMATEURS");
    }

    void LoadDictionary()
    {
        // Load the text file from Resources
        TextAsset textAsset = Resources.Load<TextAsset>("mots");

        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split('\n');

            foreach (string line in lines)
            {
                string word = line.Trim();
                if (!string.IsNullOrEmpty(word))
                {
                    wordSet.Add(word);
                }
            }
        }
        else
        {
            Debug.LogError("Dictionary file not found in Resources!");
        }
    }

    void CheckWord(string word)
    {
        if (wordSet.Contains(word))
        {
            Debug.Log($"The word '{word}' exists in the dictionary.");
        }
        else
        {
            Debug.Log($"The word '{word}' is not in the dictionary.");
        }
    }
}
