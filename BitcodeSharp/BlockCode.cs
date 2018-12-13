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

	public enum ModuleRecordCode : uint {
		Version = 1, 
		Triple = 2, 
		DataLayout = 3, 
		Function = 8, 
		VstOffset = 13, 
		SourceFilename = 16
	}

	public enum ParamAttrRecordCode : uint {
		Entry = 2
	}

	public enum ParamAttrGroupRecordCode : uint {
		Entry = 3
	}

	public enum ConstantsRecordCode : uint {
		SetType = 1, 
		Null, 
		Undef, 
		Integer, 
		WideInteger, 
		Float, 
		Aggregate, 
		String, 
		CString, 
		CEBinOp, 
		CECast, 
		CEGep, 
		CESelect, 
		CEExtractElt, 
		CEInsertElt, 
		CEShuffleVec, 
		CECmp, 
		InlineAsmOld, 
		CEShufVecEx, 
		CEInboundsGep, 
		BlockAddress, 
		Data, 
		IlineAsm, 
		CEGepWithInRangeIndex, 
		CEUnOp
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

	public enum IdentificationRecordCode : uint {
		String = 1, 
		Epoch
	}

	public enum ValueSymtabRecordCode : uint {
		Entry = 1, 
		BbEntry, 
		FnEntry, 
		CombinedEntry = 5
	}

	public enum MetadataRecordCode : uint {
		StringOld = 1, 
		Value, 
		Node, 
		Name, 
		DistinctNode, 
		Kind, 
		Location, 
		OldNode, 
		OldFnNode, 
		NamedNode, 
		Attachment, 
		GenericDebug, 
		Subrange, 
		Enumerator, 
		BasicType, 
		File, 
		DerivedType, 
		CompositeType, 
		SubroutineType, 
		CompileUnit, 
		Subprogram, 
		LexicalBlock, 
		LexicalBlockFile, 
		Namespace, 
		TemplateType, 
		TemplateValue, 
		GlobalVar, 
		LocalVar, 
		Expression, 
		ObjCProperty, 
		ImportedEntity, 
		Module, 
		Macro, 
		MacroFile, 
		Strings, 
		GlobalDeclAttachment, 
		GlobalValExpr, 
		IndexOffset, 
		Index, 
		Label
	}

	public enum MetadataAttachmentRecordCode : uint {
	}

	public enum TypeRecordCode : uint {
		NumEntry = 1, 
		Void, 
		Float, 
		Double, 
		Label, 
		Opaque, 
		Integer, 
		Pointer, 
		FunctionOld, 
		Half, 
		Array, 
		Vector, 
		X86Fp80, 
		Fp128, 
		PpcFp128, 
		Metadata, 
		X86Mmx, 
		StructAnon, 
		StructName, 
		StructNamed, 
		Function
	}

	public enum UselistRecordCode : uint {
	}

	public enum ModuleStrtabRecordCode : uint {
	}

	public enum GlobalValSummaryRecordCode : uint {
	}

	public enum OperandBundleTagsRecordCode : uint {
		Tag = 1
	}

	public enum MetadataKindRecordCode : uint {
		Kind = 6
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