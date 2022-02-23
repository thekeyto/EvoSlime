using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle : MonoBehaviour
{
    public Transform puzzles;
    public Transform playerTransform;
    Vector3 originPosition;
    bool ifPull = false;
    private void OnTriggerStay2D(Collider2D collision){
        if (collision.tag == "player"){
            if (Input.GetKeyDown(KeyCode.E)){
                if (ifPull == false){
                    Debug.Log(collision.tag);
                    ifPull = true;
                    originPosition = player.instance.transform.position - transform.position;
                }
                else{
                    this.transform.SetParent(puzzles);
                    ifPull = false;
                }
            }
        }
    }
    private void Update()
    {
        if (ifPull == true)
            this.transform.position = player.instance.transform.position-originPosition;
    }
}
