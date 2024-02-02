using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    private GameObject enemy;

    void Update()
    {
        if (enemy == null)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        enemy = Instantiate(enemyPrefab) as GameObject;
        float randomHeight = Random.Range(1.0f, 3.0f);
        enemy.transform.position = new Vector3(0, randomHeight, 0);
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
        SetRandomColor(enemy);
    }

    private void SetRandomColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            renderer.material.color = randomColor;
        }
    }
}
