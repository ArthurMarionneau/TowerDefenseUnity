using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorder = 25f;
    public float scrollSpeed = 5f;

    public float minY = 10f;
    public float maxY = 80f;
    
    private bool canMove = true;
    private void Update()
    {
        if (gameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            canMove = !canMove;
        }
        if (!canMove)
        {
            return;
        }
        
        //GAUCHE
        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= panBorder)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
        }
        
        //DROITE
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorder)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }
        
        //BAS
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorder)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime);
        }
        
        //HAUT
        if (Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= Screen.height - panBorder)
        {
            //transform.position = Vector3.forward * panSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        
        transform.position = pos;
    }
}
