using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageFrame : MonoBehaviour
{
    [SerializeField] private Image frame;
    [SerializeField] private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMessage(string message)
    {
        frame.enabled = true;
        text.text = message;
        text.enabled = true;
    }

    public void HideMessage()
    {
        text.enabled = false;
        frame.enabled = false;
    }
}
