using System;
namespace Assignment1
{
    public abstract class Form
    {
        public Form()
        {
        }
        public void WriteAt(string str, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(str);
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}
