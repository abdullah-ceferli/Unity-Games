using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject spawnerCity1;
    public GameObject spawnerCity2;
    public GameObject spawnerFreeRide;
    public GameObject spawnerSportRide;


    public void DeactivateAllSpawns()
    {
        if (spawnerCity1 != null) spawnerCity1.SetActive(false);
        if (spawnerCity2 != null) spawnerCity2.SetActive(false);
        if (spawnerFreeRide != null) spawnerFreeRide.SetActive(false);
        if (spawnerSportRide != null) spawnerSportRide.SetActive(false);

    }

    public void ActivateCity1()
    {
        DeactivateAllSpawns();
        if (spawnerCity1 != null) spawnerCity1.SetActive(true);
    }

    public void ActivateCity2()
    {
        DeactivateAllSpawns();
        if (spawnerCity2 != null) spawnerCity2.SetActive(true);
    }

    public void ActivateFreeRide()
    {
        DeactivateAllSpawns();
        if (spawnerFreeRide != null) spawnerFreeRide.SetActive(true);
    }
    
    public void ActivateSportRide()
    {
        DeactivateAllSpawns();
        if (spawnerSportRide != null) spawnerSportRide.SetActive(true);
    }
}
