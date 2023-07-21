using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIMinimap : MonoBehaviour
{
    [SerializeField]
    public Camera minimapCamera;
    [SerializeField]
    private float zoomMin = 1;          // ī�޶� orthographicSize �ּ� ũ��
    [SerializeField]
    private float zoomMax = 30;       // ī�޶� orthographicSize �ִ� ũ��
    [SerializeField]
    private float zoomOneStep = 1;  // 1ȸ �� �Ҷ� ����/���ҵǴ� ��ġ
    [SerializeField]
    private TextMeshProUGUI textMapName;

    private void Awake()
    {
           // �� �̸��� ���� �� �̸����� ����
           textMapName.text = SceneManager.GetActiveScene().name;
    }

    public void ZoomIn()
    {
        // ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰 ũ�� Ȯ��
        minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize - zoomOneStep, zoomMin);
    }
    public void ZoomOut()
    {
        // ī�޶��� orthographicSize ���� ���ҽ��� ī�޶� ���̴� �繰 ũ�� Ȯ��
        minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize - zoomOneStep, zoomMax);
    }
}
