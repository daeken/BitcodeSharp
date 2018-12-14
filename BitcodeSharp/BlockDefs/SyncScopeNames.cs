using System.Diagnostics;

namespace BitcodeSharp {
	public class SyncScopeNamesBlock {
		public SyncScopeNamesBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.SyncScopeNames);
		}
	}
}