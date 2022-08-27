using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanVinhPhuc_Alephon_Assignment
{
   class Program
   {
      // code fast speed ,clear and do bonus question
      static void Main(string[] args)
      {
         // sample test 
         string fileTest_1000 = "fileTest_1000.txt";
         string fileTest_10000 = "fileTest_10000.txt";
         string fileTest_100000 = "fileTest_100000.txt";
         string fileResult = "fileResult.txt";

         GenerateFile(fileTest_1000, 1000, -100, 1000);
         GenerateFile(fileTest_10000, 100000, -100, 1000);
         GenerateFile(fileTest_100000, 10000, -100, 1000);

         // sample test sortFile
         SortFile(fileTest_1000);
         SortFile(fileTest_10000);
         SortFile(fileTest_100000);

         // sample test mergeFile 
         MergeFile(fileTest_1000,fileTest_10000,fileResult);
         ShowFile(fileResult);

         // sample test showFile
         /*
         ShowFile(fileTest_1000);
         ShowFile(fileTest_10000);
         ShowFile(fileTest_100000);
         */

         // GET DIRECTORY STORE FILE
         Console.Write("File stored on : ");
         Console.WriteLine(Environment.CurrentDirectory);
      }


      // 1.1. GenerateFile
      // Random double number and save to filename path
      public static bool GenerateFile(string filename, int size, double max, double min)
      {
         try
         {
            // create file 
            StreamWriter sw = File.CreateText(filename);
            sw.Close();

            // write random double number to the file
            sw = new StreamWriter(filename);
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
               double a = random.NextDouble() * (max - min) + min;
               a = Math.Round(a, 2);
               sw.WriteLine(a + " ");
            }
            sw.Close();
            // ----- end of write to file
            return true;
         }
         catch (Exception ex)
         {
            return false;
         }
      }

      // 1.2. SortFile 
      // Read file and store to Array[double], use HeapSort to sort Array[] 
      // and save Array to file 
      public static bool SortFile(string filename)
      {
         List<double> temp = new List<double>();
         Double[] listDouble ;
         String line;

         try
         {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(filename);
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
               temp.Add(Convert.ToDouble(line));

               //Read the next line
               line = sr.ReadLine();
            }
            //close the file
            sr.Close();

            listDouble = temp.ToArray();
            HeapSort(listDouble);
            SaveToFile(filename, listDouble);

            return true;
         }
         catch (Exception e)
         {
            Console.Write("Exception: " + e.Message);
            return false;
         }
      }

      // 1.3. ShowFile 
      // This func will read a file and show on Console 
      public static bool ShowFile(string filename)
      {
         Console.WriteLine("\t\t\t\t\t ------ START READ FILE " + filename + " !!! -------");
         String line;
         try
         {
            StreamReader sr = new StreamReader(filename);
            line = sr.ReadLine();

            //read until you reach end of file
            while (line != null)
            {
               Console.Write(Convert.ToDouble(line) + 1 + " ");
               //Read the next line
               line = sr.ReadLine();
            }
            sr.Close();

            return true;
         }
         catch (Exception e)
         {
            Console.Write("Exception: " + e.Message);
            return false;
         }
         finally
         {
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t ------ READ FILE " + filename + " DONE !!! ------");
         }
      }

      // 1.4 MergeFile 
      public static bool MergeFile(string fileA, string fileB, string resultFile)
      {
         double[] ArrDouble = { 0 };
         List<double> listDouble = new List<double>();

         listDouble.AddRange(ReadFileToList(fileA));
         listDouble.AddRange(ReadFileToList(fileB));

         ArrDouble = listDouble.ToArray();
         HeapSort(ArrDouble);

         if (SaveToFile(resultFile, ArrDouble))
         {
            Console.WriteLine("Merge File Done");
         }
         return true;
      }

      // Save a reload file with content is Array[Double]
      public static bool SaveToFile(string filename, Double[] ArrDouble)
      {
         try
         {
            // create file 
            StreamWriter sw = File.CreateText(filename);
            sw.Close();

            // write random double number to the file
            sw = new StreamWriter(filename);

            for (int i = 0; i < ArrDouble.Length; i++)
            {

               double d_item = ArrDouble[i];
               sw.WriteLine(d_item + " ");
            }
            sw.Close();
            // ----- end of write to file
            return true;
         }
         catch (Exception ex)
         {
            return false;
         }
      }

      // Load file and store to list<Double>
      public static List<double> ReadFileToList(string filename)
      {
         List<double> listDouble = new List<double>();
         String line;
         try
         {
            StreamReader sr = new StreamReader(filename);
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
               listDouble.Add(Convert.ToDouble(line));

               //Read the next line
               line = sr.ReadLine();
            }
            sr.Close();

         }
         catch (Exception ex)
         {
            Console.Write(ex);
         }
         return listDouble;
      }

      public static void HeapSort(double[] Array)
      {
         int n = Array.Length;
         double temp;

         for (int i = n / 2; i >= 0; i--)
         {
            Heapify(Array, n - 1, i);
         }

         for (int i = n - 1; i >= 0; i--)
         {
            //swap last element of the max-heap with the first element
            temp = Array[i];
            Array[i] = Array[0];
            Array[0] = temp;

            //exclude the last element from the heap and rebuild the heap 
            Heapify(Array, i - 1, 0);
         }
      }

      // Heapify function is used to build the max heap
      // max heap has maximum element at the root which means
      // first element of the array will be maximum in max heap
      public static void Heapify(double[] Array, int n, int i)
      {
         int max = i;
         int left = 2 * i + 1;
         int right = 2 * i + 2;

         //if the left element is greater than root
         if (left <= n && Array[left] > Array[max])
         {
            max = left;
         }

         //if the right element is greater than root
         if (right <= n && Array[right] > Array[max])
         {
            max = right;
         }

         //if the max is not i
         if (max != i)
         {
            double temp = Array[i];
            Array[i] = Array[max];
            Array[max] = temp;
            //Recursively Heapify the affected sub-tree
            Heapify(Array, n, max);
         }
      }
   }
}
