using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowsKey
{
    /*    public static void Disable()
        {
            RegistryKey key = null;
            try
            {
                key = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Keyboard Layout", true);
                byte[] binary = new byte[] {
                        0x00,
                        0x00,
                        0x00,
                        0x00,
                        0x00,
                        0x00,
                        0x00,
                        0x00,
                        0x03,
                        0x00,
                        0x00,
                        0x00,
                        0x00,
                        0x00,
                        0x5B,
                        0xE0,
                        0x00,
                        0x00,
                        0x5C,
                        0xE0,
                        0x00,
                        0x00,
                        0x00,
                        0x00
                    };
                key.SetValue("Scancode Map", binary, RegistryValueKind.Binary);
            }
            catch (System.Exception ex)
            {
                Debug.Assert(false, ex.ToString());
            }
            finally
            {
                key.Close();
            }
        }

        public static void Enable()
        {
            RegistryKey key = null;
            try
            {
                key = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Keyboard Layout", true);
                key.DeleteValue("Scancode Map", true);
            }
            catch (System.Exception ex)
            {
                Debug.Assert(false, ex.ToString());
            }
            finally
            {
                key.Close();
            }
        }*/

/*    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SetWindowsHookEx(int idHook, HookHandlerDelegate lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern short GetKeyState(int keyCode);*/
}
