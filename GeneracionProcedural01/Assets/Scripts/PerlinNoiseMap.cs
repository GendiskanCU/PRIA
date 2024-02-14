using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    //Propiedades
    Dictionary<int, GameObject> tileset; //Diccionario de las losetas
    Dictionary<int, GameObject> tile_groups;//Diccionario para la organización de un conjunto de losetas

    //Prefabs de las losetas del terreno
    [SerializeField] GameObject prefab_plain;
    [SerializeField] GameObject prefab_forest;
    [SerializeField] GameObject prefab_hills;
    [SerializeField] GameObject prefab_mountains;

    int x_offset = 5;//Valor para ir retocando y probar
    int y_offset = 10;//Valor para ir retocando y probar
    float magnification = 100f;//Para hacer más grande/pequeño el mapa

    //Tamaño del mapa
    int map_width = 160, map_height= 90;

    //Lista de números enteros que crearemos con la función pseudo-aleatoria Perlin
    List<List<int>> noise_grid = new List<List<int>>();

    //Lista con todos los objetos
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();



    // Start is called before the first frame update
    void Start()
    {
        CreateTileSet();
        CreateTileGroup();
        GenerateMap();
    }

    private void CreateTileSet()
    {
        //Añadimos al diccionario cada una de las losetas, asignándole un valor
        //Nota: cuanto mayor es el valor, va referido a una "altura" mayor en el mapa
        tileset = new Dictionary<int, GameObject>();

        tileset.Add(0, prefab_plain);
        tileset.Add(1, prefab_forest);
        tileset.Add(2, prefab_hills);
        tileset.Add(3, prefab_mountains);

    }


    private void CreateTileGroup()
    {
        //Creamos unos empty GameObjects para agrupar las losetas del mismo tipo
        tile_groups = new Dictionary<int, GameObject>();

        foreach(KeyValuePair<int, GameObject> prefab_pair in tileset)
        {
            GameObject tilegroup = new GameObject();
            tilegroup.transform.parent = gameObject.transform;
            tilegroup.transform.localPosition = new Vector3(0,0,0);
            tile_groups.Add(prefab_pair.Key, tilegroup);//Añade el código con el grupo de la loseta
        }
    }


    private void GenerateMap()
    {
        //Generación de un grid Perling Noise
        for(int x = 0; x < map_width; x++)
        {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());

            for(int y = 0; y < map_height; y++)
            {
                int tile_id = GetIdUsingPerlin(x, y);

                noise_grid[x].Add(tile_id);

                CreateTile(tile_id, x, y);
            }
        }
    }


    private int GetIdUsingPerlin(int x, int y)
    {
        //Tenemos dos coordenadas. Generamos un valor para ser convertido en el código ID para saber la loseta

        float raw_perlin = Mathf.PerlinNoise( (x - x_offset) / magnification, (y - y_offset) / magnification);

        float limit_perlin = Mathf.Clamp01(raw_perlin);//Limitamos el valor a 0 - 1 para que el mapa no se "desmadre"

        float scaled_perlin = limit_perlin * tileset.Count;//Creamos la escala para cada uno de los prefabs

        if(scaled_perlin == tileset.Count)//Si se ha pasado por arriba de la escala
        {
            scaled_perlin = tileset.Count - 1;//Reajustamos el valor
        }

        return Mathf.FloorToInt(scaled_perlin);//Devuelve 0, 1, 2, 3
    }

    private void CreateTile(int tile_id, int x, int y)
    {
        //Creamos una baldosa una vez que tengo el identificador, agrupándolo y poniendo su posición
        GameObject tile_prefab = tileset[tile_id];//Cogemos el prefab

        GameObject tile_group = tile_groups[tile_id];

        GameObject tile = Instantiate(tile_prefab, tile_group.transform);//Instancia la baldosa

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);
        
        tile_grid[x].Add(tile);//Añado la loseta al grid ue representa el mapa
    }

}
