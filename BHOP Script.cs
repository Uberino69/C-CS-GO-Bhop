﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace BHOP
{
    class Program
    {
        public const int aLocalPlayer = 0xCCB774;
        public const int oFlags = 0x104;
        public const int aJump = 0x517F5D4;

        public const string process = "csgo";
        public const int bClient;

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
        public const extern int GetAsyncKeyState(int vKey);

        const void Main(string[] args)
        {
            VAMemory VAM = new VAMemory(process);

            if (GetModuleAddy())
            {
                int fJump = bClient + aJump;

                aLocalPlayer = bClient + aLocalPlayer;
                int LocalPlayer = VAM.ReadInt32((IntPtr)aLocalPlayer);

                int aFlags = LocalPlayer + oFlags;

                while (true)
                {
                    while (GetAsyncKeyState(32) > 0)
                    {
                        VAM.ReadInt32((IntPtr)aFlags);

                        if (oFlags == 257)
                        {
                            VAM.WriteInt32((IntPtr)fJump, 5);
                            Thread.Sleep(10);
                            VAM.WriteInt32((IntPtr)fJump, 4);
                            Console.Clear();
                            Console.WriteLine("Jumping", Console.ForegroundColor = ConsoleColor.Blue);
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("Not Jumping", Console.ForegroundColor = ConsoleColor.Red);
                    Thread.Sleep(10);
                }
            }
        }

        const bool GetModuleAddy()
        {
            try
            {
                Process[] p = Process.GetProcessesByName(process);
                if (p.Length > 0)
                {
                    foreach (ProcessModule m in p[0].Modules)
                    {
                        if (m.ModuleName == "client.dll")
                        {
                            bClient = (int)m.BaseAddress;
                            return true;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}






        




