using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalStoneController : MonoBehaviour
{
    int portalStonesOn = 0;
    bool portalOpen = false;
    [SerializeField] Sprite portalOnSprite;
    [SerializeField] GameObject m_leaveUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (portalOpen == true)
        {
            m_leaveUI.SetActive(true);
        }
    }

    public void AddPortalStone()
    {
        portalStonesOn++;
        if (portalStonesOn >= 4)
        {
            portalOpen = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = portalOnSprite;
            //need to enable an object here
        }
    }


}
