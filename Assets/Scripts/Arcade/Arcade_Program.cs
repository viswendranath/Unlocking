using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace arcadePatternNumberGenerator
{
    public class Arcade_Program : MonoBehaviour
    {
        // The number array
        public static int[] finalArray;
        // Text box on the screen
        public Text num;
        // Final number in string format
        public static String final_num = "";
        // Helps us to escape from the infinite loop while generating the number
        public static int loopCount = 0;
        // Size of the number displayes
        public static int SIZE = 5;
        // Maximum number that can appear in the number
        public static int MAX = 9;
        // Variables for setting variable question lenghts
        public static int minsize;
        public static int maxsize;

        private void Awake()
        {
            num.text = "";
            num = GetComponent<Text>();
            System.Random t = new System.Random();
            // Setting variable question lengths
            SIZE = t.Next(minsize, maxsize);
            Compute();
            num.text = final_num;
        }
        public static void Compute()
        {
            // Number of possibilites for each number
            int[] lengths = new int[9] { 5, 7, 5, 7, 8, 7, 5, 7, 5 };
            int[,] n = new int[9, 8] {
                { 2, 4, 5, 6, 8, 0, 0, 0 },
                { 1, 3, 4, 5, 6, 7, 9, 0 },
                { 2, 4, 5, 6, 8, 0, 0, 0 },
                { 1, 2, 3, 5, 7, 8, 9, 0 },
                { 1, 2, 3, 4, 6, 7, 8, 9 },
                { 1, 2, 3, 5, 7, 8, 9, 0 },
                { 2, 4, 5, 6, 8, 0, 0, 0 },
                { 1, 3, 4, 5, 6, 7, 9, 0 },
                { 2, 4, 5, 6, 8, 0, 0, 0 }
            };
            System.Random r = new System.Random();
            finalArray = new int[SIZE];
            int index = 0;
            // Generating the first number
            finalArray[index] = r.Next(MAX) + 1;
            bool valid;
            int a;
            while (index < SIZE - 1)
            {

                loopCount = 0;
                valid = false;
                do
                {
                    a = n[finalArray[index] - 1, r.Next(lengths[finalArray[index] - 1])];
                    valid = !present(finalArray, index, a);
                    ++loopCount;
                }
                // Assuming that we do get the number for maximum 50 tries
                while (!valid && loopCount < 50);
                if (valid)
                    finalArray[++index] = a;
                else
                {
                    Compute();
                }
                final_num = "";
                for (int i = 0; i < finalArray.Length; i++)
                {
                    final_num += finalArray[i].ToString() + " ";
                }
            }
        }
        public static bool present(int[] arr, int len, int num)
        {
            int i;
            for (i = 0; i < len; i++)
            {
                if (arr[i] == num)
                    return true;
            }
            return false;
        }
    }
}