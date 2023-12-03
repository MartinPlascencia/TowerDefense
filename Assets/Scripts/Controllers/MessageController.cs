using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;
    private Queue<string> _messageQueue = new Queue<string>();

    public void EnqueueMessage(string message, float messageTime)
    {
        _messageQueue.Enqueue(message);
        UpdateMessagesUI();
        Invoke("ClearOldestMessage",messageTime);
    }

    void UpdateMessagesUI()
    {
        _messageText.text = "";
        foreach(string message in _messageQueue)
        {
            _messageText.text += message + "\n";
        }
    }

    void ClearOldestMessage()
    {
        _messageQueue.Dequeue();
        UpdateMessagesUI();
    }
}
