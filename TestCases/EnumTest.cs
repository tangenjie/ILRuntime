﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LitJson;

namespace TestCases
{
    public class EnumTest
    {
        public enum TestEnum5
        {
            Enum1,
            Enum2,
            Enum4 = 0x12345789,
        }

        public enum TestEnum : long
        {
            Enum1,
            Enum2,
            Enum4 = 0x123456789,
        }

        enum TestEnum2 : ulong
        {
            Enum1,
            Enum2,
            Enum3234 = 0x123456789,
        }

        enum TestEnum3 : byte
        {
            Enum1bbb,
            Enum2bbb,
        }

        enum TestEnum4 : ushort
        {
            零 = 0,
            One = 1,
            佰 = 100
        }

        enum TestEnumUint : uint
        {
            Zero = 0,
            UOne = 1,
            Max = uint.MaxValue
        }

        enum TestEnumInt:int
        {
            Min  = int.MinValue,
            Max = int.MaxValue
        }

        enum TestEnumSByte : sbyte
        {
            Min = sbyte.MinValue,
            Max = sbyte.MaxValue
        }

        enum TestEnumFlag
        {
            Feature1 = 1,
            Feature2 = 2,
            Feature3 = 4,
            Feature4 = 8,
        }

        enum TestEnumEmpty
        {

        }

        static TestEnum b = TestEnum.Enum2;

        public static string Test01()
        {
            TestEnum a = TestEnum.Enum4;
            Console.WriteLine("a=" + a);

            return a.ToString();
        }

        public static string Test02()
        {
            TestEnum a = (TestEnum)1;
            Console.WriteLine("a=" + a);
            return a.ToString();
        }

        public static bool Test03()
        {
            switch (b)
            {
                case TestEnum.Enum1:
                    return false;
                case TestEnum.Enum2:
                    return true;
                default:
                    return false;
            }
        }

        public static string Test04()
        {
            return Test04Sub(TestEnum.Enum4);
        }

        static string Test04Sub(TestEnum a)
        {
            return a.ToString();
        }

        public static void Test05()
        {
            TestEnum a = TestEnum.Enum4;
            TestEnum2 b = (TestEnum2)a;
            Console.WriteLine("b=" + b);
            TestEnum3 c = (TestEnum3)EnumTest.b;
            Console.WriteLine("c=" + c);
        }

        public static string Test06()
        {
            System.IO.FileMode a = System.IO.FileMode.Create;

            Console.WriteLine("a=" + a);
            return a.ToString();
        }

        public static string Test07()
        {
            if (File.Exists("test.txt"))
            {
                File.Delete("test.txt");
            }

            using (System.IO.FileStream fs = new System.IO.FileStream("test.txt", System.IO.FileMode.Create))
            {
                fs.WriteByte(100);
            }

            using (System.IO.FileStream fs = new System.IO.FileStream("test.txt", System.IO.FileMode.Open))
            {
                return fs.ReadByte().ToString();
            }
        }

        public static void Test08()
        {
            object o = TestEnum.Enum4;

            Console.WriteLine((TestEnum)o == TestEnum.Enum4);
        }

        public static void Test09()
        {
            Dictionary<TestEnum, int> dic = new Dictionary<TestEnum, int>();
            dic[TestEnum.Enum2] = 123;

            int res;
            if (dic.TryGetValue(TestEnum.Enum2, out res))
            {
                Console.WriteLine(res);
            }
        }

        public static void Test10()
        {
            object e = TestEnum3.Enum2bbb;
            byte b = (byte)e; //InvalidCastException
            Console.WriteLine(b);
        }

        public static void Test11()
        {
            //Enum defined in ILRuntime
            var enumInIL = TestEnum.Enum4;
            var valueDirectly = $"{enumInIL}";
            var valueToString = enumInIL.ToString();

            if (valueDirectly.Equals(valueToString) == false)
            {
                throw new Exception($"Different string value: {valueDirectly} vs. {valueToString}");
            }

            //Enum defined in native code
            var enumInNative = JsonType.Int;
            valueDirectly = $"{enumInNative}";
            valueToString = enumInNative.ToString();

            var enumObj = (Object)enumInNative;
            Console.WriteLine(enumObj.GetType().FullName);

            if (valueDirectly.Equals(valueToString) == false)
            {
                throw new Exception($"Different string value: {valueDirectly} vs. {valueToString}");
            }
        }

