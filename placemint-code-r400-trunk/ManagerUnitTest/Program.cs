using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ManagerUnitTest
{
    using System.Globalization;
    using PlaceMint.Manager;

    class Program
    {
        [STAThread]
        static void Main()
        {
            Logger.Level = LoggingLevel.Fatal;
            Logger.Fatal("Fatal 1 Yes");
            Logger.Debug("Debug 1 NO");
            Logger.Trace("Trace 1 NO");
            Logger.Level = LoggingLevel.Debug;
            Logger.Fatal("Fatal 2 YES");
            Logger.Debug("Debug 2 YES");
            Logger.Trace("Trace 2 NO");
            Logger.Level = LoggingLevel.Trace;
            Logger.Fatal("Fatal 4 YES");
            Logger.Debug("Debug 3 YES");
            Logger.Trace("Trace 3 YES");

            const string INTERFACE_NAME = "ManagerUnitTest.ITest";

            Console.WriteLine("Start testing.");
            if (!executeMethodsOfInterface(INTERFACE_NAME))
            {
                Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "{0} is not defined, cannot run the tests. Press <enter>.",
                    INTERFACE_NAME));
                Console.ReadLine();
                return;
            }
            Console.WriteLine("All test succeded! Press <enter>.");
            Console.ReadLine();
        }

        static private bool executeMethodsOfInterface(string i)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            Type itest = asm.GetType(i);
            if (itest == null)
            {
                return false;
            }
            MethodInfo[] methods = itest.GetMethods();
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
            {
                if (t.IsClass && t.GetInterface(i) != null)
                {
                    Object obj1 = Activator.CreateInstance(t);
                    foreach (MethodInfo method in methods)
                    {
                        if (method.ReturnType == typeof(void))
                        {
                            t.InvokeMember(method.Name, BindingFlags.Default | BindingFlags.InvokeMethod, 
                                null, obj1, null, CultureInfo.InvariantCulture);
                        }
                    }
                }
            }
            return true;
        }
        //NativeMethods calls specific to the unit test, no need to include in regular NativeMethods class
        public static class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true, ThrowOnUnmappableChar=true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        }
    }
}
