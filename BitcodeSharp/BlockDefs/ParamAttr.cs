using System.Diagnostics;

namespace BitcodeSharp {
	public enum ParamAttrRecordCode : uint {
		Entry = 2
	}

	public class ParamAttrBlock {
		public ParamAttrBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.ParamAttr);
		}
	}
}