        public static void Test12()
        {
            var arr = Enum.GetValues(typeof(TestEnum));
            foreach (var i in arr)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static void Test13()
        {
            var emmm = TestEnum2.Enum3234;
            Console.WriteLine(emmm.ToString());

            var one = TestEnum4.One;
            Console.WriteLine(one);

            var hundred = TestEnum4.佰;
            Console.WriteLine(hundred);
            Console.WriteLine(hundred.ToString());

            var UOne = TestEnumUint.Max;
            Console.WriteLine(UOne);

            Console.WriteLine(TestEnumSByte.Max);
        }

        public static void Test14()
        {
            var arr = Enum.GetNames(typeof(TestEnum));
            foreach (var i in arr)
            {
                Console.WriteLine(i);
            }
        }

        public static void Test16()
        {
            TestEnum[] eTests = (TestEnum[])System.Enum.GetValues(typeof(TestEnum));
            foreach (var item in eTests)
            {
                Console.WriteLine(item);
            }
        }

        public static void Test17()
        {
            /*Dictionary<TestEnum, int> dic = new Dictionary<TestEnum, int>();
            dic[TestEnum.Enum2] = 123;
            int res;
            if (dic.TryGetValue(TestEnum.Enum2, out res))
            {
                Console.WriteLine(res);
            }*/

            //下面这个用例跑不过
            Dictionary<string, TestEnum> dic2 = new Dictionary<string, TestEnum>();
            dic2["abc"] = TestEnum.Enum1;
            TestEnum e;
            if (dic2.TryGetValue("abc", out e))
            {
                Console.WriteLine(e);
            }
        }

        public static void Test18()
        {
            var a = TestEnum.Enum2;
            Test18Sub(0x123456789, out a);
            switch(a)
            {
                case TestEnum.Enum1:
                    Console.WriteLine(a + "1");
                    break;
                case TestEnum.Enum2:
                    Console.WriteLine(a + "1");
                    break;
                case TestEnum.Enum4:
                    Console.WriteLine(a + "2");
                    break;
            }
            Console.WriteLine(a);
        }

        static void Test18Sub(ulong val, out TestEnum e)
        {
            e = (TestEnum)val;
        }

        public static void Test19()
        {
            Console.WriteLine(Enum.GetValues(typeof(TestEnumEmpty)).Length);
        }

        public static void Test20()
        {
            TestEnumFlag flag = TestEnumFlag.Feature1 | TestEnumFlag.Feature3;
            var res = flag.HasFlag(TestEnumFlag.Feature3);
            Console.WriteLine(res);
            if (!res)
                throw new Exception();
            res = flag.HasFlag(TestEnumFlag.Feature2);
            Console.WriteLine(res);
            if (res)
                throw new Exception();
        }
        public static void Test21()
        {
            bool res = false;
            object obj = Enum.ToObject(typeof(TestEnum5), 0x12345789);
            Console.WriteLine(obj);
            TestEnum5 b = (TestEnum5)obj;
            switch (b)
            {
                case TestEnum5.Enum1:
                    res = false;
                    break;
                case TestEnum5.Enum2:
                    res = false;
                    break;
                case TestEnum5.Enum4:
                    res = true;
                    break;
                default:
                    res = false;
                    break;
            }

            if (!res)
                throw new Exception();
        }

        public static void Test22()
        {
            int res = 0;
            TestEnum5 b = TestEnum5.Enum2;
            res = b.CompareTo(TestEnum5.Enum4);
            if (res >= 0)
                throw new Exception();
        }

        public static void Test30()
        {
            var test = new TestClass1();
            for (int i = 0; i < 10; ++i)
                test.Test();
        }
        public static void Test31()
        {
            var test = new TestClass1();
            for (int i = 0; i < 10; ++i)
                test.Test1();
        }
        public static void Test32()
        {
            var test = new TestClass1();
            for (int i = 0; i < 10; ++i)
                test.Test2();
        }
        public static void Test33()
        {
            var test = new TestClass1();
            for (int i = 0; i < 10; ++i)
                test.Test3();
        }
        private class TestClass1
        {
            private TestClass2 TestValue = new TestClass2();

            public void Test()
            {
                for (int i = 0; i < 10; ++i)
                {
                    TestValue.Test1();
                    TestValue.Test2();
                    TestValue.Test3();
                }
            }
            public void Test1()
            {
                for (int i = 0; i < 10; ++i)
                    TestValue.Test1();
            }
            public void Test2()
            {
                for (int i = 0; i < 10; ++i)
                    TestValue.Test2();
            }
            public void Test3()
            {
                for (int i = 0; i < 10; ++i)
                    TestValue.Test3();
            }
        }

        private class TestClass2
        {
            private object _testValue = TestEnumFlag.Feature3;

            public void Test1()
            {
                if (_testValue is TestEnumFlag.Feature3)
                    return;
                throw new System.Exception();
            }
            public void Test2()
            {
                if (_testValue.Equals(TestEnumFlag.Feature3))
                    return;
                throw new System.Exception();
            }
            public void Test3()
            {
                if (object.Equals(_testValue, TestEnumFlag.Feature3))
                    return;
                throw new System.Exception();
            }
        }
        class SystemType
        {
            public int value = 10;
        }

        class TestConstructor
        {
            int[] ArrayTest = null;
            TestEnum[] ArrayTest2 = new TestEnum[9];
            TestEnum[] ArrayTest3 = null;
            SystemType ReferenceTest1 = null;
            public void ArrayLengthTest()
            {
                if (ArrayTest == null)
                    ArrayTest = new int[5];
                Console.WriteLine(string.Format("Int array type {0}", ArrayTest.GetType().Name));
                Console.WriteLine(string.Format("Int array length {0}", ArrayTest.Length));
                if (ArrayTest2 == null)
                    ArrayTest2 = new TestEnum[10];
                Console.WriteLine(string.Format("Enum array type {0}", ArrayTest2.GetType().Name));
                Console.WriteLine(string.Format("Enum array length {0}", ArrayTest2.Length));
                if (ReferenceTest1 == null)
                    ReferenceTest1 = new SystemType();
                Console.WriteLine(string.Format("SystemType type {0}", ReferenceTest1.GetType().Name));
                Console.WriteLine(string.Format("SystemType va {0}", ReferenceTest1.value));
                if (ArrayTest3 == null)
                    ArrayTest3 = new TestEnum[15];
                Console.WriteLine(string.Format("Enum array type {0}", ArrayTest3.GetType().Name));
                Console.WriteLine(string.Format("Enum array length {0}", ArrayTest3.Length));
            }
        }
        public static void Test15()
        {
            TestConstructor test = new TestConstructor();
            test.ArrayLengthTest();
        }
    }
}
