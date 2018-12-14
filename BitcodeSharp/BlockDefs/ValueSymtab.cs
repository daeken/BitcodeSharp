using System.Diagnostics;

namespace BitcodeSharp {
	public enum ValueSymtabRecordCode : uint {
		Entry = 1, 
		BbEntry, 
		FnEntry, 
		CombinedEntry = 5
	}

	public class ValueSymtabBlock {
		public ValueSymtabBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.ValueSymtab);
		}
	}
}