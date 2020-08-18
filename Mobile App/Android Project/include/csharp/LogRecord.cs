// DO NOT EDIT! This is an autogenerated file. All changes will be
// overwritten!

//	Copyright (c) 2016 Vidyo, Inc. All rights reserved.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;

namespace VidyoClient
{
	public class LogRecordFactory
	{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoLogRecordConstructDefaultNative();
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoLogRecordDestructNative(IntPtr obj);
		public static LogRecord Create()
		{
			IntPtr objPtr = VidyoLogRecordConstructDefaultNative();
			return new LogRecord(objPtr);
		}
		public static void Destroy(LogRecord obj)
		{
			VidyoLogRecordDestructNative(obj.GetObjectPtr());
		}
	}
	public class LogRecord{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoLogRecord reference.
		public IntPtr GetObjectPtr(){
			IntPtr nFile = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(file ?? string.Empty);
			IntPtr nFunctionName = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(functionName ?? string.Empty);
			IntPtr nMessage = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(message ?? string.Empty);
			IntPtr nName = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(name ?? string.Empty);
			IntPtr nThreadName = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(threadName ?? string.Empty);

			VidyoLogRecordSetcategoryNameNative(objPtr, categoryName);
			VidyoLogRecordSeteventTimeNative(objPtr, eventTime);
			VidyoLogRecordSetfileNative(objPtr, nFile);
			VidyoLogRecordSetfunctionNameNative(objPtr, nFunctionName);
			VidyoLogRecordSetlevelNative(objPtr, level);
			VidyoLogRecordSetlineNative(objPtr, line);
			VidyoLogRecordSetmessageNative(objPtr, nMessage);
			VidyoLogRecordSetnameNative(objPtr, nName);
			VidyoLogRecordSetthreadNameNative(objPtr, nThreadName);

			Marshal.FreeHGlobal(nThreadName);
			Marshal.FreeHGlobal(nName);
			Marshal.FreeHGlobal(nMessage);
			Marshal.FreeHGlobal(nFunctionName);
			Marshal.FreeHGlobal(nFile);
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoLogRecordGetcategoryNameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLogRecordSetcategoryNameNative(IntPtr obj, ulong categoryName);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoLogRecordGeteventTimeNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLogRecordSeteventTimeNative(IntPtr obj, ulong eventTime);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLogRecordGetfileNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLogRecordSetfileNative(IntPtr obj, IntPtr file);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLogRecordGetfunctionNameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLogRecordSetfunctionNameNative(IntPtr obj, IntPtr functionName);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern LogLevel VidyoLogRecordGetlevelNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern void VidyoLogRecordSetlevelNative(IntPtr obj, LogLevel level);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern int VidyoLogRecordGetlineNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLogRecordSetlineNative(IntPtr obj, int line);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLogRecordGetmessageNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLogRecordSetmessageNative(IntPtr obj, IntPtr message);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLogRecordGetnameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLogRecordSetnameNative(IntPtr obj, IntPtr name);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLogRecordGetthreadNameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLogRecordSetthreadNameNative(IntPtr obj, IntPtr threadName);

		public enum LogLevel{
			LoglevelFATAL,
			LoglevelERROR,
			LoglevelWARNING,
			LoglevelINFO,
			LoglevelDEBUG,
			LoglevelSENT,
			LoglevelRECEIVED,
			LoglevelENTER,
			LoglevelLEAVE,
			LoglevelINVALID
		}
		public ulong categoryName;
		public ulong eventTime;
		public String file;
		public String functionName;
		public LogLevel level;
		public int line;
		public String message;
		public String name;
		public String threadName;
		public LogRecord(IntPtr obj){
			objPtr = obj;

			categoryName = VidyoLogRecordGetcategoryNameNative(objPtr);
			eventTime = VidyoLogRecordGeteventTimeNative(objPtr);
			file = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoLogRecordGetfileNative(objPtr));
			functionName = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoLogRecordGetfunctionNameNative(objPtr));
			level = VidyoLogRecordGetlevelNative(objPtr);
			line = VidyoLogRecordGetlineNative(objPtr);
			message = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoLogRecordGetmessageNative(objPtr));
			name = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoLogRecordGetnameNative(objPtr));
			threadName = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoLogRecordGetthreadNameNative(objPtr));
		}
	};
}
