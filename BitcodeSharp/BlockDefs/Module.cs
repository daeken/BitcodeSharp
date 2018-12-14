using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PrettyPrinter;

namespace BitcodeSharp {
	public enum ModuleRecordCode : uint {
		Version = 1, 
		Triple = 2, 
		DataLayout = 3, 
		Function = 8, 
		VstOffset = 13, 
		SourceFilename = 16
	}

	public class ModuleBlock {
		public readonly ParamAttrBlock ParamAttr;
		public readonly ParamAttrGroupBlock ParamAttrGroup;
		public readonly ConstantsBlock Constants;
		public readonly List<FunctionBlock> Functions = new List<FunctionBlock>();
		public readonly ValueSymtabBlock ValueSymtab;
		public readonly MetadataBlock Metadata;
		public readonly TypeBlock Type;
		public readonly OperandBundleTagsBlock OperandBundleTags;
		public readonly MetadataKindBlock MetadataKind;
		public readonly SyncScopeNamesBlock SyncScopeNames;

		public readonly uint Version, VstOffset;
		public readonly string Triple, DataLayout, SourceFilename;
		public readonly List<FunctionRecord> FunctionRecords = new List<FunctionRecord>();
		
		public ModuleBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.Module);
			
			foreach(var (code, record) in rb.Records)
				switch((ModuleRecordCode) code) {
					case ModuleRecordCode.Version:
						Debug.Assert(Version == 0);
						Version = record[0];
						break;
					case ModuleRecordCode.Triple:
						Debug.Assert(Triple == null);
						Triple = Encoding.UTF8.GetString(record.Select(x => (byte) x).ToArray());
						break;
					case ModuleRecordCode.DataLayout:
						Debug.Assert(DataLayout == null);
						DataLayout = Encoding.UTF8.GetString(record.Select(x => (byte) x).ToArray());
						break;
					case ModuleRecordCode.Function:
						FunctionRecords.Add(new FunctionRecord(record));
						break;
					case ModuleRecordCode.VstOffset:
						Debug.Assert(VstOffset == 0);
						VstOffset = record[0];
						break;
					case ModuleRecordCode.SourceFilename:
						Debug.Assert(SourceFilename == null);
						SourceFilename = Encoding.UTF8.GetString(record.Select(x => (byte) x).ToArray());
						break;
					case ModuleRecordCode rc: throw new NotSupportedException($"Unexpected record in Module: {rc}");
				}
			
			foreach(var child in rb.Children)
				switch(child.BlockId) {
					case BlockCode.ParamAttr:
						Debug.Assert(ParamAttr == null);
						ParamAttr = new ParamAttrBlock(child);
						break;
					case BlockCode.ParamAttrGroup:
						Debug.Assert(ParamAttrGroup == null);
						ParamAttrGroup = new ParamAttrGroupBlock(child);
						break;
					case BlockCode.Constants:
						Debug.Assert(Constants == null);
						Constants = new ConstantsBlock(child);
						break;
					case BlockCode.Function:
						Functions.Add(new FunctionBlock(child));
						break;
					case BlockCode.ValueSymtab:
						Debug.Assert(ValueSymtab == null);
						ValueSymtab = new ValueSymtabBlock(child);
						break;
					case BlockCode.Metadata:
						Debug.Assert(Metadata == null);
						Metadata = new MetadataBlock(child);
						break;
					case BlockCode.Type:
						Debug.Assert(Type == null);
						Type = new TypeBlock(child);
						break;
					case BlockCode.OperandBundleTags:
						Debug.Assert(OperandBundleTags == null);
						OperandBundleTags = new OperandBundleTagsBlock(child);
						break;
					case BlockCode.MetadataKind:
						Debug.Assert(MetadataKind == null);
						MetadataKind = new MetadataKindBlock(child);
						break;
					case BlockCode.SyncScopeNames:
						Debug.Assert(SyncScopeNames == null);
						SyncScopeNames = new SyncScopeNamesBlock(child);
						break;
					case BlockCode bc: throw new NotSupportedException($"Unexpected subblock in Module: {bc}");
				}
		}
	}

	public class FunctionRecord {
		public readonly uint StrtabOffset, StrtabSize, Type, CallingConvention;
		public readonly bool IsProto;
		public readonly uint Linkage, ParamAttr, Alignment, Section, Visibility, Gc;
		public readonly uint UnnamedAddr, PrologueData, DllStorageClass, Comdat, PrefixData;
		public readonly uint PersonalityFn, PreemptionSpecifier;

		public FunctionRecord(List<uint> record) {
			Debug.Assert(record.Count == 18);
			StrtabOffset = record[0];
			StrtabSize = record[1];
			Type = record[2];
			CallingConvention = record[3];
			IsProto = record[4] != 0;
			Linkage = record[5];
			ParamAttr = record[6];
			Alignment = record[7];
			Section = record[8];
			Visibility = record[9];
			Gc = record[10];
			UnnamedAddr = record[11];
			PrologueData = record[12];
			DllStorageClass = record[13];
			Comdat = record[14];
			PrefixData = record[15];
			PersonalityFn = record[16];
			PreemptionSpecifier = record[17];
		}
	}
}