using QuickGameFramework.Runtime.UI;

namespace UI {
    public class TitlePageCtrl : Controller {
        private void Start() {
            DispatchMessage(new Message{Command =  Message.CommonCommand.Show});
        }
    }
}