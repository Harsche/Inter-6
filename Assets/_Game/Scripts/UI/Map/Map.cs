using Harsche.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour{
    [SerializeField] private RectTransform mapSpaceTransform;
    [SerializeField] private BoxCollider gameSpaceBoxCollider;
    [SerializeField] private Transform playerIcon;
    [SerializeField] private Image mapImage;
    [SerializeField] private Sprite floor1;
    [SerializeField] private Sprite floor2;
    [SerializeField] private Toggle toggleFloor1;
    [SerializeField] private Toggle toggleFloor2;
    [SerializeField] private GameObject legenda1;
    [SerializeField] private GameObject legenda2;
    [SerializeField] private float changeFloorHeight;

    private Bounds gameSpaceBounds;
    private Vector2 mapSpaceSize;
    private Vector2 halfMapSpaceSize;
    private int playerCurrentFloor;

    public static Map Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (!gameSpaceBoxCollider) return;
        gameSpaceBounds = gameSpaceBoxCollider.bounds;
        mapSpaceSize = mapSpaceTransform.sizeDelta;
        halfMapSpaceSize = mapSpaceSize / 2;
    }
    
    private void ChangeFloorDisplay(int floor){
        mapImage.sprite = floor == 2 ? floor2 : floor1;
        playerIcon.gameObject.SetActive(floor == playerCurrentFloor);
    }

    public void UpdateMap(){
        if (!gameSpaceBoxCollider) return;
        // Calculate Player Position
        Vector3 playerPosition = Player.Instance.transform.position;

        var inMinMaxX = new Vector2(gameSpaceBounds.min.x, gameSpaceBounds.max.x);
        // var inMinMaxY = new Vector2(gameSpaceBounds.min.y, gameSpaceBounds.max.y);
        var inMinMaxZ = new Vector2(gameSpaceBounds.min.z, gameSpaceBounds.max.z);

        var outMinMaxX = new Vector2(-halfMapSpaceSize.x, halfMapSpaceSize.x);
        var outMinMaxY = new Vector2(-halfMapSpaceSize.y, halfMapSpaceSize.y);

        var playerMapPosition = new Vector2(
            UtilityMethods.RemapClamped(inMinMaxX, outMinMaxX, playerPosition.x),
            UtilityMethods.RemapClamped(inMinMaxZ, outMinMaxY, playerPosition.z)
        );

        playerCurrentFloor = playerPosition.y >= changeFloorHeight ? 2 : 1;

        toggleFloor1.isOn = playerCurrentFloor == 1;
        if (toggleFloor1.isOn)
        {
            legenda2.SetActive(false);
            legenda1.SetActive(true);
        }

        toggleFloor2.isOn = playerCurrentFloor == 2;
        if (toggleFloor2.isOn)
        {
            legenda1.SetActive(false);
            legenda2.SetActive(true);
        }
        // ChangeFloorDisplay(playerCurrentFloor);

        playerIcon.localPosition = playerMapPosition;
        playerIcon.localEulerAngles = Vector3.back * Player.Instance.transform.localEulerAngles.y;
    }

    public void DisplayFloor1(bool value){
        if(value) ChangeFloorDisplay(1);
    }
    
    public void DisplayFloor2(bool value){
        if(value) ChangeFloorDisplay(2);
    }
}