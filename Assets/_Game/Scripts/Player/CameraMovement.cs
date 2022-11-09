using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerRef;
    [SerializeField] float sensitivityCam;

    private float camY;

    public float valueCamY;
    public float valueCamX;

    public void CameraMove()
    {
        if(Player.Instance.PlayerMovement.stopInput) return;
        float finalSensitivity = sensitivityCam * (1 + PlayerPrefs.GetFloat("CameraSensitivity"));

        playerRef.Rotate(0, valueCamX * finalSensitivity * Time.deltaTime, 0); //Movimento de camera horizontal

        camY += -valueCamY * finalSensitivity * Time.deltaTime; //Movimento da camera vertical

        camY = Mathf.Clamp(camY, -75, 75); //Angulo vertical min e max da camera

        transform.localEulerAngles = new Vector3(camY, 0, 0);
    }
}
