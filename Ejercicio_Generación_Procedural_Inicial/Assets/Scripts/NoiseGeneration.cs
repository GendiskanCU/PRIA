using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGeneration : MonoBehaviour
{
    [SerializeField] private int width;
    private int height;

    [SerializeField] private int repeatNum = 5;

    [SerializeField] private int minHeight, maxHeight;

    [SerializeField] private int spikeSpawnHeight = 10;//Límite Altura de los obstáculos

    [SerializeField] private GameObject dirt, grass, spike;

    // Start is called before the first frame update
    void Start()
    {
        Generation();
    }

    private void Generation()
    {
        int repeatValue = 0;

        for(int x = 0; x < width; x++)
        {
            if(repeatValue == 0)//Cuando ya no haya que repetir la altura
            {                
                height = Random.Range(minHeight, maxHeight);//Recalcula la altura dentro de límites
                GenerateFlatPlatform(x);
                repeatValue = repeatNum;//Restaura las repeticiones
            }
            else//Cuando haya que repetir la altura
            {
                GenerateFlatPlatform(x);
                repeatValue--;//Resta número de repeticiones
            }
            

            for(int y = 0; y < height; y++)
            {
                SpawnObject(dirt, x, y);
            }

            SpawnObject(grass, x, height);
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

    
    /// <summary>
    /// Genera una plataforma en coordenada x hasta la altura límite
    /// </summary>
    /// <param name="x"></param>
    private void GenerateFlatPlatform(int x)
    {
        for(int y = 0; y < height; y++)
        {
            SpawnObject(dirt, x, y);
        }

        //Instancia arriba la hierba
        SpawnObject(grass, x, height);
        //Si la altura está dentro de los límites de los pinchos también los instancia
        if(height < spikeSpawnHeight)
        {            
            SpawnObject(spike, x, height + 1);
        }
        
        
    }
}
