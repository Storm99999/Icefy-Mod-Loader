using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Icefy_Mod_Loader.lib
{
    internal class Injector
    {
        static readonly IntPtr INTPTR_ZERO = (IntPtr)0;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        public static int Inject(string PN, string DLLP)
        {
            if (!File.Exists(DLLP)) 
            { 
                return 1; 
            }

            uint _procId = 0;
            Process[] _procs = Process.GetProcesses();
            for (int i = 0; i < _procs.Length; i++)
            {
                if (_procs[i].ProcessName == PN)
                {
                    _procId = (uint)_procs[i].Id;
                }
            }

            if (_procId == 0) { return 2; }

            if (!SI(_procId, DLLP))
            {
                return 3;
            }

            return 4;
        }

        public static bool SI(uint P, string DDLP)
        {
            IntPtr hndProc = OpenProcess((0x2 | 0x8 | 0x10 | 0x20 | 0x400), 1, P);

            if (hndProc == INTPTR_ZERO) { return false; }


            IntPtr lpAddress = VirtualAllocEx(hndProc, (IntPtr)null, (IntPtr)DDLP.Length, (0x1000 | 0x2000), 0x40);

            if (lpAddress == INTPTR_ZERO)
            {
                return false;
            }

            byte[] bytes = Encoding.ASCII.GetBytes(DDLP);

            if (WriteProcessMemory(hndProc, lpAddress, bytes, (uint)bytes.Length, 0) == 0)
            {
                return false;
            }

            CloseHandle(hndProc);

            return true;

        }
    }
}
