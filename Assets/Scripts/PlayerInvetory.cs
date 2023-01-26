using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvetory : MonoBehaviour
{
    public Bullet[] bullets;
    public static PlayerInvetory instance;
   
    void Awake()

    {
    
        instance = this; 
        for (int i = 1; i < bullets.Length; i++)
        {
            bullets[i].amount=0;
        }
    }
    // update canves manager to bulet amount
    public void UpdateCanvas()
    {
        for (int i = 1; i < bullets.Length; i++)
        {
            CanvasManager.instance.texts[i].text = "X " + bullets[i].amount.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCanvas();
        
    }
}
