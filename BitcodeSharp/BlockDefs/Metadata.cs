using System.Diagnostics;

namespace BitcodeSharp {
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
	
	public class MetadataBlock {
		public MetadataBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.Metadata);
		}
	}
}