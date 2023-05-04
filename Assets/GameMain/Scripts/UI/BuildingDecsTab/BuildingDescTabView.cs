using System;
using System.Collections;
using System.Collections.Generic;
using QuickGameFramework.Runtime.UI;
using UnityEngine;

namespace OasisProject3D.UI.GameMainUIPackage {
    internal class BuildingDescTabView : View<BuildingDescTabUIData> {
        private BuildingDescTab _tab;

        protected override void Start() {
            base.Start();
            _tab = (BuildingDescTab)UIPanel.ui;
        }

        protected override void OnShow(BuildingDescTabUIData data) {
            throw new NotImplementedException();
        }

        protected override void OnHide(BuildingDescTabUIData data) {
            throw new NotImplementedException();
        }

        protected override void ProcessMessage(ValueType command, BuildingDescTabUIData data) {
            throw new NotImplementedException();
        }

        protected override bool OnBackButtonClicked() {
            throw new NotImplementedException();
        }
    }
}