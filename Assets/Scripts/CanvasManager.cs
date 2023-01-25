using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    public static CanvasManager instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        
    }
}
