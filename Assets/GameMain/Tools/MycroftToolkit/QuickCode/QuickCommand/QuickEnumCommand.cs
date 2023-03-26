using System;

namespace MycroftToolkit.QuickCode {
    // 命令调用者类
    public interface IEnumCommander<T>where T: Enum {
        public void CallCmd(IEnumCmdReceiver<T> receiver, T command);
    }
    
    // 命令接收者接口
    public interface IEnumCmdReceiver<in T>where T: Enum{
        void ExecuteCmd(T cmd);

        void ReflectExecuteCmd(T cmd) {
            this.InvokeMethod(cmd+"CommandHandler");
        }
    }
}
