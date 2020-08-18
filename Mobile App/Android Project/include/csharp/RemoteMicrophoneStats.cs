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
	public class RemoteMicrophoneStatsFactory
	{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoRemoteMicrophoneStatsConstructDefaultNative();
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoRemoteMicrophoneStatsDestructNative(IntPtr obj);
		public static RemoteMicrophoneStats Create()
		{
			IntPtr objPtr = VidyoRemoteMicrophoneStatsConstructDefaultNative();
			return new RemoteMicrophoneStats(objPtr);
		}
		public static void Destroy(RemoteMicrophoneStats obj)
		{
			VidyoRemoteMicrophoneStatsDestructNative(obj.GetObjectPtr());
		}
	}
	public class RemoteMicrophoneStats{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoRemoteMicrophoneStats reference.
		public IntPtr GetObjectPtr(){
			IntPtr nCodecName = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(codecName ?? string.Empty);
			IntPtr nId = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(id ?? string.Empty);
			IntPtr nLocalSpeakerStreams = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * localSpeakerStreams.Count);
			int nLocalSpeakerStreamsSize = 0;
			IntPtr nName = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(name ?? string.Empty);

			foreach (LocalSpeakerStreamStats iter in localSpeakerStreams) {
				Marshal.WriteIntPtr(nLocalSpeakerStreams + (nLocalSpeakerStreamsSize * Marshal.SizeOf<IntPtr>()), iter.GetObjectPtr());
				nLocalSpeakerStreamsSize++;
			}

			VidyoRemoteMicrophoneStatsSetbitsPerSampleNative(objPtr, bitsPerSample);
			VidyoRemoteMicrophoneStatsSetcodecDtxNative(objPtr, codecDtx);
			VidyoRemoteMicrophoneStatsSetcodecNameNative(objPtr, nCodecName);
			VidyoRemoteMicrophoneStatsSetcodecQualitySettingNative(objPtr, codecQualitySetting);
			VidyoRemoteMicrophoneStatsSetidNative(objPtr, nId);
			VidyoRemoteMicrophoneStatsSetlastFrameMsNative(objPtr, lastFrameMs);
			VidyoRemoteMicrophoneStatsSetlocalSpeakerStreamsNative(objPtr, nLocalSpeakerStreams, nLocalSpeakerStreamsSize);
			VidyoRemoteMicrophoneStatsSetnameNative(objPtr, nName);
			VidyoRemoteMicrophoneStatsSetnumberOfChannelsNative(objPtr, numberOfChannels);
			VidyoRemoteMicrophoneStatsSetreceiveNetworkBitRateNative(objPtr, receiveNetworkBitRate);
			VidyoRemoteMicrophoneStatsSetreceiveNetworkDelayNative(objPtr, receiveNetworkDelay);
			VidyoRemoteMicrophoneStatsSetreceiveNetworkDroppedPacketsNative(objPtr, receiveNetworkDroppedPackets);
			VidyoRemoteMicrophoneStatsSetreceiveNetworkJitterNative(objPtr, receiveNetworkJitter);
			VidyoRemoteMicrophoneStatsSetreceiveNetworkPacketsConcealedNative(objPtr, receiveNetworkPacketsConcealed);
			VidyoRemoteMicrophoneStatsSetreceiveNetworkPacketsLostNative(objPtr, receiveNetworkPacketsLost);
			VidyoRemoteMicrophoneStatsSetsampleRateMeasuredNative(objPtr, sampleRateMeasured);
			VidyoRemoteMicrophoneStatsSetsampleRateSetNative(objPtr, sampleRateSet);

			Marshal.FreeHGlobal(nName);
			Marshal.FreeHGlobal(nLocalSpeakerStreams);
			Marshal.FreeHGlobal(nId);
			Marshal.FreeHGlobal(nCodecName);
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoRemoteMicrophoneStatsGetbitsPerSampleNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetbitsPerSampleNative(IntPtr obj, uint bitsPerSample);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoRemoteMicrophoneStatsGetcodecDtxNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetcodecDtxNative(IntPtr obj, uint codecDtx);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoRemoteMicrophoneStatsGetcodecNameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetcodecNameNative(IntPtr obj, IntPtr codecName);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoRemoteMicrophoneStatsGetcodecQualitySettingNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetcodecQualitySettingNative(IntPtr obj, uint codecQualitySetting);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoRemoteMicrophoneStatsGetidNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetidNative(IntPtr obj, IntPtr id);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern int VidyoRemoteMicrophoneStatsGetlastFrameMsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetlastFrameMsNative(IntPtr obj, int lastFrameMs);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoRemoteMicrophoneStatsGetlocalSpeakerStreamsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetlocalSpeakerStreamsNative(IntPtr obj, IntPtr localSpeakerStreams, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoRemoteMicrophoneStatsGetlocalSpeakerStreamsArrayNative(IntPtr obj, ref int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsFreelocalSpeakerStreamsArrayNative(IntPtr obj, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoRemoteMicrophoneStatsGetnameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetnameNative(IntPtr obj, IntPtr name);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoRemoteMicrophoneStatsGetnumberOfChannelsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetnumberOfChannelsNative(IntPtr obj, uint numberOfChannels);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoRemoteMicrophoneStatsGetreceiveNetworkBitRateNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetreceiveNetworkBitRateNative(IntPtr obj, ulong receiveNetworkBitRate);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoRemoteMicrophoneStatsGetreceiveNetworkDelayNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetreceiveNetworkDelayNative(IntPtr obj, ulong receiveNetworkDelay);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoRemoteMicrophoneStatsGetreceiveNetworkDroppedPacketsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetreceiveNetworkDroppedPacketsNative(IntPtr obj, ulong receiveNetworkDroppedPackets);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoRemoteMicrophoneStatsGetreceiveNetworkJitterNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetreceiveNetworkJitterNative(IntPtr obj, uint receiveNetworkJitter);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoRemoteMicrophoneStatsGetreceiveNetworkPacketsConcealedNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetreceiveNetworkPacketsConcealedNative(IntPtr obj, ulong receiveNetworkPacketsConcealed);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoRemoteMicrophoneStatsGetreceiveNetworkPacketsLostNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetreceiveNetworkPacketsLostNative(IntPtr obj, ulong receiveNetworkPacketsLost);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoRemoteMicrophoneStatsGetsampleRateMeasuredNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetsampleRateMeasuredNative(IntPtr obj, uint sampleRateMeasured);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern uint VidyoRemoteMicrophoneStatsGetsampleRateSetNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoRemoteMicrophoneStatsSetsampleRateSetNative(IntPtr obj, uint sampleRateSet);

		public uint bitsPerSample;
		public uint codecDtx;
		public String codecName;
		public uint codecQualitySetting;
		public String id;
		public int lastFrameMs;
		public List<LocalSpeakerStreamStats> localSpeakerStreams;
		public String name;
		public uint numberOfChannels;
		public ulong receiveNetworkBitRate;
		public ulong receiveNetworkDelay;
		public ulong receiveNetworkDroppedPackets;
		public uint receiveNetworkJitter;
		public ulong receiveNetworkPacketsConcealed;
		public ulong receiveNetworkPacketsLost;
		public uint sampleRateMeasured;
		public uint sampleRateSet;
		public RemoteMicrophoneStats(IntPtr obj){
			objPtr = obj;

			List<LocalSpeakerStreamStats> csLocalSpeakerStreams = new List<LocalSpeakerStreamStats>();
			int nLocalSpeakerStreamsSize = 0;
			IntPtr nLocalSpeakerStreams = VidyoRemoteMicrophoneStatsGetlocalSpeakerStreamsArrayNative(VidyoRemoteMicrophoneStatsGetlocalSpeakerStreamsNative(objPtr), ref nLocalSpeakerStreamsSize);
			int nLocalSpeakerStreamsIndex = 0;
			while (nLocalSpeakerStreamsIndex < nLocalSpeakerStreamsSize) {
				LocalSpeakerStreamStats csTlocalSpeakerStreams = new LocalSpeakerStreamStats(Marshal.ReadIntPtr(nLocalSpeakerStreams + (nLocalSpeakerStreamsIndex * Marshal.SizeOf(nLocalSpeakerStreams))));
				csLocalSpeakerStreams.Add(csTlocalSpeakerStreams);
				nLocalSpeakerStreamsIndex++;
			}

			bitsPerSample = VidyoRemoteMicrophoneStatsGetbitsPerSampleNative(objPtr);
			codecDtx = VidyoRemoteMicrophoneStatsGetcodecDtxNative(objPtr);
			codecName = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoRemoteMicrophoneStatsGetcodecNameNative(objPtr));
			codecQualitySetting = VidyoRemoteMicrophoneStatsGetcodecQualitySettingNative(objPtr);
			id = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoRemoteMicrophoneStatsGetidNative(objPtr));
			lastFrameMs = VidyoRemoteMicrophoneStatsGetlastFrameMsNative(objPtr);
			localSpeakerStreams = csLocalSpeakerStreams;
			name = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoRemoteMicrophoneStatsGetnameNative(objPtr));
			numberOfChannels = VidyoRemoteMicrophoneStatsGetnumberOfChannelsNative(objPtr);
			receiveNetworkBitRate = VidyoRemoteMicrophoneStatsGetreceiveNetworkBitRateNative(objPtr);
			receiveNetworkDelay = VidyoRemoteMicrophoneStatsGetreceiveNetworkDelayNative(objPtr);
			receiveNetworkDroppedPackets = VidyoRemoteMicrophoneStatsGetreceiveNetworkDroppedPacketsNative(objPtr);
			receiveNetworkJitter = VidyoRemoteMicrophoneStatsGetreceiveNetworkJitterNative(objPtr);
			receiveNetworkPacketsConcealed = VidyoRemoteMicrophoneStatsGetreceiveNetworkPacketsConcealedNative(objPtr);
			receiveNetworkPacketsLost = VidyoRemoteMicrophoneStatsGetreceiveNetworkPacketsLostNative(objPtr);
			sampleRateMeasured = VidyoRemoteMicrophoneStatsGetsampleRateMeasuredNative(objPtr);
			sampleRateSet = VidyoRemoteMicrophoneStatsGetsampleRateSetNative(objPtr);
			VidyoRemoteMicrophoneStatsFreelocalSpeakerStreamsArrayNative(nLocalSpeakerStreams, nLocalSpeakerStreamsSize);
		}
	};
}