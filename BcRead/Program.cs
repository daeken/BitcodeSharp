using System;
using System.IO;
using BitcodeSharp;

namespace BcRead {
	class Program {
		static void Main(string[] args) {
			var data = File.ReadAllBytes(args[0]);
			var br = new BitcodeReader(data);
		}
	}
}