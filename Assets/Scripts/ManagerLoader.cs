using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLoader : MonoBehaviour
{
    public AppManager theAm;

    void Awake()
    {
        if(FindObjectOfType<AppManager>() == null)
        {
            AppManager.instance = Instantiate(theAm);
            DontDestroyOnLoad(AppManager.instance.gameObject);
        }
    }
}
