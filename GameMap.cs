using System.Collections;
using UnityEngine;
using System.Text;
using System.IO;

public class Position
{
    public int x, y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class GameMap{
    public int wallsHeight;
    public Hashtable mapList = new Hashtable();
    public Position spawn, size, end;
    public ArrayList walls;
    public ArrayList floor;
    public ArrayList doors;
    public ArrayList monsters;
    public string endPhrase;

    public void processLine(int index, string buffer) {
        for (int i = 0; i < buffer.Length; i++)
            switch (buffer[i]){
                case '0':
                    walls.Add(new Position(index, i));
                    break;
                case 'X':
                    this.spawn = new Position(index, i);
                    break;
                case ' ':
                    floor.Add(new Position(index, i));
                    break;
                case 'D':
                    doors.Add(new Position(index, i));
                    break;
                case 'M':
                    monsters.Add(new Position(index, i));
                    break;
                case 'E':
                    this.end = new Position(index, i);
                    break;
            }
    }

    public bool load(string filename)
    {
        try
        {
            int i = 0;
            string buffer;
            string[] mapSize;

            walls = new ArrayList();
            floor = new ArrayList();
            doors = new ArrayList();
            monsters = new ArrayList();
            StreamReader streamReader = new StreamReader(filename, Encoding.Default);
            buffer = streamReader.ReadLine();
            mapSize = buffer.Split(';');
            this.size = new Position(int.Parse(mapSize[0]), int.Parse(mapSize[1]));
            this.wallsHeight = int.Parse(mapSize[2]);
            while (((buffer = streamReader.ReadLine()) != null) && i < this.size.y)
                processLine(i++, buffer);
            this.endPhrase = buffer;
            streamReader.Close();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public GameMap(int map_number)
    {
        mapList[0] = "Assets\\Maps\\map1.txt";
        mapList[1] = "Assets\\Maps\\map2.txt";
        this.load(mapList[map_number].ToString());
    }
}
