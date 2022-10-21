using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeetTextGenerator{
    [CreateAssetMenu(fileName = "Leet Text Generator", menuName = "Tools/Leet Text Generator", order = 0)]
    public class LeetTextGenerator : ScriptableObject{
        public bool toUpper;
        [TextArea] public string text;
        [TextArea] public string outputText;
        public List<Letter> letters;

        public void GenerateEncryptedText(){
            outputText = text;
            foreach (Letter letter in letters){
                outputText = outputText.Replace(letter.character, letter.number);
                outputText = outputText.Replace(letter.character.ToUpper(), letter.number);
            }

            if (toUpper) outputText = outputText.ToUpper();
        }

        [Serializable]
        public class Letter{
            public string character;
            public string number;
        }
    }
}