namespace BitcodeSharp {
	public class Bitstream {
		byte[] Data;
		int ByteOff, BitOff;

		public bool End => ByteOff >= Data.Length;

		public Bitstream(byte[] data) => Data = data;

		public uint ConsumeOne() {
			var bit = (Data[ByteOff] >> BitOff) & 1;
			if(++BitOff == 8) {
				BitOff = 0;
				ByteOff++;
			}
			return (uint) bit;
		}

		public uint Consume(uint bits) {
			var v = 0U;
			for(var i = 0; i < bits; ++i)
				v |= ConsumeOne() << i;
			return v;
		}

		public uint Vbr(uint bits) {
			var v = 0U;
			var off = 0U;
			while(true) {
				var c = Consume(bits);
				v |= (uint) ((c & ((1 << (int) (bits - 1)) - 1)) << (int) off);
				off += bits - 1;
				if(c >> (int) (bits - 1) == 0)
					return v;
			}
		}

		public void Align32() {
			while(BitOff != 0 || (ByteOff & 3) != 0)
				ConsumeOne();
		}

		public char Char6() => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._"[(int) Consume(6)];
	}
}