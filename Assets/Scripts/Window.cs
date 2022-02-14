using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] protected GameObject panelGameObject;
    public void OnOpenButtonClick() 
    {
        panelGameObject.SetActive(true);
    }
}
