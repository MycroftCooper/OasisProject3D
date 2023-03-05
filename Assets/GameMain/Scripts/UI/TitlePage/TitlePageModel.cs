using System;
using QuickGameFramework.Runtime.UI;

namespace UI {
    public class TitlePageModel : Model<TitlePageData> {
        protected override void OnShow(ValueType extraParams) {

        }

        protected override void OnHide(ValueType extraParams) {
            throw new NotImplementedException();
        }

        protected override void ProcessMessage(Message message) {
            TitlePageCommand command = (TitlePageCommand)message.Command;
            switch (command) {
            }
        }
    }
}
