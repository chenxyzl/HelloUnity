using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance { get; private set; }
    public Tilemap interactableMap;
    public Tile interactableTile; 
    public Tile groundHoed;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        InitInteractableMap();
    }

    void InitInteractableMap()
    {
        
        foreach(Vector3Int postion in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(postion);
            if(tile != null)
            {
                interactableMap.SetTile(postion, interactableTile);
            }
        }
    }


    public void HoeGround(Vector3 position)
    {
        var tilePostion = interactableMap.WorldToCell(position);
        var tile = interactableMap.GetTile(tilePostion);
        if(tile == null) {
            Debug.Log("���ﲻ����ֲ���ط�������:"+ tilePostion);
            return;
        }

        if(tile.name != interactableTile.name)
        {
            Debug.Log("���ﲻ����ֲ�����Ͳ�ƥ��:" + tilePostion);
            return;
        }
        interactableMap.SetTile(tilePostion, groundHoed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
