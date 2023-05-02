using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Platform
{
    public Transform platform;
    public Transform endPosition;
}
public class PlatformSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private bool _enable = true;
    [SerializeField] private bool restart = false;
    [SerializeField] private bool loop = false;
    public string prompt => _prompt;
    public GameObject canvas => _canvas;



    public bool enable  => _enable;

    public float speed = 1.0f;
    public Platform[] platforms;
    private int coroutineCount = 0;
    

    public bool Interact(Interactor interactor)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            foreach (Platform item in platforms)
            {
                coroutineCount++;
                StartCoroutine(MovePlatforms(item.platform, item.platform.position, item.endPosition.position));
                item.endPosition.position = item.platform.position;
            }
            
        }
        return true;
    }

    IEnumerator MovePlatforms(Transform platform, Vector3 startPosition, Vector3 endPosition) 
    {
        this._enable = false;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startPosition, endPosition);

        for(; ; )
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionJourney = distCovered / journeyLength;

            if (fractionJourney < 1)
            { 
                platform.position = Vector3.Lerp(startPosition, endPosition, fractionJourney);
            }

            if (fractionJourney >= 1 )
            {
                if (this.loop == false)
                {
                    break;
                }
                else
                {
                    Vector3 tempStartPosition = startPosition;
                    startPosition = endPosition;
                    endPosition = tempStartPosition;

                    startTime = Time.time;
                    journeyLength = Vector3.Distance(startPosition, endPosition);
                }
            }
         

            yield return null;
        }

        if (--coroutineCount == 0 && this.restart == true && this.loop == false) 
        {
            this._enable = true;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Platform item in platforms)
        {
            Gizmos.DrawLine(item.platform.position, item.endPosition.position);
        }
        

    }

}
