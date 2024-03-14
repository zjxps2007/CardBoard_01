using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public Image CursorGaugeImage;
    private Vector3 ScreenCenter;
    private float GaugeTimer;

    private bool isTriggered = false;
    public GameObject Sphere;
    public Texture TextureImamge;
    
    // Start is called before the first frame update
    void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        RaycastHit hit;

        CursorGaugeImage.fillAmount = GaugeTimer;

        isTriggered = Input.GetMouseButtonDown(0);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.collider.CompareTag("Box"))
            {
                GaugeTimer += 0.33f * Time.deltaTime;
                if (GaugeTimer >= 1.0f || isTriggered)
                {
                 // hit.transform.gameObject.SetActive(false);
                 Sphere.GetComponent<Renderer>().material.SetTexture("_MainTex", TextureImamge);
                 GaugeTimer = 0.0f;
                 isTriggered = false;
                }
            }
            else if (hit.collider.CompareTag("Button"))
            {
                GaugeTimer += 0.33f * Time.deltaTime;
                if (GaugeTimer >= 1.0f || isTriggered)
                {
                    Debug.Log("hit");
                    hit.transform.GetComponent<Button>().onClick.Invoke();
                    GaugeTimer = 0.0f;
                    isTriggered = false;
                }
            }
            else
            {
                GaugeTimer = 0.0f;
                isTriggered = false;
            }
        }
    }
}
