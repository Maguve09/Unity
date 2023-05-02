using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool a;
    public GameObject[] respawnPoints;
    private Vector3 respawnPoint;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = player.transform.position;
        a = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < respawnPoints.Length; i++) {
            //if player collider is in the respawn point collider
            if (player.GetComponent<Collider>().bounds.Intersects(respawnPoints[i].GetComponent<Collider>().bounds)) {
                respawnPoint = respawnPoints[i].transform.position;
                respawnPoints[i].SetActive(false);
            }
        }

        if (a == true && player.transform.position.y < -10){

            a = false;

            StartCoroutine(vidabajar());
        }
     
}
    IEnumerator vidabajar(){

           
            player.transform.position = respawnPoint;
            yield return new WaitForSeconds(1f);
            a = true;

    }

}