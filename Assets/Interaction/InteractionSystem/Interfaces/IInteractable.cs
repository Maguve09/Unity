using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public GameObject canvas { get; }
    public string prompt { get; }
    public bool enable  { get;  }


    public bool Interact(Interactor interactor);
}
