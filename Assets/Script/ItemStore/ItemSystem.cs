using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem
{
    public static string ItemDescription(int id)//获取物品的描述
    {
        switch (id)
        {
            case 0: return "什么都没有哦~";
            case 1: return "ce的传家宝，可以在意识层面复活ce";
            case 2: return "地球的饮品，有种神奇的魅力";
            case 3: return "据说是地球曾经使用的交流工具，现在代指ce的记忆游戏中恢复记忆的药品，易溶，冲服有效";
            case 4: return "据说是地球曾经使用的交流工具，现在代指ce的记忆游戏中失去记忆的药品，易溶，冲服有效";
            case 5: return "超强化的信，效果是普通信的亿倍，谨慎使用";
            case 6: return "超强化的邮，效果是普通邮的亿倍，谨慎使用";
            case 7: return "香甜可口的咖啡，醇香四溢";
            case 8: return "香甜可口的咖啡，醇香四溢，添加了信";
            case 9: return "香甜可口的咖啡，醇香四溢，添加了邮";
            case 10: return "危险的咖啡，添加了最后的信";
            case 11: return "危险的咖啡，添加了最后的邮";
            case 12: return "含有某个信息的符号，或许是某个密码箱的密码";
            case 13: return "拥有中型火力，可以与哨站的副炮媲美";
            case 14: return "你好吗，怎么称呼呢，后世的我，那个银河联盟到底是哪一世搞出来的，我实在是打理不来，我先选择结束我的记忆游戏了，你加油吧，我做的信会告诉你怎么做的";
            case 15: return "超小份的邮，不足以失去大量记忆，有强镇静作用";
            case 16: return "含有某个信息的符号，或许是某个密码箱的密码";
            case 17: return "ce的传家宝，可以在意识层面复活ce，不过现在被改装，与kown绑定";
            case 18: return "原子不稳定的晶体，持续释放能量，极易发生核爆炸";
            case 19: return "原子排列紧密的晶体，核反应时可控、高效";
            case 20: return "利用射线反射观察的镜头，可以穿过遮盖物";
            case 21: return "技术不成熟的核反应堆";
            case 22: return "ce制作意识装置时用到的技术装置，可以改装或者制作复活装置";
            case 23: return "技术成熟的核反应堆，装入了高效的反应材料";
            case 24: return "不稳定的核反应堆，用久了会发生爆炸";
            case 25: return "含有某个信息的符号，或许是某个密码箱的密码";
            case 26: return "含有某个信息的符号，或许是某个密码箱的密码";
            case 27: return "含有某个信息的符号，或许是某个密码箱的密码";
            case 28: return "香甜可口的咖啡，醇香四溢 \n由咖啡合成";
            case 29: return "香甜可口的咖啡，醇香四溢，添加了信 \n由咖啡与信合成";
            case 30: return "香甜可口的咖啡，醇香四溢，添加了邮 \n由咖啡与邮合成";
            case 31: return "香甜可口的咖啡，醇香四溢，添加了信 \n由咖啡与邮箱里的信合成";
            case 32: return "香甜可口的咖啡，醇香四溢，添加了邮 \n由咖啡与飞船里的邮合成";
            case 33: return "危险的咖啡，添加了最后的信 \n由咖啡与最后的信合成";
            case 34: return "危险的咖啡，添加了最后的邮 \n由咖啡与最后的邮合成";
            case 35: return "ce的传家宝，可以在意识层面复活ce，不过现在被改装，与kown绑定 \n由复活装置（Ce）与中微子调试器";
            case 36: return "含有某个信息的符号，或许是某个密码箱的密码 \n由复活装置（Ce）与透视镜头合成";
            case 37: return "含有某个信息的符号，或许是某个密码箱的密码 \n由复活装置（Kown）与透视镜头合成";
            case 38: return "不稳定的核反应堆，用久了会发生爆炸 \n由炸药原材料合成";
            case 39: return "技术成熟的核反应堆，装入了高效的反应材料 \n由能量矿石和无尽能源制作装置合成";
        }
        return "没有相关描述";
    }
    public static string ItemName(int id)//获取物品的名称
    {
        switch (id)
        {
            case 0: return "";
            case 1: return "复活装置（Ce）";
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
            case 16: return "主控室隐含的数字";
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
            case 28: return "冲泡好的咖啡";
            case 29: return "回忆咖啡";
            case 30: return "失忆咖啡";
            case 31: return "回忆咖啡";
            case 32: return "失忆咖啡";
            case 33: return "最后的回忆咖啡";
            case 34: return "最后的失忆咖啡";
            case 35: return "复活装置(Kown)";
            case 36: return "复活装置隐含的数字";
            case 37: return "复活装置隐含的数字";
            case 38: return "炸药";
            case 39: return "无尽能源";
        }
        return "未知物品";
    }
    public static Sprite ItemSprite(int id)//获取物品的贴图
    {
        switch (id)
        {
            case 0: return Resources.Load<Sprite>("Image/Item/空物品");
            case 1: return Resources.Load<Sprite>("Image/Item/复活装置【博士");
            case 2: return Resources.Load<Sprite>("Image/Item/咖啡");
            case 3: return Resources.Load<Sprite>("Image/Item/信");
            case 4: return Resources.Load<Sprite>("Image/Item/邮");
            case 5: return Resources.Load<Sprite>("Image/Item/最后的信");
            case 6: return Resources.Load<Sprite>("Image/Item/最后的邮");
            case 7: return Resources.Load<Sprite>("Image/Item/冲泡好的咖啡");
            case 8: return Resources.Load<Sprite>("Image/Item/回忆咖啡");
            case 9: return Resources.Load<Sprite>("Image/Item/失忆咖啡");
            case 10: return Resources.Load<Sprite>("Image/Item/最后的回忆咖啡");
            case 11: return Resources.Load<Sprite>("Image/Item/最后的失忆咖啡");
            case 12: return Resources.Load<Sprite>("Image/Item/飞船隐藏数字");
            case 13: return Resources.Load<Sprite>("Image/Item/武器");
            case 14: return Resources.Load<Sprite>("Image/Item/邮箱里的信");
            case 15: return Resources.Load<Sprite>("Image/Item/飞船里的邮");
            case 16: return Resources.Load<Sprite>("Image/Item/主控室隐藏数字");
            case 17: return Resources.Load<Sprite>("Image/Item/复活装置【Know");
            case 18: return Resources.Load<Sprite>("Image/Item/炸药原材料");
            case 19: return Resources.Load<Sprite>("Image/Item/能量矿石");
            case 20: return Resources.Load<Sprite>("Image/Item/透视镜头");
            case 21: return Resources.Load<Sprite>("Image/Item/无尽能源制作装置");
            case 22: return Resources.Load<Sprite>("Image/Item/中微子调试器");
            case 23: return Resources.Load<Sprite>("Image/Item/无尽能源");
            case 24: return Resources.Load<Sprite>("Image/Item/炸药");
            case 25: return Resources.Load<Sprite>("Image/Item/矿石星球隐含的数字");
            case 26: return Resources.Load<Sprite>("Image/Item/黑市隐藏数字");
            case 27: return Resources.Load<Sprite>("Image/Item/复活装置隐藏数字");
            case 28: return Resources.Load<Sprite>("Image/Item/冲泡好的咖啡"); ;
            case 29: return Resources.Load<Sprite>("Image/Item/回忆咖啡"); ;
            case 30: return Resources.Load<Sprite>("Image/Item/失忆咖啡"); ;
            case 31: return Resources.Load<Sprite>("Image/Item/回忆咖啡"); ;
            case 32: return Resources.Load<Sprite>("Image/Item/失忆咖啡"); ;
            case 33: return Resources.Load<Sprite>("Image/Item/最后的回忆咖啡"); ;
            case 34: return Resources.Load<Sprite>("Image/Item/最后的失忆咖啡"); ;
            case 35: return Resources.Load<Sprite>("Image/Item/复活装置【Know"); ;
            case 36: return Resources.Load<Sprite>("Image/Item/复活装置隐藏数字"); ;
            case 37: return Resources.Load<Sprite>("Image/Item/复活装置隐藏数字"); ;
            case 38: return Resources.Load<Sprite>("Image/Item/炸药"); ;
            case 39: return Resources.Load<Sprite>("Image/Item/无尽能源");
        }
        return Resources.Load<Sprite>("Image/Item/错误贴图");
    }
    public static bool MakeFormula(int id, int[] store)//判定是否可合成该物品 物品配方id 背包物品组
    {
        foreach (int item in FormulaItem(id))
        {
            if (!StoreSystem.Instance.Find(item)) { return false; }
        }
        return true;
    }
    public static int[] FormulaItem(int id)//获取合成该物品的 配方物体id表
    {
        switch (id)
        {
            case 28:
                return new int[] { 2 };
            case 29:
                return new int[] { 2, 3 };
            case 30:
                return new int[] { 2, 4 };
            case 31:
                return new int[] { 2, 14 };
            case 32:
                return new int[] { 2, 15 };
            case 33:
                return new int[] { 2, 5 };
            case 34:
                return new int[] { 2, 6 };
            case 35:
                return new int[] { 1, 22 };
            case 36:
                return new int[] { 1, 20 };
            case 37:
                return new int[] { 17, 20 };
            case 38:
                return new int[] { 18 };
            case 39:
                return new int[] { 19, 21 };
        }
        return new int[] { 0 };
    }

    public static int[] FormulaToItem(int id)
    {
        switch (id)
        {
            case 28:
                return new int[] { 7 };
            case 29:
                return new int[] { 8 };
            case 30:
                return new int[] { 9 };
            case 31:
                return new int[] { 8 };
            case 32:
                return new int[] { 9 };
            case 33:
                return new int[] { 10 };
            case 34:
                return new int[] { 11 };
            case 35:
                return new int[] { 17 };
            case 36:
                return new int[] { 1, 20, 27 };
            case 37:
                return new int[] { 17, 20, 27 };
            case 38:
                return new int[] { 24 };
            case 39:
                return new int[] { 23 };
        }
        return new int[] { 0 };
    }
}