using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VFXID
{
    Fire
}

public class VFXController : MonoBehaviour
{
    public static VFXController Instance => instance;
    private static VFXController instance;

    [SerializeField] private GameObject[] vfxPrefabs;

    private Dictionary<VFXID, VFX[,]> vfxsesPooler;

    private void Awake()
    {
        if(!ReferenceEquals(VFXController.Instance, null))
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        CreateVFXPooler(Map.Instance.MapData);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void CreateVFXPooler(Tile[,] mapData)
    {
        vfxsesPooler = new Dictionary<VFXID, VFX[,]>();

        for (int currentVFXPrefab = 0; currentVFXPrefab < vfxPrefabs.Length; currentVFXPrefab++)
        {
            VFX currentVFX = vfxPrefabs[currentVFXPrefab].GetComponent<VFX>();

            VFX[,] newVFXPool = new VFX[mapData.GetLength(0), mapData.GetLength(1)];

            for(int x = 0; x < newVFXPool.GetLength(0); x++)
            {
                for(int y = 0; y < newVFXPool.GetLength(1); y++)
                {
                    newVFXPool[x, y] = Instantiate(vfxPrefabs[currentVFXPrefab], GameWorldToMapCastController.Instance.CastMapPosToGameWorld(new MapPosition(x, y)), Quaternion.identity, this.transform).GetComponent<VFX>();
                }
            }

            vfxsesPooler.Add(currentVFX.ID, newVFXPool);
        }
    }

    public void Play(VFXID vfxID, MapPosition startPoint, MapPosition[] area)
    {
        for(int i = 0; i < area.Length; i++)
        {
            if (Arrays2DExtensions.CheckIfPositionIsWithinBoundsOfArray<VFX>(startPoint.X + area[i].X, startPoint.Y + area[i].Y, vfxsesPooler[vfxID]))
            {
                (vfxsesPooler[vfxID])[startPoint.X + area[i].X, startPoint.Y + area[i].Y].PlayEffect();
            }
        }
    }

    public void Play(VFXID vfxID, List<MapPosition> affectedPositions)
    {
        for (int i = 0; i < affectedPositions.Count; i++)
        {
            if (Arrays2DExtensions.CheckIfPositionIsWithinBoundsOfArray<VFX>(affectedPositions[i], vfxsesPooler[vfxID]))
            {
                (vfxsesPooler[vfxID])[affectedPositions[i].X, affectedPositions[i].Y].PlayEffect();
            }
        }
    }
}
