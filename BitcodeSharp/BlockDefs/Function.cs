using System.Diagnostics;

namespace BitcodeSharp {
	public class FunctionBlock {
		public FunctionBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.Function);
		}
	}
}