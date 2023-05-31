using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EngineLib;

namespace practice11
{

    public class TestCollections
    {
        private static IternCombEngine first, mid, last, another;
        public int Count { get => stack1.Count; }
        
        public Stack<IternCombEngine> stack1 = new Stack<IternCombEngine>();
        public Stack<string> stack2 = new Stack<string>();
        public Dictionary<Engine, IternCombEngine> dict1 = new Dictionary<Engine, IternCombEngine>();
        public Dictionary<string, IternCombEngine> dict2 = new Dictionary<string, IternCombEngine>();

        public TestCollections()
        {
            // заполнение коллекций 1000 элементов
            
            for (int i = 1; i <= 1001; i++)
            {
                IternCombEngine itcombeng = new IternCombEngine(1);
                
                if (dict2.ContainsKey(itcombeng.BaseEngine.Show()))
                {
                    i--;
                    continue;
                }

                if (i == 1) first = new IternCombEngine(itcombeng.Capacity, itcombeng.Power, itcombeng.Weight, itcombeng.IStock);
                else if (i == 500) mid = new IternCombEngine(itcombeng.Capacity, itcombeng.Power, itcombeng.Weight, itcombeng.IStock);
                else if (i == 1000) last = new IternCombEngine(itcombeng.Capacity, itcombeng.Power, itcombeng.Weight, itcombeng.IStock);
                else if (i == 1001)
                {
                    another = (new IternCombEngine(itcombeng.Capacity, itcombeng.Power, itcombeng.Weight, itcombeng.IStock));
                    break;
                }
                
                stack1.Push(itcombeng);
                stack2.Push(itcombeng.Show());
                dict1.Add(itcombeng.BaseEngine, itcombeng);
                dict2.Add(itcombeng.BaseEngine.Show(), itcombeng);
            }
        }

        #region MyRegion

        public void SearchEngs()
        {
            // first
            
            Console.WriteLine($"\nИзмерение времени для доступа к первому обьекту\n{first.Show()}");
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            bool flag = stack1.Contains(first);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Stack<Engine> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = stack2.Contains(first.Show());
            stopwatch.Stop();
            Console.WriteLine(flag + $" Stack<string> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = dict1.ContainsKey(first.BaseEngine);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Dictionary<Engine, IternCombEngine> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = dict1.ContainsValue(first);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Dictionary<string, IternCombEngine> : {stopwatch.ElapsedTicks} тактов");
            
            // mid
            
            Console.WriteLine($"\nИзмерение времени для доступа к обьекту по середине\n{mid.Show()}");
            
            stopwatch.Restart();
            flag = stack1.Contains(mid);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Stack<Engine> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = stack2.Contains(mid.Show());
            stopwatch.Stop();
            Console.WriteLine(flag + $" Stack<string> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = dict1.ContainsKey(mid.BaseEngine);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Dictionary<Engine, IternCombEngine> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = dict1.ContainsValue(mid);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Dictionary<string, IternCombEngine> : {stopwatch.ElapsedTicks} тактов");
            
            //  last
            
            Console.WriteLine($"\nИзмерение времени для доступа к последнему обьекту\n{last.Show()}");
            
            stopwatch.Restart();
            flag = stack1.Contains(last);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Stack<Engine> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = stack2.Contains(last.Show());
            stopwatch.Stop();
            Console.WriteLine(flag + $" Stack<string> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = dict1.ContainsKey(last.BaseEngine);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Dictionary<Engine, IternCombEngine> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = dict1.ContainsValue(last);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Dictionary<string, IternCombEngine> : {stopwatch.ElapsedTicks} тактов");
            
            // another
            
            Console.WriteLine($"\nИзмерение времени для доступа к обьекту отсутствующему в коллекции\n{another.Show()}");
            
            stopwatch.Restart();
            flag = stack1.Contains(another);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Stack<Engine> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = stack2.Contains(another.Show());
            stopwatch.Stop();
            Console.WriteLine(flag + $" Stack<string> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = dict1.ContainsKey(another.BaseEngine);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Dictionary<Engine, IternCombEngine> : {stopwatch.ElapsedTicks} тактов");
            
            stopwatch.Restart();
            flag = dict1.ContainsValue(another);
            stopwatch.Stop();
            Console.WriteLine(flag + $" Dictionary<string, IternCombEngine> : {stopwatch.ElapsedTicks} тактов");
        }
        
        public static void PrintDict(Dictionary<Engine, Engine> dict)
        {
            // печать словаря <int, Engine>
            Console.WriteLine("Печать очереди:\n");
            if (dict.Count > 0) //проверка НЕ пустоты
                foreach (Engine key in dict.Keys)
                    Console.WriteLine(key.Show() + " |" + dict[key].Show());
            else
                Console.WriteLine("Пустой словарь!");
        }

        public void AddElem()
        {
            IternCombEngine eng;
            do
            {
                eng = new IternCombEngine(1);
            } while (dict1.ContainsKey(eng.BaseEngine));
            Console.WriteLine("\nДобавление элемента :" + eng.Show());
            
            stack1.Push(eng);
            stack2.Push(eng.Show());
            dict1.Add(eng.BaseEngine, eng);
            dict2.Add(eng.BaseEngine.Show(), eng);
            
            Console.WriteLine($"\nРазмер коллекции : {stack1.Count}");
        }

        public void RemovElem()
        {
            IternCombEngine eng = dict1.Values.ToArray()[new Random().Next(0, dict1.Count)];
            Console.WriteLine("\nУдаление элемента :" + eng.Show());
            //stack1
            Stack<IternCombEngine> temp1 = new Stack<IternCombEngine>();
            while (!Equals(stack1.Peek(), eng))
            {
                temp1.Push(stack1.Peek());
                stack1.Pop();
            }
            stack1.Pop();
            while (temp1.Count != 0)
            {
                stack1.Push(temp1.Peek());
                temp1.Pop();
            }
            //stack2
            Stack<string> temp2 = new Stack<string>();
            while (!Equals(stack2.Peek(), eng.Show()))
            {
                temp2.Push(stack2.Peek());
                stack2.Pop();
            }
            stack2.Pop();
            while (temp2.Count != 0)
            {
                stack2.Push(temp2.Peek());
                temp2.Pop();
            }
            //dicts
            dict1.Remove(eng.BaseEngine);
            dict2.Remove(eng.BaseEngine.Show());
            
            Console.WriteLine($"\nРазмер коллекции : {stack1.Count}");
        }
        
        
        #endregion
    }
}