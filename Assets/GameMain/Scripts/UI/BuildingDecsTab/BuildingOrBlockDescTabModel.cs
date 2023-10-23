using System;
using System.Collections;
using System.Collections.Generic;
using cfg.BuildingSystem;
using OasisProject3D.BuildingSystem;
using OasisProject3D.ResourceSystem;
using QuickGameFramework.Runtime.UI;
using UnityEngine;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal class BuildingOrBlockDescTabModel : Model<BuildingOrBlockTabUIData> {
        private BuildingCtrl _currentBuilding;
        protected override void OnShow(ValueType extraParams) {
            if (extraParams is BuildingDescTabUIData @params) {
                data = new BuildingOrBlockTabUIData {
                    IsBlock = false,
                    BuildingData = @params,
                    BlockData = default,
                };
            } else {
                data = new BuildingOrBlockTabUIData {
                    IsBlock = true,
                    BuildingData = default,
                    BlockData = (BlockDescTabUIData)extraParams,
                };
            }
            if (data.IsBlock) {
                UpdateBlockData();
            } else {
                UpdateBuildingData();
            }
        }

        protected override void OnHide(ValueType extraParams) {
            _currentBuilding = null;
            data.IsBlock = false;
            data.BuildingData = default;
            data.BlockData = default;
            data.NeedHide = true;
        }

        protected override void ProcessMessage(Message message) {
            BuildingOrBlockTabUICmd cmd = (BuildingOrBlockTabUICmd) message.Command;
            switch (cmd) {
                case BuildingOrBlockTabUICmd.ChangeBuilding:
                    break;
                case BuildingOrBlockTabUICmd.DeleteBuilding:
                    if (_currentBuilding == null) {
                        return;
                    }
                    _currentBuilding.Delete();
                    OnHide(message.ExtraParams);
                    break;
                case BuildingOrBlockTabUICmd.UpgradeBuilding:
                    //todo: UpgradeBuilding
                    break;
                case BuildingOrBlockTabUICmd.RebuildBuilding:
                    //todo: RebuildBuilding
                    break;
                case BuildingOrBlockTabUICmd.MoveBuilding:
                    if (_currentBuilding == null) {
                        return;
                    }
                    // _currentBuilding.ExecuteCmd(BuildingCmd.Move);
                    OnShow(new BuildingDescTabUIData { Ctrl = _currentBuilding });
                    break;
                case BuildingOrBlockTabUICmd.SwitchBuildingOpenOrClose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UpdateBlockData(){}

        private void UpdateBuildingData() {
            BuildingManager mgr = GamePlayEnter.BuildingMgr;
            BuildingFactory factory = mgr.Factory;
            BuildingDescTabUIData buildingData = data.BuildingData;
            _currentBuilding = buildingData.Ctrl;
            var buildingKey = buildingData.Ctrl == null ? buildingData.BuildingID : _currentBuilding.ID;
            buildingData.BuildingName = factory.GetBuildingName(buildingKey);
            buildingData.BuildingDesc = factory.GetBuildingDesc(buildingKey);
            buildingData.BuildingIcon = factory.GetBuildingIcon(buildingKey);
            buildingData.BuildingState = buildingData.Ctrl == null ? null : buildingData.Ctrl.FsmCtrl.GetCurrentState().name;
            
            BuildingConfig config = factory.GetBuildingConfig(buildingKey);
            Dictionary<Sprite, Vector2Int> costIconDict = new Dictionary<Sprite, Vector2Int>();
            ResourceManager resMgr = GamePlayEnter.ResMgr;
            foreach (var (resType, targetNum) in config.BulidCost) {
                Sprite resIcon = resMgr.GetResIcon(resType);
                int currentNum = resMgr.GetResNum(resType);
                Vector2Int progress = new Vector2Int(currentNum, targetNum);
                costIconDict.Add(resIcon, progress);
            }
            buildingData.CostIconDict = costIconDict;
            
            if (buildingData.Ctrl == null) {
                data.BuildingData = buildingData;
                return;
            }
            
            
            
            data.BuildingData = buildingData;
        }
    }
}