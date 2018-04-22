using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour
{
    public static Globals instance;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    public int MyIndex;
}
