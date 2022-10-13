using Harsche.Utils;
using UnityEngine;

public class Map : MonoBehaviour{
    [SerializeField] private RectTransform mapSpaceTransform;
    [SerializeField] private BoxCollider gameSpaceBoxCollider;

    private Bounds gameSpaceBounds;
    private Vector2 mapSpaceSize;

    public static Map Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gameSpaceBounds = gameSpaceBoxCollider.bounds;
        mapSpaceSize = mapSpaceTransform.sizeDelta;
    }

    public void UpdateMap(){
        // Calculate Player Position

        Vector3 playerPosition = Player.Instance.transform.position;

        var inMinMaxX = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);
        var inMinMaxY = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);
        var inMinMaxZ = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);

        var outMinMaxX = new Vector2(-mapSpaceSize.x / 2, mapSpaceSize.x / 2);
        var outMinMaxY = new Vector2(-mapSpaceSize.y / 2, mapSpaceSize.y / 2);

        var playerMapPosition = new Vector2(
            UtilityMethods.RemapClamped(inMinMaxX, outMinMaxX, playerPosition.x),
            UtilityMethods.RemapClamped(inMinMaxY, outMinMaxY, playerPosition.y)
        );
    }