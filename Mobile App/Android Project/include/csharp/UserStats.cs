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
	public class UserStatsFactory
	{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoUserStatsConstructDefaultNative();
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoUserStatsDestructNative(IntPtr obj);
		public static UserStats Create()
		{
			IntPtr objPtr = VidyoUserStatsConstructDefaultNative();
			return new UserStats(objPtr);
		}
		public static void Destroy(UserStats obj)
		{
			VidyoUserStatsDestructNative(obj.GetObjectPtr());
		}
	}
	public class UserStats{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoUserStats reference.
		public IntPtr GetObjectPtr(){
			IntPtr nHost = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(host ?? string.Empty);
			IntPtr nId = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(id ?? string.Empty);
			IntPtr nRoomStats = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * roomStats.Count);
			int nRoomStatsSize = 0;
			IntPtr nServiceType = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(serviceType ?? string.Empty);

			foreach (RoomStats iter in roomStats) {
				Marshal.WriteIntPtr(nRoomStats + (nRoomStatsSize * Marshal.SizeOf<IntPtr>()), iter.GetObjectPtr());
				nRoomStatsSize++;
			}

			VidyoUserStatsSethostNative(objPtr, nHost);
			VidyoUserStatsSetidNative(objPtr, nId);
			VidyoUserStatsSetlatencyTestStatsNative(objPtr, latencyTestStats.GetObjectPtr());
			VidyoUserStatsSetportNative(objPtr, port);
			VidyoUserStatsSetroomStatsNative(objPtr, nRoomStats, nRoomStatsSize);
			VidyoUserStatsSetserviceTypeNative(objPtr, nServiceType);

			Marshal.FreeHGlobal(nServiceType);
			Marshal.FreeHGlobal(nRoomStats);
			Marshal.FreeHGlobal(nId);
			Marshal.FreeHGlobal(nHost);
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoUserStatsGethostNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoUserStatsSethostNative(IntPtr obj, IntPtr host);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoUserStatsGetidNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoUserStatsSetidNative(IntPtr obj, IntPtr id);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoUserStatsGetlatencyTestStatsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoUserStatsSetlatencyTestStatsNative(IntPtr obj, IntPtr latencyTestStats);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoUserStatsGetportNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoUserStatsSetportNative(IntPtr obj, uint port);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoUserStatsGetroomStatsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoUserStatsSetroomStatsNative(IntPtr obj, IntPtr roomStats, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoUserStatsGetroomStatsArrayNative(IntPtr obj, ref int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoUserStatsFreeroomStatsArrayNative(IntPtr obj, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoUserStatsGetserviceTypeNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoUserStatsSetserviceTypeNative(IntPtr obj, IntPtr serviceType);

		public String host;
		public String id;
		public LatencyTestStats latencyTestStats;
		public uint port;
		public List<RoomStats> roomStats;
		public String serviceType;
		public UserStats(IntPtr obj){
			objPtr = obj;

			LatencyTestStats csLatencyTestStats = new LatencyTestStats(VidyoUserStatsGetlatencyTestStatsNative(objPtr));
			List<RoomStats> csRoomStats = new List<RoomStats>();
			int nRoomStatsSize = 0;
			IntPtr nRoomStats = VidyoUserStatsGetroomStatsArrayNative(VidyoUserStatsGetroomStatsNative(objPtr), ref nRoomStatsSize);
			int nRoomStatsIndex = 0;
			while (nRoomStatsIndex < nRoomStatsSize) {
				RoomStats csTroomStats = new RoomStats(Marshal.ReadIntPtr(nRoomStats + (nRoomStatsIndex * Marshal.SizeOf(nRoomStats))));
				csRoomStats.Add(csTroomStats);
				nRoomStatsIndex++;
			}

			host = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoUserStatsGethostNative(objPtr));
			id = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoUserStatsGetidNative(objPtr));
			latencyTestStats = csLatencyTestStats;
			port = VidyoUserStatsGetportNative(objPtr);
			roomStats = csRoomStats;
			serviceType = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoUserStatsGetserviceTypeNative(objPtr));
			VidyoUserStatsFreeroomStatsArrayNative(nRoomStats, nRoomStatsSize);
		}
	};
}
