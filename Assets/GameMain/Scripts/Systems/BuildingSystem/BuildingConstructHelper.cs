using UnityEngine;

namespace OasisProject3D.BuildingSystem {
    public class BuildingConstructHelper {
        private readonly BuildingCtrl _targetBuilding;

        private Material _buildingDefaultMat;
        private Material _buildingConstructMat;
        private Texture _buildingTex;
        private static readonly int BaseMapKey = Shader.PropertyToID("BaseMap");
        private static readonly int ProgressKey = Shader.PropertyToID("Progress");
        private static readonly int HeightKey = Shader.PropertyToID("_Height");

        public BuildingConstructHelper(BuildingCtrl targetBuilding) {
            _targetBuilding = targetBuilding;
            SetConstructMoveMaterial();
        }
        
        private void SetConstructMoveMaterial() {
            _buildingConstructMat = new Material(BuildingFactory.Instance.GetMaterial("construct_material"));
            _buildingConstructMat.name = _buildingConstructMat.name.Replace("(Instance)","");
            _buildingDefaultMat = new Material(_targetBuilding.BuildingMeshRenderer.material);
            _buildingDefaultMat.name = _buildingDefaultMat.name.Replace("(Instance)","");
            _buildingConstructMat.SetTexture(BaseMapKey, _buildingTex);
            float buildingHeight = _targetBuilding.BuildingMeshFilter.sharedMesh.bounds.size.y;
            _buildingConstructMat.SetFloat(HeightKey, buildingHeight);
            _targetBuilding.BuildingMeshRenderer.material = _buildingConstructMat;
        }
        
        private void UnsetConstructMoveMaterial() {
            _targetBuilding.BuildingMeshRenderer.material = _buildingDefaultMat;
            _buildingDefaultMat = null;
            _buildingConstructMat = null;
            _buildingTex = null;
        }

        public bool UpdateBuildingConstructProgress(float progress) {
            _buildingConstructMat.SetFloat(ProgressKey, progress);
            if (!(progress >= 1)) return false;
            UnsetConstructMoveMaterial();
            return true;
        }
    }
}