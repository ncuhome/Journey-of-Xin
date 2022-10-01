

using System;

public class Core//小游戏核心
{
    int[] square = new int[] { 0, 2, 3, 2, 2, 4, 6, 6, 1, 3, 6, 6, 5, 4, 1, 4 };
    int cursor = 15;//当前光标（空格）的索引值
    //左下角为起点

    public Core()
    {

    }
    
    public void tell()
    {
        for(int i=0;i<16;i++)
        {
            Console.Write(" " + square[i]+" ");
            if (i % 4 == 3) { Console.WriteLine(); }
        }
    }
    public void Up()//空格向上交换
    {
        if(cursor > 3)//光标不在最顶上
        {
            square[cursor] = square[cursor-4];
            square[cursor - 4] = 0;
            cursor -= 4;
        }
    }

    public void Down()//空格向下交换
    {
        if(cursor < 12)//光标不在最下方
        {
            square[cursor] = square[cursor + 4];
            square[cursor + 4] = 0;
            cursor += 4;
        }
    }

    public void Left()//空格向左交换
    {
        if (cursor % 4 != 0)//光标不在最左边
        {
            square[cursor] = square[cursor - 1];
            square[cursor - 1] = 0;
            cursor--;
        }
    }

    public void Right()//空格向右交换
    {
        if (cursor % 4 != 3)//光标不在最右边
        {
            square[cursor] = square[cursor + 1];
            square[cursor + 1] = 0;
            cursor++;
        }
    }

    public bool isSuccess()//判定是否解谜成功
    {
        for(int i=0;i<15;i++)
        {
            if (square[i] != i) { return false; }
        }
        return true;
    }
}