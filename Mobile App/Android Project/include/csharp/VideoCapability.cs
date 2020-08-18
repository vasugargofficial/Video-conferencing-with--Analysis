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
	public class VideoCapabilityFactory
	{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoVideoCapabilityConstructDefaultNative();
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoVideoCapabilityDestructNative(IntPtr obj);
		public static VideoCapability Create()
		{
			IntPtr objPtr = VidyoVideoCapabilityConstructDefaultNative();
			return new VideoCapability(objPtr);
		}
		public static void Destroy(VideoCapability obj)
		{
			VidyoVideoCapabilityDestructNative(obj.GetObjectPtr());
		}
	}
	public class VideoCapability{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoVideoCapability reference.
		public IntPtr GetObjectPtr(){
			IntPtr nRanges = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * ranges.Count);
			int nRangesSize = 0;

			foreach (VideoFrameIntervalRange iter in ranges) {
				Marshal.WriteIntPtr(nRanges + (nRangesSize * Marshal.SizeOf<IntPtr>()), iter.GetObjectPtr());
				nRangesSize++;
			}

			VidyoVideoCapabilitySetheightNative(objPtr, height);
			VidyoVideoCapabilitySetrangesNative(objPtr, nRanges, nRangesSize);
			VidyoVideoCapabilitySetwidthNative(objPtr, width);

			Marshal.FreeHGlobal(nRanges);
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoVideoCapabilityGetheightNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoVideoCapabilitySetheightNative(IntPtr obj, ulong height);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoVideoCapabilityGetrangesNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoVideoCapabilitySetrangesNative(IntPtr obj, IntPtr ranges, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoVideoCapabilityGetrangesArrayNative(IntPtr obj, ref int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoVideoCapabilityFreerangesArrayNative(IntPtr obj, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoVideoCapabilityGetwidthNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoVideoCapabilitySetwidthNative(IntPtr obj, ulong width);

		public class VideoFrameIntervalRangeFactory
		{
#if __IOS__
			const string importLib = "__Internal";
#else
			const string importLib = "libVidyoClient";
#endif
			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr VidyoVideoFrameIntervalRangeConstructDefaultNative();
			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			public static extern void VidyoVideoFrameIntervalRangeDestructNative(IntPtr obj);
			public static VideoFrameIntervalRange Create()
			{
				IntPtr objPtr = VidyoVideoFrameIntervalRangeConstructDefaultNative();
				return new VideoFrameIntervalRange(objPtr);
			}
			public static void Destroy(VideoFrameIntervalRange obj)
			{
				VidyoVideoFrameIntervalRangeDestructNative(obj.GetObjectPtr());
			}
		}
		public class VideoFrameIntervalRange{
#if __IOS__
			const string importLib = "__Internal";
#else
			const string importLib = "libVidyoClient";
#endif
			private IntPtr objPtr; // opaque VidyoVideoFrameIntervalRange reference.
			public IntPtr GetObjectPtr(){
				IntPtr nFormats = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * formats.Count);
				int nFormatsSize = 0;

				foreach (MediaFormat iter in formats) {
					Marshal.WriteInt32(nFormats + (nFormatsSize * Marshal.SizeOf<Int32>()), (int)iter);
					nFormatsSize++;
				}

				VidyoVideoFrameIntervalRangeSetformatsNative(objPtr, nFormats, nFormatsSize);
				VidyoVideoFrameIntervalRangeSetrangeNative(objPtr, range.GetObjectPtr());
				VidyoVideoFrameIntervalRangeSetstepNative(objPtr, step);

				Marshal.FreeHGlobal(nFormats);
				return objPtr;
			}
			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr VidyoVideoFrameIntervalRangeGetformatsNative(IntPtr obj);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern void VidyoVideoFrameIntervalRangeSetformatsNative(IntPtr obj, IntPtr formats, int size);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr VidyoVideoFrameIntervalRangeGetformatsArrayNative(IntPtr obj, ref int size);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern void VidyoVideoFrameIntervalRangeFreeformatsArrayNative(IntPtr obj, int size);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr VidyoVideoFrameIntervalRangeGetrangeNative(IntPtr obj);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern void VidyoVideoFrameIntervalRangeSetrangeNative(IntPtr obj, IntPtr range);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern ulong VidyoVideoFrameIntervalRangeGetstepNative(IntPtr obj);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern void VidyoVideoFrameIntervalRangeSetstepNative(IntPtr obj, ulong step);

			public class TimeRangeFactory
			{
#if __IOS__
				const string importLib = "__Internal";
#else
				const string importLib = "libVidyoClient";
#endif
				[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
				public static extern IntPtr VidyoTimeRangeConstructDefaultNative();
				[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
				public static extern void VidyoTimeRangeDestructNative(IntPtr obj);
				public static TimeRange Create()
				{
					IntPtr objPtr = VidyoTimeRangeConstructDefaultNative();
					return new TimeRange(objPtr);
				}
				public static void Destroy(TimeRange obj)
				{
					VidyoTimeRangeDestructNative(obj.GetObjectPtr());
				}
			}
			public class TimeRange{
#if __IOS__
				const string importLib = "__Internal";
#else
				const string importLib = "libVidyoClient";
#endif
				private IntPtr objPtr; // opaque VidyoTimeRange reference.
				public IntPtr GetObjectPtr(){

					VidyoTimeRangeSetbeginNative(objPtr, begin);
					VidyoTimeRangeSetendNative(objPtr, end);

					return objPtr;
				}
				[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
				private static extern ulong VidyoTimeRangeGetbeginNative(IntPtr obj);

				[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
				private static extern void VidyoTimeRangeSetbeginNative(IntPtr obj, ulong begin);

				[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
				private static extern ulong VidyoTimeRangeGetendNative(IntPtr obj);

				[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
				private static extern void VidyoTimeRangeSetendNative(IntPtr obj, ulong end);

				public ulong begin;
				public ulong end;
				public TimeRange(IntPtr obj){
					objPtr = obj;

					begin = VidyoTimeRangeGetbeginNative(objPtr);
					end = VidyoTimeRangeGetendNative(objPtr);
				}
			};
			public List<MediaFormat> formats;
			public TimeRange range;
			public ulong step;
			public VideoFrameIntervalRange(IntPtr obj){
				objPtr = obj;

				List<MediaFormat> csFormats = new List<MediaFormat>();
				int nFormatsSize = 0;
				IntPtr nFormats = VidyoVideoFrameIntervalRangeGetformatsArrayNative(VidyoVideoFrameIntervalRangeGetformatsNative(objPtr), ref nFormatsSize);
				int nFormatsIndex = 0;
				while (nFormatsIndex < nFormatsSize) {
					csFormats.Add((MediaFormat)Marshal.ReadInt32(Marshal.ReadIntPtr(nFormats + (nFormatsIndex * Marshal.SizeOf(nFormats)))));
					nFormatsIndex++;
				}

				TimeRange csRange = new TimeRange(VidyoVideoFrameIntervalRangeGetrangeNative(objPtr));
				formats = csFormats;
				range = csRange;
				step = VidyoVideoFrameIntervalRangeGetstepNative(objPtr);
				VidyoVideoFrameIntervalRangeFreeformatsArrayNative(nFormats, nFormatsSize);
			}
		};
		public ulong height;
		public List<VideoFrameIntervalRange> ranges;
		public ulong width;
		public VideoCapability(IntPtr obj){
			objPtr = obj;

			List<VideoFrameIntervalRange> csRanges = new List<VideoFrameIntervalRange>();
			int nRangesSize = 0;
			IntPtr nRanges = VidyoVideoCapabilityGetrangesArrayNative(VidyoVideoCapabilityGetrangesNative(objPtr), ref nRangesSize);
			int nRangesIndex = 0;
			while (nRangesIndex < nRangesSize) {
				VideoFrameIntervalRange csTranges = new VideoFrameIntervalRange(Marshal.ReadIntPtr(nRanges + (nRangesIndex * Marshal.SizeOf(nRanges))));
				csRanges.Add(csTranges);
				nRangesIndex++;
			}

			height = VidyoVideoCapabilityGetheightNative(objPtr);
			ranges = csRanges;
			width = VidyoVideoCapabilityGetwidthNative(objPtr);
			VidyoVideoCapabilityFreerangesArrayNative(nRanges, nRangesSize);
		}
	};
}