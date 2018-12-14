using System.Diagnostics;

namespace BitcodeSharp {
	public enum ParamAttrGroupRecordCode : uint {
		Entry = 3
	}

	public class ParamAttrGroupBlock {
		public ParamAttrGroupBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.ParamAttrGroup);
		}
	}
}