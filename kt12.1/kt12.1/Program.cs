using System;
using System.Collections.Generic;

public interface IList<T>
{
    void Add(T item);
    void Remove(T item);
    T Get(int index);
    void Set(int index, T item);
    int Count { get; }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"{Name} ({Age} лет)";
    }
}

public class ArrayList<T> : IList<T>
{
    private T[] items;
    private int count;
    private const int DefaultCapacity = 4;

    public ArrayList()
    {
        items = new T[DefaultCapacity];
        count = 0;
    }

    public int Count => count;

    public void Add(T item)
    {
        if (count == items.Length)
        {
            Array.Resize(ref items, items.Length * 2);
        }
        items[count++] = item;
    }

    public void Remove(T item)
    {
        int index = -1;
        for (int i = 0; i < count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(items[i], item))
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }
            count--;
            items[count] = default(T);
        }
    }

    public T Get(int index)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException(nameof(index));
        return items[index];
    }

    public void Set(int index, T item)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException(nameof(index));
        items[index] = item;
    }
}

public class LinkedList<T> : IList<T>
{
    private class Node
    {
        public T Data { get; set; }
        public Node Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    private Node head;
    private int count;

    public int Count => count;

    public void Add(T item)
    {
        Node newNode = new Node(item);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }

    public void Remove(T item)
    {
        if (head == null) return;

        if (EqualityComparer<T>.Default.Equals(head.Data, item))
        {
            head = head.Next;
            count--;
            return;
        }

        Node current = head;
        while (current.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Next.Data, item))
            {
                current.Next = current.Next.Next;
                count--;
                return;
            }
            current = current.Next;
        }
    }

    public T Get(int index)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Node current = head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }
        return current.Data;
    }

    public void Set(int index, T item)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Node current = head;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }
        current.Data = item;
    }
}

class Program1
{
    static void Main()
    {
        Console.WriteLine("=== Задание 1: IList<T> ===");

        IList<int> arrayList = new ArrayList<int>();
        arrayList.Add(10);
        arrayList.Add(20);
        arrayList.Add(30);
        Console.WriteLine($"ArrayList Count: {arrayList.Count}");
        Console.WriteLine($"ArrayList[1]: {arrayList.Get(1)}");

        IList<string> linkedList = new LinkedList<string>();
        linkedList.Add("Hello");
        linkedList.Add("World");
        linkedList.Add("!");
        Console.WriteLine($"LinkedList Count: {linkedList.Count}");
        Console.WriteLine($"LinkedList[0]: {linkedList.Get(0)}");

        IList<Person> personList = new ArrayList<Person>();
        personList.Add(new Person("Иван", 25));
        personList.Add(new Person("Мария", 30));
        Console.WriteLine($"Person at index 0: {personList.Get(0)}");

        personList.Remove(personList.Get(0));
        Console.WriteLine($"After removal - Count: {personList.Count}");
    }
}