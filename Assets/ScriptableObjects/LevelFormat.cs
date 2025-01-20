using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelList", menuName ="Create Level List")]
public class LevelFormat : ScriptableObject
{
    public Level[] level;
}
[System.Serializable]
public class Level
{
    public Sprite sprite;
    public Color color;
}