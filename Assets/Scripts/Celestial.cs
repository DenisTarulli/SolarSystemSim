using UnityEngine;

public class Celestial : MonoBehaviour
{
    [SerializeField] public CelestialStats stats;
    private DatabaseManager databaseManager;
    private Rigidbody rb;

    private void Awake()
    {
        stats = new CelestialStats();

        rb = GetComponent<Rigidbody>();

        databaseManager = FindObjectOfType<DatabaseManager>();

        SetStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            databaseManager.SaveData(stats);
    }

    private void SetStats()
    {
        stats.Name = gameObject.name;

        if (gameObject.name == "Earth")
            stats.Mass = 1f;       
        else
            stats.Mass = rb.mass;

        stats.Radius = transform.localScale.x;
        stats.DistanceToSun = transform.position.x;
    }
}
