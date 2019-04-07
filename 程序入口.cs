using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Private.BlurClear
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WindowCompositonAttributeData data = WindowCompositonAttributeData.透明;
            if (args != null && args.Length > 0)
            {
                if ("透明".Equals(args[0]))
                {
                    data = WindowCompositonAttributeData.透明;
                }
                else if ("模糊".Equals(args[0]))
                {
                    data = WindowCompositonAttributeData.模糊;
                }
            }

            Program.CloseOldProcess();
            
            //Explorer.exe重启后，系统窗口句柄会发生改变，所以不定义全局变量，每次都重新去获取句柄
            do
            {
                List<IntPtr> handles = new List<IntPtr>();
                //获取句柄
                //任务栏
                IntPtr taskbar = WindowsAPI.FindWindow("Shell_TrayWnd", "");
                if ((int)taskbar > 0)
                {
                    handles.Add(taskbar);

                    //可能有多个屏幕，即存在多个任务栏
                    IntPtr taskbar_2 = new IntPtr(0);
                    do
                    {
                        taskbar_2 = WindowsAPI.FindWindowEx(new IntPtr(0), taskbar_2, "Shell_SecondaryTrayWnd", "");
                        if ((int)taskbar_2 > 0)
                        {
                            handles.Add(taskbar_2);
                        }
                    } while ((int)taskbar_2 > 0);
                }

                //开始菜单
                IntPtr startMenu = WindowsAPI.FindWindow("Windows.UI.Core.CoreWindow", "启动");
                if ((int)startMenu > 0)
                {
                    handles.Add(startMenu);
                }

                //通知图标
                IntPtr notifyIcon = WindowsAPI.FindWindow("NotifyIconOverflowWindow", "");
                if ((int)notifyIcon > 0)
                {
                    handles.Add(notifyIcon);
                }

                //操作中心
                IntPtr operationCenter = WindowsAPI.FindWindow("Windows.UI.Core.CoreWindow", "操作中心");
                if ((int)operationCenter > 0)
                {
                    handles.Add(operationCenter);
                }

                //网络连接
                IntPtr net = WindowsAPI.FindWindow("Windows.UI.Core.CoreWindow", "网络连接");
                if ((int)net > 0)
                {
                    handles.Add(net);
                }
                //音量控制
                IntPtr volumeControl = WindowsAPI.FindWindow("Windows.UI.Core.CoreWindow", "音量控制");
                if ((int)volumeControl > 0)
                {
                    handles.Add(volumeControl);
                }

                //日期和时间信息
                IntPtr dateInfo = WindowsAPI.FindWindow("Windows.UI.Core.CoreWindow", "日期和时间信息");
                if ((int)dateInfo > 0)
                {
                    handles.Add(dateInfo);
                }

                Program.SetWCA(handles.ToArray(), data);

            } while (Program.Wait(20));

        }

        /// <summary>
        /// 关闭之前存在的相同的 Pvate.BlurClear 进程
        /// </summary>
        private static void CloseOldProcess()
        {
            Process current = Process.GetCurrentProcess();
            //根据当前进程名称获取系统中所有和当前进程名称相同的进程
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            
            if (processes.Length > 0)
            {
                foreach (var process in processes)
                {
                    //关闭除当前进程的其他进程
                    if (process.Id != current.Id)
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch (Exception)
                        {
                        }
                        process.Close();
                    }
                }
            }
        }
        
        /// <summary>
        /// 设置 WindowCompositonAttribute 以达到 透明 模糊 的效果
        /// </summary>
        /// <param name="handles">需要设置WCA的句柄</param>
        /// <param name="data">WCA数据</param>
        private static void SetWCA(IntPtr[] handles,WindowCompositonAttributeData data)
        {
            foreach (IntPtr handle in handles)
            {
                bool success=WindowsAPI.SetWindowCompositionAttribute(handle, ref data);
            }
        }

        /// <summary>
        /// 等待指定时间并返回true
        /// </summary>
        /// <param name="waitTime">等待的时间 单位：ms</param>
        /// <returns></returns>
        private static bool Wait(int waitTime)
        {
            System.Threading.Thread.Sleep(waitTime);
            return true;
        }
    }
}
