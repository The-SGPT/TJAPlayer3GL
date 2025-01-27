﻿using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using TJAPlayer3.ErrorReporting;

namespace TJAPlayer3
{
	internal class Program
	{
		#region [ 二重起動チェック、DLL存在チェック ]
		//-----------------------------
		private static Mutex mutex二重起動防止用;

		#region [DllImport]
		[DllImport( "kernel32", CharSet = CharSet.Unicode, SetLastError = true )]
		internal static extern void FreeLibrary( IntPtr hModule );

		[DllImport( "kernel32", CharSet = CharSet.Unicode, SetLastError = true )]
		internal static extern IntPtr LoadLibrary( string lpFileName );

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern bool SetDllDirectory(string lpPathName);
		#endregion
		//-----------------------------
		#endregion

		[STAThread]
        private static void Main()
        {
			if (IntPtr.Size == 8)
			{
				// 64bitっぽい
				SetDllDirectory("lib/x64");
			}
			else if (IntPtr.Size == 4)
			{
				// 32bitっぽい
				SetDllDirectory("lib/x86");
			}
			else
			{
				throw new PlatformNotSupportedException();
			}

			ErrorReporter.WithErrorReporting(MainImpl);
        }

        private static void MainImpl()
		{
            //UpdateChecker.CheckForAndOfferUpdate();

			mutex二重起動防止用 = new Mutex( false, "DTXManiaMutex" );

			if ( mutex二重起動防止用.WaitOne( 0, false ) )
			{
				string newLine = Environment.NewLine;
				bool bDLLnotfound = false;

				Trace.WriteLine( "Current Directory: " + Environment.CurrentDirectory );
				Trace.WriteLine( "EXEのあるフォルダ: " + Path.GetDirectoryName( Application.ExecutablePath ) );

                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

				if ( !bDLLnotfound )
				{
#if DEBUG && TEST_ENGLISH
					Thread.CurrentThread.CurrentCulture = new CultureInfo( "en-US" );
#endif

                    using (var mania = new TJAPlayer3())
                    {
                        mania.Run();
                    }

					Trace.WriteLine( "" );
					Trace.WriteLine( "遊んでくれてありがとう！" );

					if ( Trace.Listeners.Count > 1 )
						Trace.Listeners.RemoveAt( 1 );
				}

				// BEGIN #24615 2011.03.09 from: Mutex.WaitOne() が true を返した場合は、Mutex のリリースが必要である。

				mutex二重起動防止用.ReleaseMutex();
				mutex二重起動防止用 = null;

				// END #24615 2011.03.09 from
			}
		}
	}
}
