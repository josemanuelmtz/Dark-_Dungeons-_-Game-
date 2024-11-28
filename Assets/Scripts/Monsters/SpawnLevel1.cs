using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnLevel1 : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array de prefabs de enemigos (deben ser al menos dos)
    public int numberOfEnemies = 10; // Número total de enemigos a generar (debe ser par)
    public Tilemap floorTilemap; // El Tilemap del piso
    public int margin = 1; // Margen para evitar los bordes del Tilemap

    private void Start()
    {
        // Asegúrate de que el número de enemigos sea par
        if (numberOfEnemies % 2 != 0)
        {
            Debug.LogWarning("El número de enemigos debe ser par. Se ajustará a " + (numberOfEnemies + 1));
            numberOfEnemies++; // Aumenta en 1 si es impar
        }

        SpawnEnemies(); // Generar enemigos al iniciar
    }

    private void SpawnEnemies()
    {
        int enemiesPerType = numberOfEnemies / 2; // Dividir el número total de enemigos en dos tipos
        int spawnedZombies = 0; // Contador de zombies generados
        int spawnedSkeletons = 0; // Contador de esqueletos generados

        // Obtener los límites del Tilemap
        Vector3Int minBounds = floorTilemap.cellBounds.min + new Vector3Int(margin, margin, 0); // Ajustar los límites inferior
        Vector3Int maxBounds = floorTilemap.cellBounds.max - new Vector3Int(margin, margin, 0); // Ajustar los límites superior

        // Generar zombies
        while (spawnedZombies < enemiesPerType)
        {
            Vector3Int randomPosition = GenerateRandomPosition(minBounds, maxBounds);
            if (floorTilemap.HasTile(randomPosition))
            {
                GameObject selectedEnemyPrefab = enemyPrefabs[0];
                Vector3 spawnPosition = floorTilemap.GetCellCenterWorld(randomPosition);
                Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
                spawnedZombies++;
            }
        }

        // Generar esqueletos
        while (spawnedSkeletons < enemiesPerType)
        {
            Vector3Int randomPosition = GenerateRandomPosition(minBounds, maxBounds);
            if (floorTilemap.HasTile(randomPosition))
            {
                GameObject selectedEnemyPrefab = enemyPrefabs[1];
                Vector3 spawnPosition = floorTilemap.GetCellCenterWorld(randomPosition);
                Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
                spawnedSkeletons++;
            }
        }
    }

    private Vector3Int GenerateRandomPosition(Vector3Int minBounds, Vector3Int maxBounds)
    {
        return new Vector3Int(
            Random.Range(minBounds.x, maxBounds.x),
            Random.Range(minBounds.y, maxBounds.y),
            0
        );
    }
}
