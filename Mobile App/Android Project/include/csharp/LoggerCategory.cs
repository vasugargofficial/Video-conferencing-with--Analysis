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
	public class LoggerCategoryFactory
	{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoLoggerCategoryConstructDefaultNative();
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoLoggerCategoryDestructNative(IntPtr obj);
		public static LoggerCategory Create()
		{
			IntPtr objPtr = VidyoLoggerCategoryConstructDefaultNative();
			return new LoggerCategory(objPtr);
		}
		public static void Destroy(LoggerCategory obj)
		{
			VidyoLoggerCategoryDestructNative(obj.GetObjectPtr());
		}
	}
	public class LoggerCategory{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoLoggerCategory reference.
		public IntPtr GetObjectPtr(){
			IntPtr nDescription = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(description ?? string.Empty);
			IntPtr nLevels = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * levels.Count);
			int nLevelsSize = 0;
			IntPtr nName = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(name ?? string.Empty);

			foreach (String iter in levels) {
				Marshal.WriteIntPtr(nLevels + (nLevelsSize * Marshal.SizeOf<IntPtr>()), MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(iter ?? string.Empty));
				nLevelsSize++;
			}

			VidyoLoggerCategorySetdescriptionNative(objPtr, nDescription);
			VidyoLoggerCategorySetlevelsNative(objPtr, nLevels, nLevelsSize);
			VidyoLoggerCategorySetnameNative(objPtr, nName);

			for (int i = 0; i < nLevelsSize; i++) {
				Marshal.FreeHGlobal(Marshal.ReadIntPtr(nLevels + (i * Marshal.SizeOf<IntPtr>())));
			}

			Marshal.FreeHGlobal(nName);
			Marshal.FreeHGlobal(nLevels);
			Marshal.FreeHGlobal(nDescription);
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLoggerCategoryGetdescriptionNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLoggerCategorySetdescriptionNative(IntPtr obj, IntPtr description);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLoggerCategoryGetlevelsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLoggerCategorySetlevelsNative(IntPtr obj, IntPtr levels, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLoggerCategoryGetlevelsArrayNative(IntPtr obj, ref int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLoggerCategoryFreelevelsArrayNative(IntPtr obj, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoLoggerCategoryGetnameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoLoggerCategorySetnameNative(IntPtr obj, IntPtr name);

		public String description;
		public List<String> levels;
		public String name;
		public LoggerCategory(IntPtr obj){
			objPtr = obj;

			List<String> csLevels = new List<String>();
			int nLevelsSize = 0;
			IntPtr nLevels = VidyoLoggerCategoryGetlevelsArrayNative(VidyoLoggerCategoryGetlevelsNative(objPtr), ref nLevelsSize);
			int nLevelsIndex = 0;
			while (nLevelsIndex < nLevelsSize) {
				csLevels.Add((string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(Marshal.ReadIntPtr(nLevels + (nLevelsIndex * Marshal.SizeOf(nLevels)))));
				nLevelsIndex++;
			}

			description = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoLoggerCategoryGetdescriptionNative(objPtr));
			levels = csLevels;
			name = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoLoggerCategoryGetnameNative(objPtr));
			VidyoLoggerCategoryFreelevelsArrayNative(nLevels, nLevelsSize);
		}
	};
}