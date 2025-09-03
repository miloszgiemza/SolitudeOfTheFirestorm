using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldToMapCastController : MonoBehaviour
{
    public static GameWorldToMapCastController Instance => instance;
    private static GameWorldToMapCastController instance;

    public BoundsXY MapGameWorldBounds => mapGameWorldBounds;

    public bool Initialised => initialised;

    private float tileSizeGameworld = 2f;
    private Vector2 posX0Y0InGameWorld = new Vector2(0, 0);

    private BoundsXY mapGameWorldBounds = new BoundsXY(new Vector2(-2.334f, -3.817f), new Vector2(2.331f, 4.933f));

    CastMapAndGameWorld cast = new CastMapAndGameWorld();

    private bool initialised = false;

    private void Awake()
    {
        if(!ReferenceEquals(GameWorldToMapCastController.Instance, null))
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
        Initialize(tileSizeGameworld, posX0Y0InGameWorld);
    }

    private void Update()
    {
        if(InputController.Instance.MainInputAssetsWrapper.MobileDevicesMap.MainAction.WasReleasedThisFrame())
        {
            Debug.Log("Cursor pos: " + Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z))));
            Debug.Log("Tile: " + cast.CastGameWorldPositionToPosOnMap(Input.mousePosition, Mathf.Abs(Camera.main.transform.position.z), tileSizeGameworld, posX0Y0InGameWorld, mapGameWorldBounds).X + " / " + cast.CastGameWorldPositionToPosOnMap(Input.mousePosition, Mathf.Abs(Camera.main.transform.position.z), tileSizeGameworld, posX0Y0InGameWorld, mapGameWorldBounds).Y);
        }
    }

    private BoundsXY CalculateGameWorldBounds(float tileSizeGameworld, Vector2 posX0Y0GameWorld, Tile[,] mapData)
    {
        Vector2 minBouds = new Vector2(posX0Y0GameWorld.x - tileSizeGameworld / 2, posX0Y0GameWorld.y - tileSizeGameworld / 2);
        Vector2 maxBounds = new Vector2(minBouds.x + mapData.GetLength(0) * tileSizeGameworld, minBouds.y + mapData.GetLength(1) * tileSizeGameworld);

        return new BoundsXY(minBouds, maxBounds);
    }

    private void Initialize(float tileSizeGameworld, Vector2 posX0Y0InGameWorld)
    {
        this.tileSizeGameworld = tileSizeGameworld;
        this.posX0Y0InGameWorld = posX0Y0InGameWorld;

        mapGameWorldBounds = CalculateGameWorldBounds(tileSizeGameworld, posX0Y0InGameWorld, Map.Instance.MapData);

        initialised = true;
    }

    public MapPosition CastGameWorldPosToMap(Vector2 posGameWorld)
    {
        return cast.CastGameWorldPositionToPosOnMap(posGameWorld, Mathf.Abs(Camera.main.transform.position.z), tileSizeGameworld, posX0Y0InGameWorld, mapGameWorldBounds);
    }

    public Vector2 CastMapPosToGameWorld(MapPosition positionMap)
    {
        return cast.CastMapPositionToGameWorldPosition(positionMap, tileSizeGameworld, posX0Y0InGameWorld);
    }

    public Vector2 CastMapPosToGameWorld(int x, int y)
    {
        return cast.CastMapPositionToGameWorldPosition(x, y, tileSizeGameworld, posX0Y0InGameWorld);
    }
}
