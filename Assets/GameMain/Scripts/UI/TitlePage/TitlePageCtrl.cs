using QuickGameFramework.Runtime.UI;

namespace OasisProject3D.UI {
    public class TitlePageCtrl : Controller {
        private void Start() {
            DispatchMessage(new Message{Command =  Message.CommonCommand.Show});
        }
    }
}