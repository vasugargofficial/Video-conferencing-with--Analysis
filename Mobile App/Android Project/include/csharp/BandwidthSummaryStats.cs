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
	public class BandwidthSummaryStatsFactory
	{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoBandwidthSummaryStatsConstructDefaultNative();
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoBandwidthSummaryStatsDestructNative(IntPtr obj);
		public static BandwidthSummaryStats Create()
		{
			IntPtr objPtr = VidyoBandwidthSummaryStatsConstructDefaultNative();
			return new BandwidthSummaryStats(objPtr);
		}
		public static void Destroy(BandwidthSummaryStats obj)
		{
			VidyoBandwidthSummaryStatsDestructNative(obj.GetObjectPtr());
		}
	}
	public class BandwidthSummaryStats{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoBandwidthSummaryStats reference.
		public IntPtr GetObjectPtr(){

			VidyoBandwidthSummaryStatsSetactualEncoderBitRateNative(objPtr, actualEncoderBitRate);
			VidyoBandwidthSummaryStatsSetavailableBandwidthNative(objPtr, availableBandwidth);
			VidyoBandwidthSummaryStatsSetleakyBucketDelayNative(objPtr, leakyBucketDelay);
			VidyoBandwidthSummaryStatsSetretransmitBitRateNative(objPtr, retransmitBitRate);
			VidyoBandwidthSummaryStatsSettargetEncoderBitRateNative(objPtr, targetEncoderBitRate);
			VidyoBandwidthSummaryStatsSettotalTransmitBitRateNative(objPtr, totalTransmitBitRate);

			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoBandwidthSummaryStatsGetactualEncoderBitRateNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoBandwidthSummaryStatsSetactualEncoderBitRateNative(IntPtr obj, ulong actualEncoderBitRate);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoBandwidthSummaryStatsGetavailableBandwidthNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoBandwidthSummaryStatsSetavailableBandwidthNative(IntPtr obj, ulong availableBandwidth);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoBandwidthSummaryStatsGetleakyBucketDelayNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoBandwidthSummaryStatsSetleakyBucketDelayNative(IntPtr obj, ulong leakyBucketDelay);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoBandwidthSummaryStatsGetretransmitBitRateNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoBandwidthSummaryStatsSetretransmitBitRateNative(IntPtr obj, ulong retransmitBitRate);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoBandwidthSummaryStatsGettargetEncoderBitRateNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoBandwidthSummaryStatsSettargetEncoderBitRateNative(IntPtr obj, ulong targetEncoderBitRate);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoBandwidthSummaryStatsGettotalTransmitBitRateNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoBandwidthSummaryStatsSettotalTransmitBitRateNative(IntPtr obj, ulong totalTransmitBitRate);

		public ulong actualEncoderBitRate;
		public ulong availableBandwidth;
		public ulong leakyBucketDelay;
		public ulong retransmitBitRate;
		public ulong targetEncoderBitRate;
		public ulong totalTransmitBitRate;
		public BandwidthSummaryStats(IntPtr obj){
			objPtr = obj;

			actualEncoderBitRate = VidyoBandwidthSummaryStatsGetactualEncoderBitRateNative(objPtr);
			availableBandwidth = VidyoBandwidthSummaryStatsGetavailableBandwidthNative(objPtr);
			leakyBucketDelay = VidyoBandwidthSummaryStatsGetleakyBucketDelayNative(objPtr);
			retransmitBitRate = VidyoBandwidthSummaryStatsGetretransmitBitRateNative(objPtr);
			targetEncoderBitRate = VidyoBandwidthSummaryStatsGettargetEncoderBitRateNative(objPtr);
			totalTransmitBitRate = VidyoBandwidthSummaryStatsGettotalTransmitBitRateNative(objPtr);
		}
	};
}
