using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OpponentBase : ScriptableObject
{
	[SerializeField] public string characterName;
	[SerializeField] public int health = 3;
	[SerializeField] public Sprite happySprite;
	[SerializeField] public Sprite sadSprite;
}

