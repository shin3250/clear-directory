using System;
using System.IO;

namespace clear_directory {
    class Program {
        static int Main(string[] args) {
            if(args.Length != 1) {
                Console.WriteLine("対象ディレクトリを指定してください。");
                return -1;
            }

            if(!Directory.Exists(args[0])) {
                Console.WriteLine("対象ディレクトリが存在しません。");
                return -1;
            }

            foreach(var file in Directory.GetFiles(args[0], "*.*", SearchOption.TopDirectoryOnly)) {
                try { File.Delete(file); } catch { };
            }

            foreach(var dir in Directory.GetDirectories(args[0], "*.*", SearchOption.TopDirectoryOnly)) {
                try { Directory.Delete(dir, true); } catch { };
            }

            return 0;
        }
    }
}
