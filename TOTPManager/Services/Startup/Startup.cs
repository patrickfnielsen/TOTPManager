using Microsoft.Win32;
using System;

namespace TOTPManager.Services.Startup
{
    public class Startup
    {
        private readonly string _appTitle;
        private readonly string _appPath;

        public Startup(string appTitle, string appPath)
        {
            _appTitle = appTitle;
            _appPath = appPath;
        }

        public bool RunOnStartup()
        {
            RegistryKey rk;
            
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                rk.SetValue(_appTitle, _appPath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RemoveFromStartup()
        {
            RegistryKey rk;

            try
            {
                rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (_appPath == null)
                {
                    rk.DeleteValue(_appTitle);
                }
                else
                {
                    if (rk.GetValue(_appTitle).ToString().ToLower() == _appPath.ToLower())
                    {
                        rk.DeleteValue(_appTitle);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public bool IsInStartup()
        {
            RegistryKey rk;
            string value;

            try
            {
                rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                value = rk.GetValue(_appTitle).ToString();
                if (value == null)
                {
                    return false;
                }
                else if (!value.ToLower().Equals(_appPath.ToLower()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}
