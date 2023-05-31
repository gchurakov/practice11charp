using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EngineLib;

namespace practice11
{
    internal class Program
    {

        #region Queue

        public static Queue InitQueue()
        {
            // Создание очереди двигателей
            Queue queue = new Queue();
            for (int i = 0; i < 10; i++) //инициализация элементов очереди
            {
                switch ((new Random()).Next() % 4)
                {
                    case 0:
                        queue.Enqueue(new Engine(1));
                        break;
                    case 1:
                        queue.Enqueue(new IternCombEngine(1));
                        break;
                    case 2:
                        queue.Enqueue(new DieselEngine(1));
                        break;
                    case 3:
                        queue.Enqueue(new TurboJetEngine(1));
                        break;
                }
            }

            return queue;
        }

        public static Queue SortQueue(Queue queue)
        {
            // сортировка очереди при помощи интерфейса ICompareable
            object[] objArray = queue.ToArray();
            Engine[] engArray = new Engine[objArray.Length];

            for (int i = 0; i < objArray.Length; i++)
                engArray[i] = (Engine) objArray[i];

            Array.Sort(engArray);
            Console.WriteLine("Очередь отсортирована по мощности двигателей.");
            return new Queue(engArray);
        }

        public static void SearchEng(Queue queue)
        {
            //поиск двигателя опред. мощности
            bool isFound = false;
            int power = InputData.InputIntNumber("Введите мощность двигателя для поиска", "Число введено неверно");
            Queue cloneQueue = (Queue) queue.Clone();
            while (cloneQueue.Count > 0)
            {
                Engine eng = ((Engine) cloneQueue.Dequeue());
                if (eng.Power == power)
                {
                    isFound = true;
                    Console.WriteLine(eng.Show());
                }
            }

            if (!isFound)
                Console.WriteLine($"Двигатели с мощностью '{power}' не найдены!");
        }

        public static void AskQueue(Queue queue)
        {
            // запросы в очередь
            static void GetNStock(Queue engines)
            {
                // количество двигат в стоке
                int amountStock = 0;
                IternCombEngine temp;
                foreach (var engine in engines)
                    if (engine is IternCombEngine)
                    {
                        temp = engine as IternCombEngine;
                        if (temp.IStock)
                            amountStock++;
                    }
                Console.WriteLine($"\nКоличество Двигателей Внутреннего сгорания в Стоковом сотоянии: {amountStock}");
            }

            static void GetNDiesel(Queue engines)
            {
                // количество дизелей
                int amountDiesel = 0;
                foreach (var engine in engines)
                    if (engine is DieselEngine)
                        amountDiesel++;
                Console.WriteLine($"\nКоличество Дизельных Двигателей : {amountDiesel}");
            }

            static void GetAverPowerICombEngs(Queue engines)
            {
                // средняя мощность двигателей внутр сгорания
                int allPower = 0, amountItCombEng = 0;
                foreach (var engine in engines)
                    if (engine is IternCombEngine)
                    {
                        allPower = allPower + (engine as Engine).Power;
                        amountItCombEng++;
                    }
                Console.WriteLine($"\nСредняя мощность Двигателей Внутреннего сгорания : {(double)allPower/amountItCombEng} Л.С.");
            }
            
            GetNStock(queue);
            GetNDiesel(queue);
            GetAverPowerICombEngs(queue);
        }

        public static void PrintQueue(Queue queue)
        {
            // печать очереди элементов типа Engine
            Console.WriteLine("Печать очереди:\n");
            if (queue.Count > 0) //проверка НЕ пустоты
                foreach (Engine eng in queue)
                    Console.WriteLine(eng.Show());
            else
                Console.WriteLine("Пустая очередь!");
        }

