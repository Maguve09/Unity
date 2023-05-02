using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private bool _enable = true;
    public string prompt => _prompt;
    public GameObject canvas => _canvas;

    public bool enable  => _enable;

    public int count = 0;
    void Update()
    {
        
    }

    public bool Interact(Interactor interactor)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            count++;
            Debug.Log("Abriendo " + count);
            this._enable = false;
        }
        return true;
    }

}
