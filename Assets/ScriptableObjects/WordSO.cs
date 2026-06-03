using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordSO", menuName = "ScriptableObjects/WordSO", order = 1)]
public class WordSO : ScriptableObject
{
    public List<string> words;
}
