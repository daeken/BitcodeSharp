using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PrettyPrinter;

namespace BitcodeSharp {
	public class Block {
		internal readonly uint AbbrLen;
		public readonly BlockCode BlockId;
		internal readonly Dictionary<uint, List<(string, uint)>> Abbrevs = new Dictionary<uint, List<(string, uint)>>();
		public readonly List<Block> Children = new List<Block>();
		public readonly List<List<uint>> Records = new List<List<uint>>();

		public Block(uint abbrLen, uint blockId) {
			AbbrLen = abbrLen;
			BlockId = (BlockCode) blockId;
		}

		public void AssignAbbrev(List<(string, uint)> abbr) => Abbrevs[(uint) (Abbrevs.Count + 4)] = abbr;
	}

	public class BitcodeTreeReader {
		public List<Block> Blocks => BlockStack.First().Children;
		
		readonly Bitstream BS;
		readonly Dictionary<uint, Dictionary<uint, List<(string, uint)>>> BlockInfoAbbrevs = new Dictionary<uint, Dictionary<uint, List<(string, uint)>>>();
		readonly Stack<Block> BlockStack = new Stack<Block>();
		Block Cur => BlockStack.Peek();
		bool InBlockInfo;
		uint? BlockInfoNum;
		
		public BitcodeTreeReader(byte[] data) {
			BlockStack.Push(new Block(2, 0));
			BS = new Bitstream(data);
			var magic = BS.Consume(32);
			Debug.Assert(magic == 0xdec04342);
			while(!BS.End)
				ReadNext();
		}

		void ReadNext() {
			switch(BS.Consume(BlockStack.Peek().AbbrLen)) {
				case 0: // END_BLOCK
					//"END_BLOCK".Print();
					BS.Align32();
					BlockStack.Pop();
					InBlockInfo = false;
					break;
				case 1: // ENTER_SUBBLOCK
					var blockId = BS.Vbr(8);
					//$"ENTER_SUBBLOCK {blockId}".Print();
					if(blockId == 0) {
						InBlockInfo = true;
						BlockInfoNum = null;
					}
					var block = new Block(BS.Vbr(4), blockId);
					if(BlockInfoAbbrevs.ContainsKey(blockId))
						foreach(var (k, v) in BlockInfoAbbrevs[blockId])
							block.Abbrevs[k] = v;
					if(blockId != 0)
						Cur.Children.Add(block);
					BlockStack.Push(block);
					BS.Align32();
					var sizeInWords = BS.Consume(32);
					break;
				case 2: { // DEFINE_ABBREV
					//"DEFINE_ABBREV".Print();
					var numOps = BS.Vbr(5);
					var abbr = new List<(string, uint)>();
					for(var i = 0; i < numOps; ++i)
						if(BS.ConsumeOne() == 1)
							abbr.Add(("literal", BS.Vbr(8)));
						else
							switch(BS.Consume(3)) {
								case 1:
									abbr.Add(("fixed", BS.Vbr(5)));
									break;
								case 2:
									abbr.Add(("vbr", BS.Vbr(5)));
									break;
								case 3:
									abbr.Add(("array", 0));
									break;
								case 4:
									abbr.Add(("char6", 0));
									break;
								case 5:
									abbr.Add(("blob", 0));
									break;
								case uint encoding:
									throw new NotSupportedException($"Unknown encoding in DEFINE_ABBREV: {encoding}");
							}

					if(InBlockInfo) {
						Debug.Assert(BlockInfoNum != null);
						if(!BlockInfoAbbrevs.ContainsKey(BlockInfoNum.Value))
							BlockInfoAbbrevs[BlockInfoNum.Value] = new Dictionary<uint, List<(string, uint)>>();
						var id = (uint) BlockInfoAbbrevs[BlockInfoNum.Value].Count + 4;
						BlockInfoAbbrevs[BlockInfoNum.Value][id] = abbr;
					} else
						Cur.AssignAbbrev(abbr);
					break;
				}
				case 3: { // UNABBREV_RECORD
					//"UNABBREV_RECORD".Print();
					var data = new List<uint> { BS.Vbr(6) };
					var numOps = BS.Vbr(6);
					for(var i = 0; i < numOps; ++i)
						data.Add(BS.Vbr(6));
					Cur.Records.Add(data);
					if(InBlockInfo && data[0] == 1) // SETBID
						BlockInfoNum = data[1];
					break;
				}
				case uint abbr when Cur.Abbrevs.ContainsKey(abbr): {
					//$"Abbreviation {abbr}".Print();
					var abbrev = Cur.Abbrevs[abbr];
					var data = new List<uint>();
					for(var i = 0; i < abbrev.Count; ++i) {
						var elem = abbrev[i];
						switch(elem.Item1) {
							case "array": {
								var length = BS.Vbr(6);
								var type = abbrev[++i];
								for(var j = 0; j < length; ++j)
									switch(type.Item1) {
										case "char6":
											data.Add(BS.Char6());
											break;
										case "fixed":
											data.Add(BS.Consume(type.Item2));
											break;
										case string _type: throw new NotSupportedException($"Unknown type in array {_type}");
									}
								break;
							}
							case "blob": {
								var length = BS.Vbr(6);
								BS.Align32();
								for(var j = 0; j < length; ++j)
									data.Add(BS.Consume(8));
								BS.Align32();
								break;
							}
							case "literal":
								data.Add(elem.Item2);
								break;
							case "vbr":
								data.Add(BS.Vbr(elem.Item2));
								break;
							case "fixed":
								data.Add(BS.Consume(elem.Item2));
								break;
							case string type: throw new NotSupportedException($"Unknown type in abbrev {type}");
						}
					}
					Cur.Records.Add(data);
					break;
				}
				case uint abbr:
					throw new NotSupportedException($"Unknown abbrev {abbr}");
			}
		}
	}
}