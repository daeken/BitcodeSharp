using System.Diagnostics;

namespace BitcodeSharp {
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
	
	public class ConstantsBlock {
		public ConstantsBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.Constants);
		}
	}
}