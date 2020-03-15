using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace dotnet_g36.Tests.Models
{
    public class TestCase
    {
        public static readonly List<Object[]> DataTest = new List<object[]>
        {
            new object[]{ new DateTime(2020, 3, 27, 11, 30, 00), 60 },
            new object[]{ new DateTime(2020, 3, 27, 12, 25, 00), 5 },
            new object[]{new DateTime(2020, 3, 27, 11, 40, 00), 50 }

        };
        public static IEnumerable<Object[]> DatumIndex
        {
            get
            {
                List<object[]> temp = new List<object[]>();
                for (int i = 0; i < DataTest.Count; i++)
                    temp.Add(new object[] { i });
                return temp;
            }
        }
    }
        //    public static List<DateTime[]> DataTest = new List<DateTime[]>
        //    {
        //      new DateTime[]
        //        { new DateTime(2020, 3, 15, 11, 30, 00),
        //         /* new DateTime(2020, 3, 15, 12, 25, 00),
        //          new DateTime(2020, 3, 2, 11, 40, 00)*/ }

        //    };
        //    public static readonly List<int[]> intTest = new List<int[]>
        //    {
        //        new int[] { 60, /*5, 50 */}

        //    };
        //    public static IEnumerable<Object[]> DatumIndex
        //    { get{

        //            int lengteDataTest = DataTest.Count;

        //            Object[][] temp = new object[lengteDataTest][];

        //            for (int i = 0; i <= temp.Length; i++)
        //                for (int j = 0; i <= temp[i].Length; j++)
        //                    temp[i][j] = temp[DataTest[j]][intTest[j]];
        //            return temp;

        //    }
        //}
    }
