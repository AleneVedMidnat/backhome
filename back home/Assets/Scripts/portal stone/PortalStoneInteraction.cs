using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PortalStoneInteraction : MonoBehaviour
{
    public bool portalStoneOn = false;
    [SerializeField] PortalStoneController m_portalStoneController;
    [SerializeField] GameObject m_partnerStoneConnector;
    [SerializeField] GameObject m_player;

    private void Start()
    {
        
    }
    void Update()
    {
        if (portalStoneOn == false)
        {
            if ((transform.position - m_player.transform.position).magnitude < 1)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    Debug.Log("key pressed stone");
                    portalStoneOn = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    m_portalStoneController.GetComponent<PortalStoneController>().AddPortalStone();
                    m_partnerStoneConnector.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
}
