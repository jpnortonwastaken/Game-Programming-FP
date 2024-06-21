using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipText : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text tooltipTextMeshPro; 
    private Vector3 lastPlayerPosition;
    public GameObject player; 
    public float movementThreshold = 0.1f; 
    void Start()
    {
        tooltipTextMeshPro.gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 lastPlayerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasPlayerMoved())
        {
            HideTooltip();
        }
    }

    private bool HasPlayerMoved()
    {
        Vector3 currentPosition = player.transform.position;
        float distance = Vector3.Distance(currentPosition, lastPlayerPosition);

        // Return true if the player has moved beyond the threshold
        return distance > movementThreshold;
    }

    private void HideTooltip()
    {
        tooltipTextMeshPro.gameObject.SetActive(false);

        // Optionally, stop checking for movement
        enabled = false;
    }
}
