using System.Diagnostics;

namespace BitcodeSharp {
	public enum OperandBundleTagsRecordCode : uint {
		Tag = 1
	}

	public class OperandBundleTagsBlock {
		public OperandBundleTagsBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.OperandBundleTags);
		}
	}
}