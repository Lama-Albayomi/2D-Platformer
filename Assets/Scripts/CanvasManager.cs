using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    public RectTransform[] panels;
    public RectTransform endPanel;
    
    public static CanvasManager instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        selected(0);
    }
    void Update()
    {
        
    }
    public void ResetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void EndScene(){
        endPanel.gameObject.SetActive(true);
    }
    public void selected(int index){
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == index)
            {
                Color color = panels[i].GetComponent<Image>().color;
                // set color alfa to max 
                color.a = 1f;

                panels[i].GetComponent<Image>().color = color;

            }
            else
            {
                Color color = panels[i].GetComponent<Image>().color;
                // set color alfa to max 
                color.a = 0.2f;
                
                panels[i].GetComponent<Image>().color = color;
                // unity change color alpha 



            }
        }

    }
}
