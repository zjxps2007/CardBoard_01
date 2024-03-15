using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public Image CursorGaugeImage;
    private Vector3 ScreenCenter;
    private float GaugeTimer;

    private AudioSource audioSource;

    public Text TextUI;
    private bool isTriggered = false;
    
    
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
                    hit.transform.gameObject.SetActive(false);
                    
                    Application.LoadLevel(1);
                    
                 GaugeTimer = 0.0f;
                 isTriggered = false;
                }
            }
            else if (hit.collider.CompareTag("Object") || hit.collider.CompareTag("Car") || hit.collider.CompareTag("Tree") || hit.collider.CompareTag("House"))
            {
                TextHit(hit.collider);
            }
            else
            {
                TextUI.text = "";
                GaugeTimer = 0.0f;
            }
        }
    }
    
    void TextHit(Collider collider)
    {
        GaugeTimer += 0.33f * Time.deltaTime;
        if (GaugeTimer >= 1.0f || isTriggered)
        {
            TextUI.text = collider.GetComponent<ObjectText>().text;
            audioSource = collider.GetComponent<AudioSource>();
            audioSource.Play();
            GaugeTimer = 0.0f;
            isTriggered = false;
        }
    }
    
}
