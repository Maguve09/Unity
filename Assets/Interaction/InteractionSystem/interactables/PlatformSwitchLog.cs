using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformSwitchLog : MonoBehaviour, IInteractable
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
            StartCoroutine(MovePlatforms() );            
        }
        return true;
    }

    IEnumerator MovePlatforms() 
    {
        this._enable = false;
        float startTime = Time.time;
        
        Debug.Log("Moviendo Plataformas....");
        for(;;) 
        {
            if (Time.time - startTime > 5){
                break;
            }
            yield return null;
           
        }
        Debug.Log("Moviendo Plataformas....[Completado]");

        this._enable = true;
    }

}
