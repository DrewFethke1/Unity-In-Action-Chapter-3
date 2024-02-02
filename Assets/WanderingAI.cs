using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    private bool isAlive;
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }

                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (fireball == null)
                    {
                        fireball = Instantiate(fireballPrefab) as GameObject;
                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }


    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }

    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();

        if (behavior != null)
        {
            behavior.SetAlive(false);
        }

        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        // Add your logic for death animation or other actions
        yield return new WaitForSeconds(3.0f);

        // Optionally destroy the GameObject after a delay
        Destroy(gameObject);
    }

}