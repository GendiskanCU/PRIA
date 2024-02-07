using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject stone;
    [SerializeField] int width; //Anchura
    [SerializeField] private int height;//Altura

    [SerializeField] private int minStoneDistance;
    [SerializeField] private int maxStoneDistance;

    // Start is called before the first frame update
    void Start()
    {
        Generation();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Generation()
    {
        int minHeight;
        int maxHeight;

        int minStoneSpawnDistance;
        int maxStoneSpawnDistance;
        int totalStoneDistance;
       

        for(int x = 0; x < width; x++)
        { //Modifica la altura de manera gradual
            
            minHeight = height - 1;
            maxHeight = height + 2;

            height = Random.Range(minHeight, maxHeight);//Calcula la altura aleatoriamente dentro del rango establecido

            //Distancias de la altura de la piedra
            minStoneSpawnDistance = height - minStoneDistance;
            maxStoneSpawnDistance = height - maxStoneDistance;
            totalStoneDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);

            for(int y = 0; y < height; y++)//Instancia la tierra
            {
                if(y < totalStoneDistance)//Si la altura está dentro de la distancia de una piedra, la debe instanciar
                {
                    SpawnObject(stone, x, y);
                }
                else //Y si no, instancia la tierra
                {
                    SpawnObject(dirt, x, y);
                }

                if(totalStoneDistance == height)
                {
                    SpawnObject(stone, x, height);
                }
                else
                {
                    //Instancia la hierba
                    SpawnObject(grass, x, height - 0.59f);
                }
                
                Debug.Log("Fin del blucle");
            }
            //Instancia la hierba
            //SpawnObject(grass, x, height - 0.59f);                     
        }
    }

    // Spawn object => Expandir
    private void SpawnObject(GameObject obj, float x, float y)
    {
        obj = Instantiate(obj, new Vector2(x, y), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}
