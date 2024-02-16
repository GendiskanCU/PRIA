using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    [SerializeField] private int minStoneHeight = 2, maxStoneHeight = 7;
    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject stone;

    // Start is called before the first frame update
    void Start()
    {
        StartProceduralGeneration();
    }

    private void StartProceduralGeneration()
    {
        //Rango de la altura aleatoria
        int minHeight, maxHeight;
        //Rango de distancia de la roca
        int minStoneSpawnDistance, maxStoneSpawnDistance, totalStoneSpawnDistance;

        for(int x = 0; x < width; x++)//Expande en el eje x
        {
            //Mofifica la altura de forma gradual
            minHeight = height - 1;
            maxHeight = height + 1;
            height = Random.Range(minHeight, maxHeight+1);

            minStoneSpawnDistance = height - minStoneHeight;
            maxStoneSpawnDistance = height - maxStoneHeight;
            totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance+1);

            for(int y = 0; y < height; y++)//Expande en el eje y
            {
                if(y < totalStoneSpawnDistance)
                {
                    SpawnObject(stone, x, y);
                }
                else
                {
                    SpawnObject(dirt, x, y);
                }                
            }

            if(totalStoneSpawnDistance == height)
            {
                SpawnObject(stone, x, height);
            }
            else
            {
                SpawnObject(grass, x, height);
            }
            
        }
    }

    /// <summary>
    /// Instancia un objeto en las coordenadas indicadas
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void SpawnObject(GameObject obj, int x, int y)
    {
        GameObject objCreated = Instantiate(obj, new Vector2(x, y), Quaternion.identity);
        objCreated.transform.parent = this.transform;                
    }
}
