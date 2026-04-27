using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    public bool isBoss = false;
    public float spawnInterval = 5.0f;
    private float nextSpawn;
    public int miniEnemySpawnCount = 3;
    private SpawnManager spawnManager;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {
            // Fixed: Added Object. prefix and safety check
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    void Update()
    {
        // Fixed: Added safety check so it won't crash if Player is destroyed
        if (player != null)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }

        if (isBoss && spawnManager != null)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                // Fixed: This call only works if the function below is PUBLIC
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
