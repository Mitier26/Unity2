using BreakInfinity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public static class Methods
{
    //public static List<T> CreateList<T>(int capacity) => Enumerable.Repeat(default(T), capacity).ToList();

    public static int Notation;
    public static string  Notate(this BigDouble number)
    {
        switch(Notation)
        {
            case 0:
                return "Lol";
            case 1:
                return "scienceeeeee";
        }
        return "";
    }

    public static void UpgradeCheck<T>(List<T> list, int length) where T : new()
    {
        try
        {
            if (list.Count == 0) list = new T[length].ToList();
            while (list.Count <length) list.Add(new T());
        }
        catch
        {
            //list = CreateList<T>(length);
            list = new T[length].ToList();
        }
    }
}
