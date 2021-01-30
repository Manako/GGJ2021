using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
    [Tooltip("Root GameObject of the menu used to toggle its activation")]
    public GameObject menuRoot;
    [Tooltip("Slider component for look sensitivity")]
    public Slider lookSensitivitySlider;
    [Tooltip("GameObject for the controls")]
    public GameObject controlImage;

    MouseHandler m_MouseHandler;

    void Start()
    {
        m_MouseHandler = FindObjectOfType<MouseHandler>();

        menuRoot.SetActive(false);

        lookSensitivitySlider.value = m_MouseHandler.lookSensitivity;
        lookSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);
    }

    private void Update()
    {
        // Lock cursor when clicking outside of menu
        if (!menuRoot.activeSelf && Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetButtonDown(GameConstants.k_ButtonNamePauseMenu)
            || (menuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
        {
            if (controlImage.activeSelf)
            {
                controlImage.SetActive(false);
                return;
            }

            SetPauseMenuActivation(!menuRoot.activeSelf);

        }

        if (Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                lookSensitivitySlider.Select();
            }
        }
    }

    public void ClosePauseMenu()
    {
        SetPauseMenuActivation(false);
    }

    void SetPauseMenuActivation(bool active)
    {
        menuRoot.SetActive(active);

        if (menuRoot.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }

    }

    void OnMouseSensitivityChanged(float newValue)
    {
        m_MouseHandler.lookSensitivity = newValue;
    }

    public void OnShowControlButtonClicked(bool show)
    {
        controlImage.SetActive(show);
    }
}
