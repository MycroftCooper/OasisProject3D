using UnityEngine;

public class BuildingConstructionController : MonoBehaviour {
    public Material buildingMaterial;
    public float constructionSpeed = 1f;
    public float currentProgress = 0f;

    public void Start() {
        GetComponent<MeshRenderer>().material = buildingMaterial;
    }

    void Update() {
        GetComponent<MeshRenderer>().material.SetFloat("Progress", currentProgress);
    }
}