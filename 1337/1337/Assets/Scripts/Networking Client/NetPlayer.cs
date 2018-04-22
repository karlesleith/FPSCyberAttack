using UnityEngine;


class NetPlayer : MonoBehaviour
{
    public static NetPlayer instance;

    public int Index;

    private void Awake()
    {
        instance = this;
    }
}

