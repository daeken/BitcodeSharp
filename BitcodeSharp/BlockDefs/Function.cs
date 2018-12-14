using System.Diagnostics;

namespace BitcodeSharp {
	public class FunctionBlock {
		public FunctionBlock(ModuleBlock module, Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.Function);
			Debug.Assert(rb.Children.Count == 0);
		}
	}
}