        public static void MenuQueue()
        {
            //меню удаления/добавления
            Console.WriteLine(@"

Меню управления Очередью:
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
1) Добавить Обьект в КОНЕЦ
2) Удаление первого элемента очереди
3) Очистить очередь
4) Печать очереди
5) Сортировка очереди по НЕубыванию (ключ=Мощность двигателя в л.с.)
6) Поиск элемента в очереди с заданной мощностью
7) Запросы к очереди
8) Клонирование очереди
9) Переход к операциям над словарем");
        }

        #endregion

        #region Dictionary<K,T>

        public static int DictCapacity = 0;

        public static Dictionary<int, Engine> InitDict()
        {
            // Создание очереди двигателей
            Dictionary<int, Engine> dict = new Dictionary<int, Engine>();
            for (int i = 1; i < 1001; i++)
            {
                switch ((new Random()).Next() % 4)
                {
                    case 0:
                        dict.Add(i, new Engine(1));
                        break;
                    case 1:
                        dict.Add(i, new IternCombEngine(1));
                        break;
                    case 2:
                        dict.Add(i, new DieselEngine(1));
                        break;
                    case 3:
                        dict.Add(i, new TurboJetEngine(1));
                        break;
                }
            }

            DictCapacity += 1000;
            return dict;
        }

        public static void PrintDict(Dictionary<int, Engine> dict)
        {
            // печать словаря <int, Engine>
            Console.WriteLine("Печать очереди:\n");
            if (dict.Count > 0) //проверка НЕ пустоты
                foreach (int key in dict.Keys)
                    Console.WriteLine(key + " |" + dict[key].Show());
            else
                Console.WriteLine("Пустой словарь!");
        }

        public static void MenuDict()
        {
            //меню удаления/добавления
            Console.WriteLine(@"

Меню управления Словарём:
------------------------------------------------------------------------------------------------------------------------------------------------------------------------
1) Добавить Пару <Ключ, Значение>
2) Удаление Пары <Ключ, Значение>
3) Очистить словарь
4) Печать Пар <Ключ, Значение> словаря
6) Поиск элемента
7) Запросы к словарю
8) Клонирование словаря
9) Переход  к операциям над классом TestCollections");

        }

        public static Dictionary<int, Engine> CloneDict(Dictionary<int, Engine> dict)
        {
            //Клонирование словаря
            Dictionary<int, Engine> clone = new Dictionary<int, Engine>();
            foreach (int key in dict.Keys)
                clone[key] = dict[key];

            return clone;
        }

        public static void AddPair(Dictionary<int, Engine> dict)
        {
            //чтение ключа и удаление элемента

            Console.WriteLine("\nДобавление элемента:\n1) Случайно\n2) Ввести ключ");
            int choise = InputData.InputValidIntNumber(1, 2, "");
            if (choise == 1)
            {
                dict.Add(++DictCapacity, new Engine());
                Console.WriteLine($"Пара <Ключ, Значение> удалена из словаря! Размер словаря: {dict.Count}");
            }
            else
            {
                int key = InputData.InputIntNumber("Введите ключ элемента для его добавления:",
                    "Число введено не верно!");
                if (dict.Keys.Contains(key))
                    Console.WriteLine($"Добавление элемента с ключом '{key}' не возможно!");
                else
                {
                    dict.Add(key, new Engine(1));
                    Console.WriteLine($"Элемент с ключом '{key}' добавлен в словарь.");
                    DictCapacity++;
                }
            }
        }

        public static void RemovePair(Dictionary<int, Engine> dict)
        {
            //чтение ключа и удаление элемента
            if (dict.Count == 0)
                Console.WriteLine("Пустой словарь!");
            else
            {
                Console.WriteLine("\nВыбор ключа для удаления:\n1) Последний элемент\n2) Ввести ключ");
                int choise = InputData.InputValidIntNumber(1, 2, "");
                if (choise == 1)
                {
                    dict.Remove(dict.Count);
                    Console.WriteLine($"Пара <Ключ, Значение> удалена из словаря! Размер словаря: {dict.Count}");
                }
                else
                {
                    int key = InputData.InputIntNumber("Введите ключ элемента для его удаления:",
                        "Число введено не верно!");
                    if (dict.Keys.Contains(key))
                    {
                        dict.Remove(key);
                        Console.WriteLine($"Элемент с ключом '{key}' удален!");
                    }
                    else
                        Console.WriteLine($"Элемента с ключом '{key}' в словаре не существует!");
                }
            }
        }

        public static void SearchPair(Dictionary<int, Engine> dict)
        {
            //ввод ключа и поиск элемента с заданным ключом в массиве
            Console.WriteLine("\nВведите ключ элемента:");
            int key = InputData.InputIntNumber("Введите ключ элемента для его удаления:",
                "Число введено не верно!");
            if (dict.Keys.Contains(key))
                Console.WriteLine($"Элемент с ключом {key} найден в словаре:\n{key} |{dict[key].Show()}");
            else
                Console.WriteLine($"Элемента с ключом {key} в словаре не существует!");
        }

        public static void AskDict(Dictionary<int, Engine> dict)
        {
            // запросы в очередь
            static void GetNStock(Dictionary<int, Engine> dict)
            {
                // количество двигат в стоке
                int amountStock = 0;
                IternCombEngine temp;
                foreach (int eng in dict.Keys)
                    if (dict[eng] is IternCombEngine)
                    {
                        temp = dict[eng] as IternCombEngine;
                        if (temp.IStock)
                            amountStock++;
                    }
                Console.WriteLine($"\nКоличество Двигателей Внутреннего сгорания в Стоковом сотоянии: {amountStock}");
            }

            static void GetNDiesel(Dictionary<int, Engine> dict)
            {
                // количество дизелей
                int amountDiesel = 0;
                foreach (int eng in dict.Keys)
                    if (dict[eng] is DieselEngine)
                        amountDiesel++;
                Console.WriteLine($"\nКоличество Дизельных Двигателей : {amountDiesel}");
            }

            static void GetAverPowerICombEngs(Dictionary<int, Engine> dict)
            {
                // средняя мощность двигателей внутр сгорания
                int allPower = 0, amountItCombEng = 0;
                foreach (int eng in dict.Keys)
                    if (dict[eng] is IternCombEngine)
                    {
                        allPower = allPower + (dict[eng] as Engine).Power;
                        amountItCombEng++;
                    }
                Console.WriteLine($"\nСредняя мощность Двигателей Внутреннего сгорания : {(double)allPower/amountItCombEng} Л.С.");
            }
            
            GetNStock(dict);
            GetNDiesel(dict);
            GetAverPowerICombEngs(dict);
        }
        
        #endregion



        public static void Main(string[] args)
        {

            #region Part1. Queue

            Queue engQueue = InitQueue();
            //PrintQueue(engQueue);

            int choise = 0;
            while (choise != 9)
            {
                MenuQueue();
                choise = InputData.InputValidIntNumber(1, 9, "");
                switch (choise)
                {
                    case 1: // добавление элемен а в очередь
                        engQueue.Enqueue(new Engine(1));
                        Console.WriteLine($"Обьект добавлен в конец очереди! Размер Очереди: {engQueue.Count}");
                        break;
                    case 2: // удаление первого элемента
                        Console.WriteLine((engQueue.Count > 0)
                            ? "Удаленный первый элемент очереди:\n" + ((Engine) engQueue.Dequeue()).Show()
                            : "Пустая очередь!");
                        break;
                    case 3: // очистка очереди
                        engQueue.Clear();
                        Console.WriteLine($"Очередь очищена! Размер Очереди: {engQueue.Count}");
                        break;
                    case 4: // печaть очереди
                        PrintQueue(engQueue);
                        break;
                    case 5: // Сортировка очереди
                        engQueue = SortQueue(engQueue);
                        break;
                    case 6: // Поиск элемента
                        SearchEng(engQueue);
                        break;
                    case 7: // Запросы к очереди
                        AskQueue(engQueue);
                        break;
                    case 8: // копирование и клонирование
                        // пов копирование очереди
                        Queue copyEngs = (Queue) engQueue.Clone();
                        //клонирование
                        Queue temp = new Queue(engQueue);
                        Queue clonQueue = new Queue();
                        for (int i = 0; i < engQueue.Count; i++)
                        {
                            clonQueue.Enqueue(((Engine)temp.Peek()).Clone());
                            temp.Dequeue();
                        }

                        Console.WriteLine($"Оригинальная очередь : {engQueue.Count}");
                        Console.WriteLine($"Копия очереди : {copyEngs.Count}");
                        Console.WriteLine($"Клон очереди : {clonQueue.Count}");
                        break;
                    
                }
            }

            #endregion

            #region Part2. Dictionary<K,T>

            Console.WriteLine("\nЧасть 2. Dictionary<int, Engine>:\n");
            Dictionary<int, Engine> engDict = InitDict();
            //Dictionary<Engine, IternCombEngine> engDict = InitDict();
            //PrintDict(engDict);

            choise = 0;
            while (choise != 9)
            {
                MenuDict();
                choise = InputData.InputValidIntNumber(1, 9, "");
                switch (choise)
                {
                    case 1: // добавление Пары в словарь
                        AddPair(engDict);
                        break;
                    case 2: // удаление Пары из словаря
                        RemovePair(engDict);
                        break;
                    case 3: // очистка словаря
                        engDict.Clear();
                        Console.WriteLine($"Cловарь очищен! Размер словаря: {engDict.Count}");
                        break;
                    case 4: // печaть словаря
                        PrintDict(engDict);
                        break;
                    case 5: // Сортировка словаря
                        //SortDict(engDict);
                        break;
                    case 6: // Поиск элемента
                        SearchPair(engDict);
                        break;
                    case 7: // Запросы к словарю
                        AskDict(engDict);
                        break;
                    case 8: // Клонирование словаря
                        Dictionary<int, Engine> clonEngDict = CloneDict(engDict);
                        break;
                }
            }

            #endregion

            #region Part 3. Stack<T> Dictionary<K,T>

            TestCollections col = new TestCollections();
            col.SearchEngs();
            Console.WriteLine($"\nРазмер коллекции : {col.Count}");
            col.AddElem();
            col.RemovElem();
            #endregion
        }
    }
}
