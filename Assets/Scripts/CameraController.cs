using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 10f;
    [SerializeField] float scrollSpeed = 3f;
    [SerializeField] float minX = -6f;
    [SerializeField] float maxX  = 20f;
    [SerializeField] float minY = 3f;
    [SerializeField] float maxY  = 20f;
    [SerializeField] float minZ = -6f;
    [SerializeField] float maxZ  = 8f;

    // Update is called once per frame
    void Update()
    {
        if(MainManager.gameEnded)
        {
            return ;
        }
        
        if(Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}
