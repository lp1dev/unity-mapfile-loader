using System;
using UnityEngine;
using UnityEngine.UI;

public class mapGeneration : MonoBehaviour
{
    public int selectedMap = 0;
    public GameMap gameMap;
    private GameObject player;

    void generateCube(int x, int y, int z, Material material)
    {
        Vector3 spawnLocation = new Vector3(x, z, y);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Renderer>().material = material;
        cube.transform.position = spawnLocation;
    }

    void generateWalls(){
        Material wallsMaterial = Resources.Load("Materials/wallMaterial") as Material;
        foreach (Position position in gameMap.walls)
            for (int i = 0; i < gameMap.wallsHeight; i++)
                generateCube(position.x, position.y, 1 + i, wallsMaterial);
    }

    void generateFloor()
    {
        Material floorMaterial = Resources.Load("Materials/floorMaterial") as Material;
        foreach (Position position in gameMap.floor)
            generateCube(position.x, position.y, 0, floorMaterial);
        generateCube(gameMap.spawn.x, gameMap.spawn.y, 0, floorMaterial);
        generateCube(gameMap.end.x, gameMap.end.y, 0, floorMaterial);
    }

    void generateDoors()
    {
        Material doorMaterial = Resources.Load("Materials/doorMaterial") as Material;
        foreach (Position position in gameMap.doors)
            for (int i = 0; i < gameMap.wallsHeight; i++)
                generateCube(position.x, position.y, 1 + i, doorMaterial);
    }

    void generateMonsters()
    {
        //Material doorMaterial = Resources.Load("Materials/doorMaterial") as Material;
        foreach (Position position in gameMap.monsters)
        {
            GameObject monster = (GameObject)Instantiate(Resources.Load("Prefabs/ghost"));
            monster.transform.position = new Vector3(position.x, 1.5F, position.y);
            generateCube(position.x, position.y, 0, Resources.Load("Materials/floorMaterial") as Material);
        }
    }

    public void generateMap(int mapNumber)
    {
        gameMap = new GameMap(mapNumber);
        player = GameObject.Find("character"); //Generating the character
        player.transform.position = new Vector3(gameMap.spawn.x, 1, gameMap.spawn.y);
        generateWalls();
        generateFloor();
        generateDoors();
        generateMonsters();
        //Generating end of level
        GameObject levelEnd = (GameObject)Instantiate(Resources.Load("Prefabs/levelEnd"));
        levelEnd.transform.position = new Vector3(gameMap.end.x, 1.5F, gameMap.end.y);
        GameObject subtitle = GameObject.Find("subtitle");
        subtitle.GetComponent<Text>().text = gameMap.endPhrase;
    }

    void Start () {
        generateMap(selectedMap);
    }

	void Update () {
	}
}
