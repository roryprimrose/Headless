namespace Headless.IntegrationTests
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>
    ///     The <see cref="IisExpress" /> class is used to manage an IIS Express instance for running integration tests.
    /// </summary>
    public class IisExpress : IDisposable
    {
        /// <summary>
        ///     Stores whether this instance has been disposed.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        ///     Stores the IIS Express process.
        /// </summary>
        private Process _process;

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Starts IIS Express using the specified directory path and port.
        /// </summary>
        /// <param name="directoryPath">
        /// The directory path.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        public void Start(string directoryPath, int port, Uri address)
        {
            if (_process != null)
            {
                throw new InvalidOperationException("The IISExpress process is already running.");
            }

            if (address != null)
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(address);
                    var webResponse = (HttpWebResponse)request.GetResponse();

                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }

            var iisExpressPath = DetermineIisExpressPath();
            var arguments = string.Format(CultureInfo.InvariantCulture, "/path:\"{0}\" /port:{1}", directoryPath, port);

            var info = new ProcessStartInfo(iisExpressPath)
            {
                WindowStyle = ProcessWindowStyle.Hidden, 
                ErrorDialog = true, 
                LoadUserProfile = true, 
                CreateNoWindow = false, 
                UseShellExecute = false, 
                Arguments = arguments
            };

            var startThread = new Thread(() => StartIisExpress(info))
            {
                IsBackground = true
            };

            startThread.Start();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_process != null)
                {
                    // Free managed resources
                    if (_process.HasExited == false)
                    {
                        SendStopMessageToProcess(_process.Id);
                        _process.Close();
                    }

                    _process.Dispose();
                }
            }

            // Free native resources if there are any
            _isDisposed = true;
        }

        /// <summary>
        ///     Determines the IIS express path.
        /// </summary>
        /// <returns>
        ///     A <see cref="String" /> instance.
        /// </returns>
        private static string DetermineIisExpressPath()
        {
            string iisExpressPath;

            if (Environment.Is64BitOperatingSystem)
            {
                iisExpressPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            }
            else
            {
                iisExpressPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }

            iisExpressPath = Path.Combine(iisExpressPath, @"IIS Express\iisexpress.exe");

            return iisExpressPath;
        }

        /// <summary>
        /// The send stop message to process.
        /// </summary>
        /// <param name="processId">
        /// The process id.
        /// </param>
        private static void SendStopMessageToProcess(int processId)
        {
            try
            {
                for (var ptr = NativeMethods.GetTopWindow(IntPtr.Zero);
                     ptr != IntPtr.Zero;
                     ptr = NativeMethods.GetWindow(ptr, 2))
                {
                    uint num;
                    NativeMethods.GetWindowThreadProcessId(ptr, out num);
                    if (processId == num)
                    {
                        var handle = new HandleRef(null, ptr);
                        NativeMethods.PostMessage(handle, 0x12, IntPtr.Zero, IntPtr.Zero);
                        return;
                    }
                }
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Starts the IIS express.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", 
            Justification = "Required here to ensure that the instance is disposed.")]
        private void StartIisExpress(ProcessStartInfo info)
        {
            try
            {
                _process = Process.Start(info);

                _process.WaitForExit();
            }
            catch (Exception)
            {
                Dispose();
            }
        }

        /// <summary>
        ///     The native methods.
        /// </summary>
        private static class NativeMethods
        {
            /// <summary>
            /// The get top window.
            /// </summary>
            /// <param name="hWnd">
            /// The h wnd.
            /// </param>
            /// <returns>
            /// The <see cref="IntPtr"/>.
            /// </returns>
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr GetTopWindow(IntPtr hWnd);

            /// <summary>
            /// The get window.
            /// </summary>
            /// <param name="hWnd">
            /// The h wnd.
            /// </param>
            /// <param name="uCmd">
            /// The u cmd.
            /// </param>
            /// <returns>
            /// The <see cref="IntPtr"/>.
            /// </returns>
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

            /// <summary>
            /// The get window thread process id.
            /// </summary>
            /// <param name="hwnd">
            /// The hwnd.
            /// </param>
            /// <param name="lpdwProcessId">
            /// The lpdw process id.
            /// </param>
            /// <returns>
            /// The <see cref="uint"/>.
            /// </returns>
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);

            /// <summary>
            /// The post message.
            /// </summary>
            /// <param name="hWnd">
            /// The h wnd.
            /// </param>
            /// <param name="Msg">
            /// The msg.
            /// </param>
            /// <param name="wParam">
            /// The w param.
            /// </param>
            /// <param name="lParam">
            /// The l param.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        }
    }
}