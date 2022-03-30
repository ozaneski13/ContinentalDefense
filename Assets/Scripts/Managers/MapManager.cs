using UnityEngine;
using System;
using UnityEditor.AI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject _nodePrefab = null;
    [SerializeField] private Transform _nodesParent = null;
    [SerializeField] private int _mapSize = 8;

    private GameObject[,] _nodes = null;

    public Action<GameObject, GameObject> MapInitialized;
    private void Awake()
    {
        CreateMap();
    }
    private void CreateMap()
    {
        GameObject tempNode;
        int x = 0;
        int z = 0;
        _nodes = new GameObject[_mapSize, _mapSize];

        for (int i = 0; i < _mapSize; i++)
        {
            for (int j = 0; j < _mapSize; j++)
            {
                tempNode = Instantiate(_nodePrefab, _nodesParent);

                tempNode.transform.position = new Vector3(x + j, 0, z + i);

                x += (int)_nodePrefab.transform.localScale.x;

                _nodes[i, j] = tempNode;
            }

            x = 0;
            z += (int)_nodePrefab.transform.localScale.z;
        }

        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();

        InitStartStop();
    }

    private void InitStartStop()
    {
        int startingPointX = UnityEngine.Random.Range(0, 2);
        int startingPointY;

        if (startingPointX == 0)
            startingPointY = UnityEngine.Random.Range(0, _mapSize);
        else
            startingPointY = 0;

        int endingPointX = UnityEngine.Random.Range(0, _mapSize);
        int endingPointY;

        if (endingPointX == _mapSize - 1)
            endingPointY = UnityEngine.Random.Range(0, _mapSize);
        else
            endingPointY = _mapSize - 1;

        _nodes[startingPointX, startingPointY].GetComponent<MeshRenderer>().material.color = Color.green;
        _nodes[endingPointX, endingPointY].GetComponent<MeshRenderer>().material.color = Color.red;

        MapInitialized?.Invoke(_nodes[startingPointX, startingPointY], _nodes[endingPointX, endingPointY]);
        //ADD START TO startingPointX, startingPointY
        //ADD END TO endingPointX, endingPointY
    }
}