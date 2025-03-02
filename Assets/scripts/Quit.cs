using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void QuitGame(){
    Application.Quit();
    Debug.Log("Quit!");
   }
}
