using System;
using System.Collections.Generic;

namespace BitcodeSharp {
	public enum BlockCode : uint {
		Module = 8, 
		ParamAttr, 
		ParamAttrGroup, 
		Constants, 
		Function, 
		Identification, 
		ValueSymtab, 
		Metadata, 
		MetadataAttachment, 
		Type, 
		Uselist, 
		ModuleStrtab, 
		GlobalValSummary, 
		OperandBundleTags, 
		MetadataKind, 
		Strtab, 
		FullLtoGlobalValSummary, 
		Symtab, 
		SyncScopeNames
	}

	public enum FunctionRecordCode : uint {
		DeclareBlocks = 1, 
		BinOp, 
		Cast, 
		GepOld, 
		Select, 
		ExtractElt, 
		InsertElt, 
		ShuffleVec, 
		Cmp, 
		Ret, 
		Br, 
		Switch, 
		Invoke, 
		Unreachable = 15, 
		Phi, 
		Alloca = 19, 
		Load, 
		VaArg = 23, 
		StoreOld, 
		ExtractVal = 26, 
		InsertVal, 
		Cmp2, 
		VSelect, 
		InboundsGepOld, 
		IndirectBr, 
		DebugLocAgain = 33, 
		Call, 
		DebugLoc, 
		Fence, 
		CmpxchgOld, 
		AtomicRmw, 
		Resume, 
		LandingPadOld, 
		LoadAtomic, 
		StoreAtomicOld, 
		Gep, 
		Store, 
		StoreAtomic, 
		Cmpxchg, 
		LandingPad, 
		CleanupRet, 
		CatchRet, 
		CatchPad, 
		CleanupPad, 
		CatchSwitch, 
		Bundle, 
		UnOp
	}

	public enum MetadataAttachmentRecordCode : uint {
	}

	public enum UselistRecordCode : uint {
	}

	public enum ModuleStrtabRecordCode : uint {
	}

	public enum GlobalValSummaryRecordCode : uint {
	}

	public enum StrtabRecordCode : uint {
		Blob = 1
	}

	public enum FullLtoGlobalValSummaryRecordCode : uint {
	}

	public enum SymtabRecordCode : uint {
		Blob = 1
	}

	public enum SyncScopeNamesRecordCode : uint {
		Name = 1
	}
}