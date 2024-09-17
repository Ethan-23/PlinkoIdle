using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIScoreDisplay : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject textContainer;
    [SerializeField] GameObject scoreTextFolder;
    [SerializeField] int maxDisplay = 20;
    List<GameObject> scoreDisplays = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPointGain(Vector3 position, float amount)
    {
        Vector3 screenPosition = cam.WorldToScreenPoint(position);
        screenPosition += new Vector3(0, 10f, 0);
        GameObject scoreTextContainer = Instantiate(textContainer, screenPosition, Quaternion.identity);
        scoreDisplays.Add(scoreTextContainer);
        CleanDisplays();
        TMP_Text scoreText = scoreTextContainer.GetComponentInChildren<TMP_Text>();
        scoreText.text = "+ " + Math.Round(amount,2).ToString();
        scoreTextContainer.transform.SetParent(scoreTextFolder.transform);
    }

    public void CleanDisplays()
    {
        if (scoreDisplays.Count > maxDisplay)
        {
            GameObject temp = scoreDisplays[0];
            scoreDisplays.RemoveAt(0);
            GameObject.Destroy(temp);
        }
    }
}
