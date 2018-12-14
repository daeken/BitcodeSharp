using System.Diagnostics;

namespace BitcodeSharp {
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
	
	public class TypeBlock {
		public TypeBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.Type);
		}
	}
}