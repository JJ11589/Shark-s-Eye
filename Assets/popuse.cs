using System.Collections;
using System.Collections.Generic; // Required for IEnumerator
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Popuse : MonoBehaviour
{
    public GameObject fruitPrefab; // Prefab to spawn
    [SerializeField] public Transform[] spawnfruit; // Array of spawn points
    [SerializeField] public List<Sprite> sprites;
    [SerializeField] public Button tryAgainText;
    public float minDelay = 0.1f;  // Minimum spawn delay
    public float maxDelay = 1f;    // Maximum spawn delay
    private float waitTime;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        // Ensure coroutine starts with correctly spelled method name
        if (spawnfruit == null || spawnfruit.Length == 0)
        {
            Debug.LogWarning("SpawnFruit array is empty. Please assign spawn points in the Inspector.");
            return;
        }

        InvokeRepeating("SpawnFruitRoutine", 1f, 1f);
    }

    public void SpawnFruitRoutine()
    {
        count++;
        if(count < 20)
        {
            Debug.Log("SpawnFruitRoutine");
            float delay = Random.Range(minDelay, maxDelay);
            //yield return new WaitForSeconds(delay);

            int spawnIndex = Random.Range(0, spawnfruit.Length);
            Transform spawnpoint = spawnfruit[spawnIndex];
            float xpos = Random.Range(0, 8);
            spawnpoint.position = new Vector3(xpos, spawnpoint.position.y, 0);
            // Spawn fruitPrefab at the spawn point
            SpriteRenderer spriteRenderer = Instantiate(fruitPrefab, spawnpoint.position, spawnpoint.rotation).GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = sprites[0];
        }
        else
        {
            CancelInvoke();
            Time.timeScale = 0;
            tryAgainText.gameObject.SetActive(true);
        }

    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
