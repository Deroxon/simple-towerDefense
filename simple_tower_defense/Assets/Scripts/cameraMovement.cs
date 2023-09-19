using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 0;
    private float xMax;
    private float yMin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        getInput();
    }

    private void getInput()
    {
        // instructions of movement of camera
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin,0), -10);


        // example of simply one click for right
        /*
         if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
            }
         */

    }

    public void setLimits(Vector3 maxTile)
    {
        // left down point of camera, by function ViewportToWorldPoint, if we wanna left bottom its 0,0 if right top its 1,1
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;

    }

}
