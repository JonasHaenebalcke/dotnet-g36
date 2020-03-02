using System;
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
}
