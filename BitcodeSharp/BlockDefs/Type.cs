using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PrettyPrinter;

namespace BitcodeSharp {
	public enum TypeRecordCode : uint {
		NumEntry = 1, 
		Void, 
		Float, 
		Double, 
		Label, 
		Opaque, 
		Integer, 
		Pointer, 
		FunctionOld, 
		Half, 
		Array, 
		Vector, 
		X86Fp80, 
		Fp128, 
		PpcFp128, 
		Metadata, 
		X86Mmx, 
		StructAnon, 
		StructName, 
		StructNamed, 
		Function
	}

	public class TypeBlock {
		readonly List<(TypeRecordCode Code, List<uint> Data)> RawRecords;
		readonly List<LlvmType> Types;
		
		public TypeBlock(Block rb) {
			Debug.Assert(rb.BlockId == BlockCode.Type);
			Debug.Assert(rb.Children.Count == 0);

			var hasNumEntry = (TypeRecordCode) rb.Records[0].Code == TypeRecordCode.NumEntry;
			Debug.Assert(!hasNumEntry || rb.Records[0].Data[0] + 1 == rb.Records.Count);
			RawRecords = rb.Records.Skip(hasNumEntry ? 1 : 0).Select(x => ((TypeRecordCode) x.Code, x.Data)).ToList();
			Types = Enumerable.Range(0, RawRecords.Count).Select(x => (LlvmType) null).ToList();
			for(var i = 0U; i < RawRecords.Count; ++i)
				ParseType(i);
		}

		LlvmType ParseType(uint i) {
			LlvmType Sub() {
				var (code, record) = RawRecords[(int) i];
				switch(code) {
					case TypeRecordCode.Void: return new VoidType();
					case TypeRecordCode.Integer: return new IntegerType(record[0]);
					case TypeRecordCode.Pointer: return new PointerType(ParseType(record[0]), record[1]);
					case TypeRecordCode.Function:
						return new FunctionType(record[0] != 0, ParseType(record[1]),
							record.Skip(2).Select(ParseType).ToList());
					case TypeRecordCode.Metadata: return new MetadataType();
					case TypeRecordCode rc: throw new NotSupportedException($"Unexpected record in Type: {rc}");
				}
			}

			if(Types[(int) i] != null) return Types[(int) i];
			return Types[(int) i] = Sub();
		}
	}
	
	public abstract class LlvmType {
	}

	public class VoidType : LlvmType {
	}

	public class IntegerType : LlvmType {
		public readonly uint Width;

		public IntegerType(uint width) => Width = width;
	}

	public class PointerType : LlvmType {
		public readonly LlvmType PointeeType;
		public readonly uint AddressSpace;

		public PointerType(LlvmType pointeeType, uint addressSpace) {
			PointeeType = pointeeType;
			AddressSpace = addressSpace;
		}
	}

	public class MetadataType : LlvmType {
	}

	public class FunctionType : LlvmType {
		public readonly bool HasVarargs;
		public readonly LlvmType ReturnType;
		public readonly IReadOnlyList<LlvmType> ParamTypes;

		public FunctionType(bool hasVarargs, LlvmType returnType, IReadOnlyList<LlvmType> paramTypes) {
			HasVarargs = hasVarargs;
			ReturnType = returnType;
			ParamTypes = paramTypes;
		}
	}
}