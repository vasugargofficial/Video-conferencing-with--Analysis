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
	public class ContactInfoFactory
	{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr VidyoContactInfoConstructDefaultNative();
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void VidyoContactInfoDestructNative(IntPtr obj);
		public static ContactInfo Create()
		{
			IntPtr objPtr = VidyoContactInfoConstructDefaultNative();
			return new ContactInfo(objPtr);
		}
		public static void Destroy(ContactInfo obj)
		{
			VidyoContactInfoDestructNative(obj.GetObjectPtr());
		}
	}
	public class ContactInfo{
#if __IOS__
		const string importLib = "__Internal";
#else
		const string importLib = "libVidyoClient";
#endif
		private IntPtr objPtr; // opaque VidyoContactInfo reference.
		public IntPtr GetObjectPtr(){
			IntPtr nEmails = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * emails.Count);
			int nEmailsSize = 0;
			IntPtr nGroups = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * groups.Count);
			int nGroupsSize = 0;
			IntPtr nHandle = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(handle ?? string.Empty);
			IntPtr nId = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(id ?? string.Empty);
			IntPtr nName = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(name ?? string.Empty);
			IntPtr nNickname = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(nickname ?? string.Empty);
			IntPtr nPhoto = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(photo ?? string.Empty);
			IntPtr nTelephones = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * telephones.Count);
			int nTelephonesSize = 0;

			foreach (ContactInfoProperty iter in emails) {
				Marshal.WriteIntPtr(nEmails + (nEmailsSize * Marshal.SizeOf<IntPtr>()), iter.GetObjectPtr());
				nEmailsSize++;
			}
			foreach (String iter in groups) {
				Marshal.WriteIntPtr(nGroups + (nGroupsSize * Marshal.SizeOf<IntPtr>()), MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(iter ?? string.Empty));
				nGroupsSize++;
			}
			foreach (ContactInfoProperty iter in telephones) {
				Marshal.WriteIntPtr(nTelephones + (nTelephonesSize * Marshal.SizeOf<IntPtr>()), iter.GetObjectPtr());
				nTelephonesSize++;
			}

			VidyoContactInfoSetemailsNative(objPtr, nEmails, nEmailsSize);
			VidyoContactInfoSetgroupsNative(objPtr, nGroups, nGroupsSize);
			VidyoContactInfoSethandleNative(objPtr, nHandle);
			VidyoContactInfoSetidNative(objPtr, nId);
			VidyoContactInfoSetnameNative(objPtr, nName);
			VidyoContactInfoSetnicknameNative(objPtr, nNickname);
			VidyoContactInfoSetphotoNative(objPtr, nPhoto);
			VidyoContactInfoSetpresenceStateNative(objPtr, presenceState);
			VidyoContactInfoSettelephonesNative(objPtr, nTelephones, nTelephonesSize);
			VidyoContactInfoSettimestampNative(objPtr, timestamp);

			for (int i = 0; i < nGroupsSize; i++) {
				Marshal.FreeHGlobal(Marshal.ReadIntPtr(nGroups + (i * Marshal.SizeOf<IntPtr>())));
			}

			Marshal.FreeHGlobal(nTelephones);
			Marshal.FreeHGlobal(nPhoto);
			Marshal.FreeHGlobal(nNickname);
			Marshal.FreeHGlobal(nName);
			Marshal.FreeHGlobal(nId);
			Marshal.FreeHGlobal(nHandle);
			Marshal.FreeHGlobal(nGroups);
			Marshal.FreeHGlobal(nEmails);
			return objPtr;
		}
		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGetemailsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSetemailsNative(IntPtr obj, IntPtr emails, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGetemailsArrayNative(IntPtr obj, ref int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoFreeemailsArrayNative(IntPtr obj, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGetgroupsNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSetgroupsNative(IntPtr obj, IntPtr groups, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGetgroupsArrayNative(IntPtr obj, ref int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoFreegroupsArrayNative(IntPtr obj, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGethandleNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSethandleNative(IntPtr obj, IntPtr handle);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGetidNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSetidNative(IntPtr obj, IntPtr id);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGetnameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSetnameNative(IntPtr obj, IntPtr name);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGetnicknameNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSetnicknameNative(IntPtr obj, IntPtr nickname);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGetphotoNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSetphotoNative(IntPtr obj, IntPtr photo);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern ContactInfoPresenceState VidyoContactInfoGetpresenceStateNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern void VidyoContactInfoSetpresenceStateNative(IntPtr obj, ContactInfoPresenceState presenceState);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGettelephonesNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSettelephonesNative(IntPtr obj, IntPtr telephones, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr VidyoContactInfoGettelephonesArrayNative(IntPtr obj, ref int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoFreetelephonesArrayNative(IntPtr obj, int size);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong VidyoContactInfoGettimestampNative(IntPtr obj);

		[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
		private static extern void VidyoContactInfoSettimestampNative(IntPtr obj, ulong timestamp);

		public enum ContactInfoPresenceState{
			ContactinfopresencestateUnavailable,
			ContactinfopresencestateDoNotDisturb,
			ContactinfopresencestateExtendedAway,
			ContactinfopresencestateAway,
			ContactinfopresencestateAvailable,
			ContactinfopresencestateInterestedInChat
		}
		public class ContactInfoPropertyFactory
		{
#if __IOS__
			const string importLib = "__Internal";
#else
			const string importLib = "libVidyoClient";
#endif
			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr VidyoContactInfoPropertyConstructDefaultNative();
			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			public static extern void VidyoContactInfoPropertyDestructNative(IntPtr obj);
			public static ContactInfoProperty Create()
			{
				IntPtr objPtr = VidyoContactInfoPropertyConstructDefaultNative();
				return new ContactInfoProperty(objPtr);
			}
			public static void Destroy(ContactInfoProperty obj)
			{
				VidyoContactInfoPropertyDestructNative(obj.GetObjectPtr());
			}
		}
		public class ContactInfoProperty{
#if __IOS__
			const string importLib = "__Internal";
#else
			const string importLib = "libVidyoClient";
#endif
			private IntPtr objPtr; // opaque VidyoContactInfoProperty reference.
			public IntPtr GetObjectPtr(){
				IntPtr nTypes = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * types.Count);
				int nTypesSize = 0;
				IntPtr nValue = MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(value ?? string.Empty);

				foreach (String iter in types) {
					Marshal.WriteIntPtr(nTypes + (nTypesSize * Marshal.SizeOf<IntPtr>()), MarshalPtrToUtf8.GetInstance().MarshalManagedToNative(iter ?? string.Empty));
					nTypesSize++;
				}

				VidyoContactInfoPropertySettypesNative(objPtr, nTypes, nTypesSize);
				VidyoContactInfoPropertySetvalueNative(objPtr, nValue);

				for (int i = 0; i < nTypesSize; i++) {
					Marshal.FreeHGlobal(Marshal.ReadIntPtr(nTypes + (i * Marshal.SizeOf<IntPtr>())));
				}

				Marshal.FreeHGlobal(nValue);
				Marshal.FreeHGlobal(nTypes);
				return objPtr;
			}
			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr VidyoContactInfoPropertyGettypesNative(IntPtr obj);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern void VidyoContactInfoPropertySettypesNative(IntPtr obj, IntPtr types, int size);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr VidyoContactInfoPropertyGettypesArrayNative(IntPtr obj, ref int size);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern void VidyoContactInfoPropertyFreetypesArrayNative(IntPtr obj, int size);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr VidyoContactInfoPropertyGetvalueNative(IntPtr obj);

			[DllImport(importLib, CallingConvention = CallingConvention.Cdecl)]
			private static extern void VidyoContactInfoPropertySetvalueNative(IntPtr obj, IntPtr value);

			public List<String> types;
			public String value;
			public ContactInfoProperty(IntPtr obj){
				objPtr = obj;

				List<String> csTypes = new List<String>();
				int nTypesSize = 0;
				IntPtr nTypes = VidyoContactInfoPropertyGettypesArrayNative(VidyoContactInfoPropertyGettypesNative(objPtr), ref nTypesSize);
				int nTypesIndex = 0;
				while (nTypesIndex < nTypesSize) {
					csTypes.Add((string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(Marshal.ReadIntPtr(nTypes + (nTypesIndex * Marshal.SizeOf(nTypes)))));
					nTypesIndex++;
				}

				types = csTypes;
				value = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoContactInfoPropertyGetvalueNative(objPtr));
				VidyoContactInfoPropertyFreetypesArrayNative(nTypes, nTypesSize);
			}
		};
		public List<ContactInfoProperty> emails;
		public List<String> groups;
		public String handle;
		public String id;
		public String name;
		public String nickname;
		public String photo;
		public ContactInfoPresenceState presenceState;
		public List<ContactInfoProperty> telephones;
		public ulong timestamp;
		public ContactInfo(IntPtr obj){
			objPtr = obj;

			List<ContactInfoProperty> csEmails = new List<ContactInfoProperty>();
			int nEmailsSize = 0;
			IntPtr nEmails = VidyoContactInfoGetemailsArrayNative(VidyoContactInfoGetemailsNative(objPtr), ref nEmailsSize);
			int nEmailsIndex = 0;
			while (nEmailsIndex < nEmailsSize) {
				ContactInfoProperty csTemails = new ContactInfoProperty(Marshal.ReadIntPtr(nEmails + (nEmailsIndex * Marshal.SizeOf(nEmails))));
				csEmails.Add(csTemails);
				nEmailsIndex++;
			}

			List<String> csGroups = new List<String>();
			int nGroupsSize = 0;
			IntPtr nGroups = VidyoContactInfoGetgroupsArrayNative(VidyoContactInfoGetgroupsNative(objPtr), ref nGroupsSize);
			int nGroupsIndex = 0;
			while (nGroupsIndex < nGroupsSize) {
				csGroups.Add((string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(Marshal.ReadIntPtr(nGroups + (nGroupsIndex * Marshal.SizeOf(nGroups)))));
				nGroupsIndex++;
			}

			List<ContactInfoProperty> csTelephones = new List<ContactInfoProperty>();
			int nTelephonesSize = 0;
			IntPtr nTelephones = VidyoContactInfoGettelephonesArrayNative(VidyoContactInfoGettelephonesNative(objPtr), ref nTelephonesSize);
			int nTelephonesIndex = 0;
			while (nTelephonesIndex < nTelephonesSize) {
				ContactInfoProperty csTtelephones = new ContactInfoProperty(Marshal.ReadIntPtr(nTelephones + (nTelephonesIndex * Marshal.SizeOf(nTelephones))));
				csTelephones.Add(csTtelephones);
				nTelephonesIndex++;
			}

			emails = csEmails;
			groups = csGroups;
			handle = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoContactInfoGethandleNative(objPtr));
			id = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoContactInfoGetidNative(objPtr));
			name = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoContactInfoGetnameNative(objPtr));
			nickname = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoContactInfoGetnicknameNative(objPtr));
			photo = (string)MarshalPtrToUtf8.GetInstance().MarshalNativeToManaged(VidyoContactInfoGetphotoNative(objPtr));
			presenceState = VidyoContactInfoGetpresenceStateNative(objPtr);
			telephones = csTelephones;
			timestamp = VidyoContactInfoGettimestampNative(objPtr);
			VidyoContactInfoFreetelephonesArrayNative(nTelephones, nTelephonesSize);
			VidyoContactInfoFreegroupsArrayNative(nGroups, nGroupsSize);
			VidyoContactInfoFreeemailsArrayNative(nEmails, nEmailsSize);
		}
	};
}
