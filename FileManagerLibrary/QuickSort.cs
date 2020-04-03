﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FileManagerLibrary
{
    public static class QuickSort<T> where T : IComparable
    {
        // Quick sort algoritm
        public static void SortQuick(ref List<T> list, int left, int right)
        {
            // Start and end variabler är använda för att ha koll när det behövs göra swap
            int start = left;
            int end = right;
            
            // Definiera en pivot för att jämföra med start och end element. Här väljs pivot i mitten
            T pivot = list[(left + right) / 2];

            // While loop gäller när start och end inte korsar varandra. Om de korsar varandra, kommer funktionen anropa sig själv rekursivt 
            while (start <= end)
            {
                // Inkrementera start tills element på vänster sida är större än eller lika med pivot
                while (list[start].CompareTo(pivot)<0 ) 
                {
                    start++;

                }

                // Dekrement end tills element på höger sida är mindre än eller lika med pivot
                while (list[end].CompareTo(pivot)>0) 
                {
                    end--;
                }

                // Gör en swap av start och end och fortsätta inkrementa start och decrementa end
                if (start <= end) 
                {
                    Swap(list, start, end);
                    
                    start++;
                    end--;
                }
            }


            // Anropar rekursivt både på vänster sida och höger sida
            if (left < end)
                SortQuick(ref list, left, end);
            if (start < right)
                SortQuick(ref list, start, right);
        }
        
        // Swap funktion
        private static void Swap(List<T>list, int a, int b)
        {
            T t = list[a];
            list[a] = list[b];
            list[b] = t;
        }
    }

}