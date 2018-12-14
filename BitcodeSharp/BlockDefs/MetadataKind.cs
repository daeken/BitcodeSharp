using System.Diagnostics;

namespace BitcodeSharp {
	public enum MetadataKindRecordCode : uint {
		Kind = 6
	}

	public class MetadataKindBlock {
		public MetadataKindBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.MetadataKind);
		}
	}
}