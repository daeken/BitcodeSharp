using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BitcodeSharp {
	public enum IdentificationRecordCode : uint {
		String = 1, 
		Epoch
	}

	public class IdentificationBlock {
		public readonly string String;
		public readonly uint? Epoch;
		
		public IdentificationBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.Identification);
			Debug.Assert(rb.Children.Count == 0);
			foreach(var (code, record) in rb.Records)
				switch((IdentificationRecordCode) code) {
					case IdentificationRecordCode.String:
						Debug.Assert(String == null);
						String = Encoding.UTF8.GetString(record.Select(x => (byte) x).ToArray());
						break;
					case IdentificationRecordCode.Epoch:
						Debug.Assert(Epoch == null);
						Epoch = record[0];
						break;
					case IdentificationRecordCode rc: throw new NotSupportedException($"Unexpected record in Identification: {rc}");
				}
		}
	}
}