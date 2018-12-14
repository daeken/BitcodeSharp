using System;
using System.Diagnostics;
using PrettyPrinter;

namespace BitcodeSharp {
	public class BitcodeReader {
		public readonly IdentificationBlock Identification;
		public readonly ModuleBlock Module;
		
		public BitcodeReader(byte[] data) {
			var blocks = new BitcodeTreeReader(data).Blocks;
			//foreach(var block in blocks)
			//	DisplayRaw(block);
			foreach(var block in blocks)
				switch(block.BlockId) {
					case BlockCode.Identification:
						Debug.Assert(Identification == null);
						Identification = new IdentificationBlock(block);
						break;
					case BlockCode.Module:
						Debug.Assert(Module == null);
						Module = new ModuleBlock(block);
						break;
					case BlockCode.Symtab:
						break;
					case BlockCode.Strtab:
						break;
					case BlockCode bc: throw new NotSupportedException($"Unknown top-level block {bc}");
				}
		}

		void DisplayRaw(Block block, int depth = 0) {
			Console.WriteLine(new string('\t', depth++) + $"Block {block.BlockId}");
			foreach(var (code, record) in block.Records) {
				var rce = GetRecordEnum(block.BlockId, code);
				Console.WriteLine(new string('\t', depth) + $"Record {rce}");
			}
			foreach(var child in block.Children)
				DisplayRaw(child, depth);
		}

		object GetRecordEnum(BlockCode bc, uint rc) {
			switch(bc) {
				case BlockCode.Module: return (ModuleRecordCode) rc;
				case BlockCode.ParamAttr: return (ParamAttrRecordCode) rc;
				case BlockCode.ParamAttrGroup: return (ParamAttrGroupRecordCode) rc;
				case BlockCode.Constants: return (ConstantsRecordCode) rc;
				case BlockCode.Function: return (FunctionRecordCode) rc;
				case BlockCode.Identification: return (IdentificationRecordCode) rc;
				case BlockCode.ValueSymtab: return (ValueSymtabRecordCode) rc;
				case BlockCode.Metadata: return (MetadataRecordCode) rc;
				case BlockCode.MetadataAttachment: return (MetadataAttachmentRecordCode) rc;
				case BlockCode.Type: return (TypeRecordCode) rc;
				case BlockCode.Uselist: return (UselistRecordCode) rc;
				case BlockCode.ModuleStrtab: return (ModuleStrtabRecordCode) rc;
				case BlockCode.GlobalValSummary: return (GlobalValSummaryRecordCode) rc;
				case BlockCode.OperandBundleTags: return (OperandBundleTagsRecordCode) rc;
				case BlockCode.MetadataKind: return (MetadataKindRecordCode) rc;
				case BlockCode.Strtab: return (StrtabRecordCode) rc;
				case BlockCode.FullLtoGlobalValSummary: return (FullLtoGlobalValSummaryRecordCode) rc;
				case BlockCode.Symtab: return (SymtabRecordCode) rc;
				case BlockCode.SyncScopeNames: return (SyncScopeNamesRecordCode) rc;
				default: throw new NotSupportedException($"Unknown block code to GetRecordEnum {bc} ({(uint) bc})");
			}
		}
	}
}