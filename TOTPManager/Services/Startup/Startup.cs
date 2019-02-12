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
            try
            {
                var rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                rk?.SetValue(_appTitle, _appPath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RemoveFromStartup()
        {
            try
            {
                var rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (_appPath == null)
                {
                    rk?.DeleteValue(_appTitle);
                }
                else
                {
                    if (rk?.GetValue(_appTitle).ToString().ToLower() == _appPath.ToLower())
                    {
                        rk?.DeleteValue(_appTitle);
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
            try
            {
                var rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                var value = rk.GetValue(_appTitle).ToString();
                return value.ToLower().Equals(_appPath.ToLower());
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}
