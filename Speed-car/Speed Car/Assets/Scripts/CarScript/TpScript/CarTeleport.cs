using UnityEngine;

public class CarTeleport : MonoBehaviour
{
    public Camera mainCamera; // Камера
    public Transform parkingPosition; // Позиция парковки (City 1)
    public Transform cityPosition;    // Позиция второго города (City 2)
    public Transform freeRidePosition;
    public Transform sportRidePosition;
    public GameObject car; // Машина

    public SpawnerManager spawnerManager; // Ссылка на менеджер спавнов

    public void TeleportToCity()
    {
        car.transform.position = cityPosition.position;
        car.transform.rotation = cityPosition.rotation;

        if (mainCamera != null)
        {
            mainCamera.transform.position = car.transform.position + new Vector3(0, 5, -10);
            mainCamera.transform.LookAt(car.transform);
        }

        if (spawnerManager != null)
        {
            spawnerManager.ActivateCity2(); // Включаем спавн города 2
        }
    }

    public void TeleportToParking()
    {
        car.transform.position = parkingPosition.position;
        car.transform.rotation = parkingPosition.rotation;

        if (mainCamera != null)
        {
            mainCamera.transform.position = car.transform.position + new Vector3(0, 5, -10);
            mainCamera.transform.LookAt(car.transform);
        }

        if (spawnerManager != null)
        {
            spawnerManager.ActivateCity1(); // Включаем спавн города 1
        }
    }

    public void TeleportToFreeRide()
    {
        car.transform.position = freeRidePosition.position;
        car.transform.rotation = freeRidePosition.rotation;

        if (mainCamera != null)
        {
            mainCamera.transform.position = car.transform.position + new Vector3(0, 5, -10);
            mainCamera.transform.LookAt(car.transform);
        }

        if (spawnerManager != null)
        {
            spawnerManager.ActivateFreeRide(); // Включаем спавн для спортивной трассы
        }
    }

    public void TeleportToSportRide()
    {
        car.transform.position = sportRidePosition.position;
        car.transform.rotation = sportRidePosition.rotation;

        if (mainCamera != null)
        {
            mainCamera.transform.position = car.transform.position + new Vector3(0, 5, -10);
            mainCamera.transform.LookAt(car.transform);
        }

        if (spawnerManager != null)
        {
            spawnerManager.ActivateSportRide(); // Включаем спавн для спортивной трассы
        }
    }
}
