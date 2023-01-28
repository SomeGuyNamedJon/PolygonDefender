using UnityEngine;
using System.Collections;

//Aquired from Asset, slightly modified

[System.Serializable]
public class vCell  {

	public static int size = 3;

	[System.Serializable]
	public struct camStack{
		public GameObject[] cam;
	}

	public camStack[] stack = new camStack[size];
}
