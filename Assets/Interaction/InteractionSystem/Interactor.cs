using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private float radius = 2;
    [SerializeField] private LayerMask layerMask;
    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int founds;
    [SerializeField] private TMP_Text text;
    IInteractable interactable;

    void Update()
    {
        founds = Physics.OverlapSphereNonAlloc(point.position, radius, colliders, layerMask);
        if (founds == 0  ){
            text.text = "";
            return;
        }


        if (founds > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();
            if (interactable != null && interactable.enable == true)
            {
                text.text = interactable.prompt;
                interactable.Interact(this);
            }
            else{
                text.text = "";    
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point.position, radius);

    }
}