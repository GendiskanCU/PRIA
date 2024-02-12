//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SceneProceduralGeneration : MonoBehaviour
{
    /*[SerializeField] private GameObject brick;
    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject border;*/

    //Límites del borde de la escena
    [SerializeField][Tooltip("Límites x,y mínimos de la escena")] private Vector2 minCoordBorderArea = new Vector2(-20, -1);
    [SerializeField][Tooltip("Límites x,y máximos de la escena")] private Vector2 maxCoodBorderArea = new Vector2(20, 15);

    //Número de bloques de relleno en cada borde de la escena para evitar que
    //la cámara recoja el vacío existente fuera de los límites
    private enum numBorderBlocks  {arriba = 5, abajo = 3, izquierda = 8, derecha = 8};
    
    [SerializeField][Tooltip("Altura máxima del suelo generado proceduralmente")][Range(4, 8)] private int height = 5;
    
    [SerializeField][Tooltip("Diferencia máxima entre la altura de un conjunto de tierra y el inmediatamente posterior")]
    [Range(1, 3)] private int differenceBetweenHeight = 3;

    [SerializeField][Tooltip("Distancia mínima desde ladrillo a tierra")][Range(0, 3)] private int minDistanceBricks;
    [SerializeField][Tooltip("Distancia máxima desde ladrillo a tierra")][Range(0, 3)] private int maxDistanceBricks;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public IEnumerator GeneratesScene()
    {
        //Genera los bordes del recinto de juego
        yield return StartCoroutine(GeneratesBorders());
        //Genera el suelo proceduralmente
        yield return StartCoroutine(GeneratesFloor());
    }


    /// <summary>
    /// Instancia un objecto en la posición especificada
    /// </summary>
    /// <param name="prefabToInstantiate"></param>
    /// <param name="positionToInstantiate"></param>
    private void SpawnObject(string prefabToInstantiate, Vector3 positionToInstantiate)
    {        
        GameObject newObject = PhotonNetwork.Instantiate(prefabToInstantiate, positionToInstantiate, Quaternion.identity);
        newObject.transform.parent = this.transform;
    }


    /// <summary>
    /// Genera los bordes de la escena
    /// </summary>
    private IEnumerator GeneratesBorders()
    {         
        for(int x = (int)minCoordBorderArea.x - (int)numBorderBlocks.izquierda; x <= (int)maxCoodBorderArea.x + (int)numBorderBlocks.derecha; x++)
        {
            //Fila inferior
            for(int y = (int)minCoordBorderArea.y; y >= (int)minCoordBorderArea.y - (int)numBorderBlocks.abajo; y--)
            {
                SpawnObject("Border", new Vector3(x, y, 0));
                yield return new WaitForSeconds(0.001f);                
            }  
                
            //Fila superior
            for(int y = (int)maxCoodBorderArea.y; y <= (int)maxCoodBorderArea.y + (int)numBorderBlocks.arriba; y++)
            {
                SpawnObject("Border", new Vector3(x, y, 0));
                yield return new WaitForSeconds(0.001f);                   
            }            
        }            
        for(int y = (int)minCoordBorderArea.y + 1; y <= (int)maxCoodBorderArea.y - 1; y++)
        {
            //Columna izquierda
            for(int x = (int)minCoordBorderArea.x; x >= minCoordBorderArea.x - (int)numBorderBlocks.izquierda; x--)
            {
                SpawnObject("Border", new Vector3(x, y, 0));
                yield return new WaitForSeconds(0.001f);                   
            }
            
            //Columna derecha
            for(int x = (int)maxCoodBorderArea.x; x <= maxCoodBorderArea.x + (int)numBorderBlocks.derecha; x++)
            {
                SpawnObject("Border", new Vector3(x, y, 0));
                yield return new WaitForSeconds(0.001f);                   
            }
        }
    }


    /// <summary>
    /// Genera proceduralmente el piso de la escena
    /// </summary>
    private IEnumerator GeneratesFloor()
    {
        //Altura máxima para tierra (la última posición se dejará para hierba)
        int maxHeightDirt = height - 1;
        //Para calcular la altura del próximo conjunto
        int heightDirt;
        //Para guardar la altura del último conjunto
        int lastHeight = 0;

        //Para guardar la distancia mínima y máxima del ladrillo a tierra
        int minHeightBrick, maxHeightBrick;
        //Para calcular la altura del ladrillo
        int heightBrick;  

        for(int x = (int) minCoordBorderArea.x + 1; x < (int)maxCoodBorderArea.x; x++)
        {
            //Calcula la altura del próximo conjunto , asegurando que estará entre el mínimo y máximo
            //establecido y que no habrá más de differenceBetweenHeight alturas de diferencia con el anterior conjunto
            heightDirt = Mathf.Clamp(Random.Range((int)minCoordBorderArea.y + 1, maxHeightDirt), 
                    lastHeight - differenceBetweenHeight, lastHeight + differenceBetweenHeight);
            lastHeight = heightDirt;

            //Calcula la altura del ladrillo teniendo en cuenta la distancia a dejar con la tierra
            minHeightBrick = heightDirt - maxDistanceBricks;
            maxHeightBrick = heightDirt - minDistanceBricks;
            heightBrick = Random.Range(minHeightBrick, maxHeightBrick);

            for(int y = (int)minCoordBorderArea.y + 1; y <= heightDirt; y++)
            {
                if(y <= heightBrick) //Si la altura está dentro de la máxima para ladrillo
                {
                    SpawnObject("Brick", new Vector3(x, y, 0));//Instancia los bloques de ladrillo
                }
                else
                {
                    SpawnObject("Dirt", new Vector3(x, y, 0));//Instancia los bloques de tierra
                }
                yield return new WaitForSeconds(0.001f);   
            }
            SpawnObject("Grass", new Vector3(x, heightDirt + 1, 0) );//Instancia un bloque de hierba encima del último de tierra
        }
    }
}
