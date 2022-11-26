using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalStoneController : MonoBehaviour
{
    int portalStonesOn = 0;
    bool portalOpen = false;

    public void AddPortalStone()
    {
        portalStonesOn++;
        if (portalStonesOn == 5)
        {
            portalOpen = true;
            //need to enable an object here
        }
    }


}
