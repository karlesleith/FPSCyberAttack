using UnityEngine;
using System.Collections;

public class NumGen {

    //this is a simple class that Proce
	public static int currentPosition = 0;
	public const string k = "123424123342421432233144441212334432121223344";

	public static int GetNextNumber() {
        //Procedual number generator gets a single character between 1 and 4 
		string currentNum = k.Substring(currentPosition++ % k.Length, 1);
        //Return a character between 1 and 4
		return int.Parse (currentNum);
	}
}
