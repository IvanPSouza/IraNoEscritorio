using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    [SerializeField] public GameObject Menu1;
    [SerializeField] public GameObject Menu2;
    [SerializeField] public GameObject Menu3;
    private int currentMenu = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentMenu = 1;
        Menu1.SetActive(true);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
    }
    public void menuOne()
    {
        currentMenu = 1;
        Menu1.SetActive(true);
        Menu2.SetActive(false);
        Menu3.SetActive(false);
    }
    public void menuTwo()
    {
        currentMenu = 2;
        Menu1.SetActive(false);
        Menu2.SetActive(true);
        Menu3.SetActive(false);
    }
    public void menuThree()
    {
        currentMenu = 3;
        Menu1.SetActive(false);
        Menu2.SetActive(false);
        Menu3.SetActive(true);
    }
}
