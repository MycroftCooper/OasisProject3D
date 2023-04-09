using QuickGameFramework.Runtime.UI;

namespace OasisProject3D.UI.GameMainUIPackage {
    public class MainPageCtrl: Controller  {
        private void Start() {
            DispatchMessage(new Message{Command =  Message.CommonCommand.Show});
        }
    }
}