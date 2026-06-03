using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MoleManager : MonoBehaviour
{
    public Transform spawnPlane;
    public GameObject molePrefab;
    public Transform mole;
    ARPlane lastPlane = null;
    public ARPlaneManager arPlaneManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateAndLocateMole();
    }

    public void SetSpawnPlane()
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            lastPlane = plane;
        }

        spawnPlane = lastPlane?.transform;
    }

    public void CreateAndLocateMole()
    {
        StartCoroutine(co_CreateAndLocateMole());
    }

    IEnumerator co_CreateAndLocateMole()
    {
        yield return new WaitUntil(() => spawnPlane != null);
        while (true)
        {
            yield return new WaitForSeconds(5f);
            MeshCollider meshCollider = spawnPlane.GetComponent<MeshCollider>();
            Bounds bounds = meshCollider.bounds;

            Vector3 spawnPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),   // x
                bounds.center.y,                            // y
                Random.Range(bounds.min.z, bounds.max.z)    // z
            );

            if (mole == null)
            {
                mole = Instantiate(molePrefab, spawnPosition, Quaternion.identity).transform;
            }

            mole.position = spawnPosition;
        }
    }
}
