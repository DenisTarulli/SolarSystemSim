using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCelestial : MonoBehaviour
{
    [SerializeField] public CelestialStats Stats;
    private DatabaseManager _databaseManager;

    private void Start()
    {
        _databaseManager = FindObjectOfType<DatabaseManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _databaseManager.SaveData(Stats);
    }
}
