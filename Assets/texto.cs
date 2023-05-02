using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class texto : MonoBehaviour
{


    [SerializeField]
    private GameObject uiObject;


    [SerializeField]
    private GameObject CamaraNueva;

    private GameObject MainCamera;

    [SerializeField]
    private float tiempoDeEspera;


    

    Animator animChild;
    // Animator animPlatform;
    bool isPlayerInTrigger;
    bool hasInteracted;



    void Start()
    {
        uiObject.SetActive(false);

        foreach (Transform child in transform)
        {
            if (child.name == "Animable") {
                animChild = child.GetComponent<Animator>();
            }
        }

        //try to asign camera


        MainCamera = GameObject.FindWithTag("MainCamera");
        CamaraNueva.SetActive(false);

    }
    
    public void Update() 
    {
        if (Input.GetKeyDown("e") && isPlayerInTrigger == true && hasInteracted == false)
        {
            uiObject.SetActive(false);
            hasInteracted = true;
            // animChild.enabled = true;

    

            StartCoroutine(Cinema(tiempoDeEspera));
        }



        if (Input.GetKeyDown("e") && isPlayerInTrigger == true && hasInteracted == true)
        { 
          
            hasInteracted = false;
            uiObject.SetActive(true);
          
        }
    }

    void OnTriggerStay(Collider player)
    {
        if (hasInteracted == false)
        {
            uiObject.SetActive(true);
            isPlayerInTrigger = true;
        }


    }
    


    void OnTriggerExit(Collider player) 
    {
       if (hasInteracted == false)
        {
            uiObject.SetActive(false);
            isPlayerInTrigger = false;
        }
    }

    //coroutine to wait for animation to finish
    IEnumerator Cinema(float waitTime)
    {
        MainCamera.SetActive(false);
        CamaraNueva.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        MainCamera.SetActive(true);
        CamaraNueva.SetActive(false);
    }


}

