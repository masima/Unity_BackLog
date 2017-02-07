using UnityEngine;
using UnityEngine.UI;

public class WriteBackLog : MonoBehaviour
{

    Text text;
    public BackLog backLog;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    public void SendToLog()
    {
        backLog.Log(text.text);
    }
}
