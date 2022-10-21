using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueSaveAndLoad
{
    public interface IEventList//接口，用于管理全局事件：静态事件：是发生或未发生的事件状态；动态事件：一个会对游戏产生影响的功能函数
    {
        public bool isStaticEvent(int index);//通过索引值检索该静态事件是否被触发
        public bool ActiveEvent(int index);//通过索引值触发该动态事件的功能函数,成功调用返回true
    }
}
