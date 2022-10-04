using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public string ItemDescription(int id)//获取物品的描述
    {
        switch(id)
        {
            case 1:return "：普通的咖啡，由一种生在在‘地球’的特殊植物的种子加工制成，貌似对不是人类的Ce博士同样有提神作用？";
            case 2:return "：血红色的朗姆酒，相比普通的朗姆酒经过更长时间的发酵，在Kens星球的某个时期是贵族们专享的饮料";
        }
        return "没有相关描述";
    }
    public string ItemName(int id)//获取物品的名称
    {
        switch(id)
        {
            case 1:return "咖啡";
            case 2:return "血色朗姆酒";
        }
        return "";
    }
    public Sprite ItemSprite(int id)//获取物品的贴图
    {
        switch(id)
        {
            case 0: return Resources.Load<Sprite>("Image/Item/空物品");
            case 1: return Resources.Load<Sprite>("Image/Item/咖啡");
            case 2: return Resources.Load<Sprite>("Image/Item/朗姆酒");
        }
        return Resources.Load<Sprite>("Image/Item/错误贴图");
    }
}
