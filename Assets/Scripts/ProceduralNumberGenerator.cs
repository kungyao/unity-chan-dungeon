using UnityEngine;
using System.Collections;

public class ProceduralNumberGenerator {
	public static int currentPosition = 0;
	public const string key = "12342341341241234321321421341432";

	public static int GetNextNumber() {
		string currentNum = key.Substring(currentPosition++ % key.Length, 1);
		return int.Parse (currentNum);
	}
}
