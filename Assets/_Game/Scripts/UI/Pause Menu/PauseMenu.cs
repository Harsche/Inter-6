using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour{
    private static bool GamePaused;
    [SerializeField] private OptionItems optionItems;

    private Canvas canvas;

    private void Awake(){
        canvas = GetComponent<Canvas>();
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){ TogglePause(!GamePaused); }
    }

    public void SetCameraSensitivity(float value){
        PlayerPrefs.SetFloat("CameraSensitivity", value);
    }

    public void TogglePause(bool value){
        if (!GamePaused && Cursor.lockState == CursorLockMode.None){ return; }

        GamePaused = value;
        GameManager.IsGamePaused = value;
        Time.timeScale = GamePaused ? 0 : 1;
        canvas.enabled = GamePaused;
        Cursor.lockState = GamePaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = GamePaused;
        UpdateOptionItems();
        if (!GamePaused){ return; }

        Map.Instance.UpdateMap();
    }

    private void UpdateOptionItems(){
        optionItems.SensitivitySlider.value = PlayerPrefs.GetFloat("CameraSensitivity");
    }

    [Serializable]
    private class OptionItems{
        [field: SerializeField] public Slider SensitivitySlider{ get; private set; }
        [field: SerializeField] public Slider VolumeSlider{ get; private set; }
    }
}