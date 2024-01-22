using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 36;
        float posX = cam.pixelWidth / 2 - size / 2;
        float posY = cam.pixelHeight / 2 - size / 2;

        // Draw black circle
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(posX - 1, posY - 1, size, size), "O");
        GUI.Label(new Rect(posX + 1, posY - 1, size, size), "O");
        GUI.Label(new Rect(posX - 1, posY + 1, size, size), "O");
        GUI.Label(new Rect(posX + 1, posY + 1, size, size), "O");

        // Draw white circle
        GUI.contentColor = Color.white;
        GUI.Label(new Rect(posX, posY, size, size), "O");
    }




    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    Debug.Log("Target hit");
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(CreateBulletHole(hit.point, hit.normal));
                }
            }
        }
    }

    private IEnumerator CreateBulletHole(Vector3 pos, Vector3 normal)
    {
        GameObject hole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        hole.transform.position = pos + normal * 0.01f; // Offset the position along the normal to prevent z-fighting
        hole.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
        hole.transform.localScale = new Vector3(0.05f, 0.00001f, 0.05f);
        hole.GetComponent<Renderer>().material.color = Color.black;
        yield return null;
    }
}
