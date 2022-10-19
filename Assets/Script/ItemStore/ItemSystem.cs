using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public static string ItemDescription(int id)//获取物品的描述
    {
        switch(id)
        {
            case 1:return "没有相关描述";
            case 2:return "朴素的咖啡，别无特别之处";
            case 3: return "据说某个星球上的文明曾用此类古老的工具传递性息";
            case 4: return "邮";
            case 5: return "最后的信";
            case 6: return "最后的邮";
            case 7: return "冲泡好的咖啡";
            case 8: return "回忆咖啡";
            case 9: return "失忆咖啡";
            case 10: return "最后的回忆咖啡";
            case 11: return "最后的失忆咖啡";
            case 12: return "没有相关描述";
            case 13: return "没有相关描述";
            case 14: return "没有相关描述";
            case 15: return "没有相关描述";
            case 16: return "没有相关描述";
            case 17: return "没有相关描述";
            case 18: return "没有相关描述";
            case 19: return "没有相关描述";
            case 20: return "没有相关描述";
            case 21: return "没有相关描述";
            case 22: return "没有相关描述";
            case 23: return "没有相关描述";
            case 24: return "没有相关描述";
            case 25: return "没有相关描述";
            case 26: return "没有相关描述";
            case 27: return "没有相关描述";

        }
        return "没有相关描述";
    }
    public static string ItemName(int id)//获取物品的名称
    {
        switch(id)
        {
            case 0: return "";
            case 1: return "复活装置（博士）";
            case 2: return "咖啡";
            case 3: return "信";
            case 4: return "邮";
            case 5: return "最后的信";
            case 6: return "最后的邮";
            case 7: return "冲泡好的咖啡";
            case 8: return "回忆咖啡";
            case 9: return "失忆咖啡";
            case 10: return "最后的回忆咖啡";
            case 11: return "最后的失忆咖啡";
            case 12: return "飞船背景中隐含的数字";
            case 13: return "武器";
            case 14: return "信箱里的信";
            case 15: return "飞船里的邮";
            case 16: return "基地隐含的数字";
            case 17: return "复活装置（Kown）";
            case 18: return "炸药原材料";
            case 19: return "能量矿石";
            case 20: return "透视镜头";
            case 21: return "无尽能源制作装置";
            case 22: return "中微子调试器";
            case 23: return "无尽能源";
            case 24: return "炸药";
            case 25: return "矿石星球隐含的数字";
            case 26: return "黑市隐含的数字";
            case 27: return "复活装置隐含的数字";
        }
        return "未知物品";
    }
    public static Sprite ItemSprite(int id)//获取物品的贴图
    {
        switch(id)
        {
            case 0: return Resources.Load<Sprite>("Image/Item/空物品");
            case 1: return Resources.Load<Sprite>("Image/Item/复活装置（博士）");
            case 2: return Resources.Load<Sprite>("Image/Item/朗姆酒");
        }
        return Resources.Load<Sprite>("Image/Item/错误贴图");
    }
    public static bool MakeFormula(int id, int[] store)//判定是否可合成该物品 物品配方id 背包物品组
    {
        switch(id)
        {
            case 2:
                if (StoreSystem.Find(1)) { return true; }
                return false;
        }
        return false;
    }
    public static int[] FormulaItem(int id)//获取合成该物品的 配方物体id表
    {
        switch(id)
        {
            case 7:
                return new int[] { 2 };
            case 8:
                return new int[] { 2,3 };
            case 9:
                return new int[] { 2,4 };
            case 10:
                return new int[] { 2,5 };
            case 11:
                return new int[] { 2,6 }; 
        }
        return new int[] { 0 };
    }
}
