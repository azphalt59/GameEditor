using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MainEditor : MonoBehaviour
{
    [SerializeField]
    private Tilemap _TileMap;
    [SerializeField]
    private TilesInfo _TilesInfo;

    public int _MapSize;
    private Vector3Int _VectorMapSize;
    public List<Tile> _Tiles;
    public Tile _RepaintTile;
    [SerializeField]
    private GameObject TileButtonPrefab;
    [Header("ScrollView")]
    [SerializeField]
    private GameObject ScrollviewContent;
    [SerializeField]
    private GameObject Scrollview;
    public int ScrollSpeed = 15;

    public static MainEditor Instance;
    public void Awake()
    {
        if(Instance != null && Instance !=this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraPostion();
        CreateButton();
        InitializeTiles();
    }
    private void Update()
    {
        Scrollview.GetComponent<ScrollRect>().scrollSensitivity = ScrollSpeed;
        
        if(IsHoverUI(Input.mousePosition) == false)
        {
            _TilesInfo.TilePreview();
        }
        if (Input.GetMouseButton(0) && IsHoverUI(Input.mousePosition) == false)
        {
            _TilesInfo.ResetOldTileData();
            _TileMap.SetTile(MousePositionToCellPosition(), _RepaintTile);
        }
    }
    public Vector3Int MousePositionToCellPosition()
    {
        Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int SelectedTile = _TileMap.WorldToCell(MousePosition);
        return SelectedTile;
    }
    bool IsHoverUI(Vector2 position)
    {
        UnityEngine.EventSystems.PointerEventData pointer = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        pointer.position = position;
        List<UnityEngine.EventSystems.RaycastResult> raycastResults = new List<UnityEngine.EventSystems.RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(pointer, raycastResults);

        return raycastResults.Count > 0;
    }
    void CreateButton()
    {
        for(int i=0; i<_Tiles.Count; i++)
        {
            Sprite TileSprite = _Tiles[i].sprite;
            GameObject Button = Instantiate(TileButtonPrefab, ScrollviewContent.transform);
            Button.transform.GetChild(0).GetComponent<Image>().sprite = TileSprite;
            Button.GetComponent<TileButton>()._TileButton = _Tiles[i];
        }
    }
    void InitializeTiles()
    {
        _VectorMapSize = new Vector3Int(_MapSize, _MapSize, 0);
        _TileMap.size = _VectorMapSize;
        Vector3Int[] positions = new Vector3Int[_VectorMapSize.x * _VectorMapSize.y];
        TileBase[] tileArray = new TileBase[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Vector3Int(i % _VectorMapSize.x, i / _VectorMapSize.y, 0);
            //Random
            tileArray[i] = _Tiles[Random.Range(0, _Tiles.Count)];
            //Une tile
            tileArray[i] = _RepaintTile;
        }
        _TileMap.SetTiles(positions, tileArray);
    }
    void CameraPostion()
    {
        Camera.main.transform.position =
            new Vector3(
            Camera.main.orthographicSize * Camera.main.aspect,
            Camera.main.orthographicSize,
            Camera.main.transform.position.z);
    }
    public float GetSizeMap()
    {
        return _MapSize;
    }
    public Tilemap GetTilemap()
    {
        return _TileMap;
    }
}
