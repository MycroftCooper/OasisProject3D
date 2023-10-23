using FairyGUI;
using OasisProject3D.BuildingSystem;
using QuickGameFramework.Runtime;
using QuickGameFramework.Runtime.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Controller = QuickGameFramework.Runtime.UI.Controller;

namespace OasisProject3D.UI.GameMainUIPackage {
    public class BuildingOrBlockDescTabCtrl : Controller {
        private BuildingOrBlockDescTab _tab;

        private void Start() {
            _tab = (BuildingOrBlockDescTab)UIPanel.ui;
            
            var mainGameCtrlMap = GameEntry.InputMgr.playerInput.actions.FindActionMap("MainGameCtrl");
            mainGameCtrlMap.FindAction("Click").performed += OnClickedInMap;
            
            _tab.NextBuilding.onClick.Add(OnNextBuildingBtnClicked);
            _tab.LastBuilding.onClick.Add(OnLastBuildingBtnClicked);
            
            _tab.DeleteBtn.onClick.Add(OnDeleteBuildingBtnClicked);
            _tab.UpgradeBtn.onClick.Add(OnUpgradeBuildingBtnClicked);
            _tab.RebuildBtn.onClick.Add(OnRebuildBuildingBtnClicked);
            _tab.MoveBtn.onClick.Add(OnMoveBuildingBtnClicked);
            _tab.OpenCloseSwitchBtn.onClick.Add(OnSwitchBuildingBtnClicked);
        }

        public void CloseTab() {
            DispatchMessage(new Message {Command = Message.CommonCommand.Hide});
        }

        #region 事件响应
private void OnClickedInMap(InputAction.CallbackContext obj) {
            if (Stage.isTouchOnUI) {
                return;
            }

            Camera mainCamera = Camera.main;
            if (mainCamera == null) {
                QLog.Error("BuildingMoveHelper>Error>主摄像机为空！");
                return;
            }

            Vector3 mousePosition = obj.ReadValue<Vector2>();
            Ray mouseRay = mainCamera.ScreenPointToRay(mousePosition);
            var layerMask = LayerMask.GetMask("Block", "Building");
            if (!Physics.Raycast(mouseRay, out var hit, 5000f, layerMask, QueryTriggerInteraction.Collide)) {
                CloseTab();
                return;
            }

            int targetLayer = hit.transform.gameObject.layer;
            if (targetLayer == LayerMask.NameToLayer("Building")) {
                var targetBuilding = hit.transform.GetComponent<BuildingCtrl>();
                DispatchMessage(new Message {
                    Command = Message.CommonCommand.Show,
                    ExtraParams = new BuildingDescTabUIData {
                        Ctrl = targetBuilding
                    }   
                });
                return;
            }
            CloseTab();
            // todo: 打开地块详情界面
            // if (targetLayer == LayerMask.NameToLayer("Block")) {
            //     var targetBlock = hit.transform.GetComponent<BlockCtrl>();
            //     DispatchMessage(new Message {
            //         Command = BuildingDescUICommand.OpenBlockTab,
            //         ExtraParams = new BlockDescTabUIData {
            //             Ctrl = targetBlock
            //         }   
            //     });
            // }
        }

        public void OnOverBuildingBtnInBuildingList(string buildingKey) {
            DispatchMessage(new Message {
                Command = Message.CommonCommand.Show,
                ExtraParams = new BuildingDescTabUIData { BuildingID = buildingKey }
            });
        }

        private void OnNextBuildingBtnClicked() {
            DispatchMessage(new Message {
                Command = BuildingOrBlockTabUICmd.ChangeBuilding,
                ExtraParams = true
            });
        }

        private void OnLastBuildingBtnClicked() {
            DispatchMessage(new Message {
                Command = BuildingOrBlockTabUICmd.ChangeBuilding,
                ExtraParams = false
            });
        }

        private void OnDeleteBuildingBtnClicked() {
            DispatchMessage(new Message {
                Command = BuildingOrBlockTabUICmd.DeleteBuilding
            });
        }

        private void OnUpgradeBuildingBtnClicked() {
            DispatchMessage(new Message {
                Command = BuildingOrBlockTabUICmd.UpgradeBuilding
            });
        }

        private void OnRebuildBuildingBtnClicked() {
            DispatchMessage(new Message {
                Command = BuildingOrBlockTabUICmd.RebuildBuilding
            });
        }

        private void OnMoveBuildingBtnClicked() {
            DispatchMessage(new Message {
                Command = BuildingOrBlockTabUICmd.MoveBuilding
            });
        }

        private void OnSwitchBuildingBtnClicked() {
            DispatchMessage(new Message {
                Command = BuildingOrBlockTabUICmd.SwitchBuildingOpenOrClose
            });
        }
        #endregion
    }
}
